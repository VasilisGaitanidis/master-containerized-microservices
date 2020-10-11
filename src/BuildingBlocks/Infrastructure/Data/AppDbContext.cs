using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Models;
using Infrastructure.EventBus.DomainEvents;
using Microsoft.EntityFrameworkCore;
using ReflectionMagic;

namespace Infrastructure.Data
{
    public abstract class AppDbContext : DbContext
    {
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
    }
}