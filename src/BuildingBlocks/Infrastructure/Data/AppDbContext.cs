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
        private readonly IEnumerable<IDomainEventBus> _eventBuses = null;

        protected AppDbContext(DbContextOptions options, IEnumerable<IDomainEventBus> eventBuses = null)
            : base(options)
        {
            _eventBuses = eventBuses;
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
                .Where(e =>
                {
                    var baseType = e.GetType().BaseType;
                    return baseType is { } && baseType.IsGenericType && baseType.GetGenericTypeDefinition()
                            .IsAssignableFrom(typeof(AggregateRoot<>));
                })
                .Where(e => e.AsDynamic().DomainEvents.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var rootAggregator = entity.AsDynamic();
                var domainEvents = rootAggregator.DomainEvents;

                foreach (var domainEvent in domainEvents)
                    _eventBuses.Select(b => b.Handle(domainEvent));

                rootAggregator.ClearUncommittedEvents();
            }
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