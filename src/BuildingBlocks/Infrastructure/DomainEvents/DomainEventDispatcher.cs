using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Models;
using Infrastructure.Data;
using MediatR;
using ReflectionMagic;

namespace Infrastructure.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public DomainEventDispatcher(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task DispatchEventsAsync()
        {
            var entities = _context.ChangeTracker
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
                    await _mediator.Publish(domainEvent);

                rootAggregator.ClearDomainEvents();
            }
        }
    }
}