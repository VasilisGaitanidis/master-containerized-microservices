using System;
using System.Data;
using System.Threading.Tasks;
using Catalog.Domain.Models;
using Catalog.Infrastructure.EntityConfigurations;
using Domain.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Infrastructure
{
    public class CatalogDataContext : DbContext, IUnitOfWork, IDbContext
    {
        /// <summary>
        /// The default database schema.
        /// </summary>
        public const string DefaultSchema = "catalog";

        private IDbContextTransaction _currentTransaction;

        public DbSet<CatalogItem> CatalogItems { get; set; }

        public DbSet<CatalogType> CatalogTypes { get; set; }

        public CatalogDataContext(DbContextOptions<CatalogDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
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