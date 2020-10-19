using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Models;
using Infrastructure.EventBus.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ReflectionMagic;

namespace Infrastructure.Data
{
    public abstract class AppDbContext : DbContext, ITransactionContext
    {
        private IDbContextTransaction _currentTransaction;
        private readonly IEnumerable<IDomainEventDispatcher> _domainEventDispatchers = null;

        protected AppDbContext(DbContextOptions options, IEnumerable<IDomainEventDispatcher> domainEventDispatchers = null)
            : base(options)
        {
            _domainEventDispatchers = domainEventDispatchers;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            DispatchDomainEvents();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            DispatchDomainEvents();
            return base.SaveChanges();
        }

        private void DispatchDomainEvents()
        {
            var entities = ChangeTracker
                .Entries()
                .Select(e => e.Entity)
                .Where(e =>
                {
                    var baseType = e.GetType().BaseType;
                    return baseType is { } && baseType.IsGenericType && baseType.GetGenericTypeDefinition()
                        .IsAssignableFrom(typeof(AggregateRoot<>));
                })
                .Where(e => e.AsDynamic().DomainEvents.Count > 0)
                .ToArray();

            foreach (var entity in entities)
            {
                var rootAggregator = entity.AsDynamic();
                var domainEvents = rootAggregator.DomainEvents;

                foreach (var domainEvent in domainEvents)
                    _domainEventDispatchers.Select(b => b.Dispatch(domainEvent));

                rootAggregator.ClearDomainEvents();
            }
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
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