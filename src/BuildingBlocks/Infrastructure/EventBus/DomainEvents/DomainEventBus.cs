using System;
using System.Threading.Tasks;
using Domain.Core.Messaging;
using Infrastructure.Messaging.Outbox;
using MediatR;
using Newtonsoft.Json;

namespace Infrastructure.EventBus.DomainEvents
{
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMediator _mediator;

        public DomainEventBus(IOutboxMessageRepository outboxMessageRepository, IMediator mediator)
        {
            _outboxMessageRepository = outboxMessageRepository ?? throw new ArgumentNullException(nameof(outboxMessageRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(IDomainEvent domainEvent)
        {
            var outboxMessage = new OutboxMessage(
                domainEvent.Id,
                domainEvent.OccurredOn,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent));

            await _outboxMessageRepository.AddAsync(outboxMessage);

            // save changes and dispatches domain events
            await _outboxMessageRepository.UnitOfWork.SaveChangesAsync();

            await _mediator.Publish(new DomainNotificationEnvelope(domainEvent));
        }
    }
}