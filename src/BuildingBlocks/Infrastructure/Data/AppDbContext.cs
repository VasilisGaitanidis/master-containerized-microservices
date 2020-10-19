using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public abstract class AppDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        protected AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public async Task RetryOnExceptionAsync(Func<Task> operation)
        {
            await Database.CreateExecutionStrategy().ExecuteAsync(operation);
        }
    }
}