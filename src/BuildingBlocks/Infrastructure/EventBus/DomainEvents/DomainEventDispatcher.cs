using System;
using System.Threading.Tasks;
using Domain.Core.Messaging;
using Infrastructure.Messaging.Outbox;
using MediatR;
using Newtonsoft.Json;

namespace Infrastructure.EventBus.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IOutboxMessageRepository outboxMessageRepository, IMediator mediator)
        {
            _outboxMessageRepository = outboxMessageRepository ?? throw new ArgumentNullException(nameof(outboxMessageRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Dispatch(IDomainEvent domainEvent)
        {
           var outboxMessage = new OutboxMessage(
               id: domainEvent.Id,
               occurredOn: domainEvent.OccurredOn,
               type: domainEvent.GetType().FullName,
               data: JsonConvert.SerializeObject(domainEvent));

            await _outboxMessageRepository.AddAsync(outboxMessage);

            await _outboxMessageRepository.UnitOfWork.SaveChangesAsync();

            await _mediator.Publish(new DomainEnvelope<IDomainEvent>(domainEvent));
        }
    }
}