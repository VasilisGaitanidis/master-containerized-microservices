﻿using System;
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
            // TODO refactor OutboxMessage
            //var outboxMessage = new OutboxMessage(
            //    domainEvent.Id,
            //    domainEvent.OccurredOn,
            //    domainEvent.GetType().FullName,
            //    JsonConvert.SerializeObject(domainEvent));

            //await _outboxMessageRepository.AddAsync(outboxMessage);

            //await _outboxMessageRepository.UnitOfWork.SaveChangesAsync();

            // TODO Publish message with a background service.
            await _mediator.Publish(new DomainEnvelope<IDomainEvent>(domainEvent));
        }
    }
}