using System;
using System.Threading.Tasks;
using Domain.Core.Messaging;
using MediatR;

namespace Infrastructure.EventBus.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Dispatch(IDomainEvent domainEvent)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}