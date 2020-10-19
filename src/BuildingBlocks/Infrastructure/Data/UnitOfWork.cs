using System;
using System.Data;
using System.Threading.Tasks;
using Domain.Core.Data;
using Infrastructure.EventBus.DomainEvents;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public UnitOfWork(AppDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            await _context.BeginTransactionAsync(isolationLevel);
        }

        public async Task CommitTransactionAsync()
        {
            // dispatch domain events before commiting transaction
            await _domainEventDispatcher.DispatchEventsAsync();
            await _context.CommitTransactionAsync();
        }

        public void RollbackTransaction()
        {
            _context.RollbackTransaction();
        }

        public async Task RetryOnExceptionAsync(Func<Task> operation)
        {
            await _context.RetryOnExceptionAsync(operation);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}