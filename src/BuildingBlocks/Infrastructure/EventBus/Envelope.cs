using Domain.Core.Messaging;
using MediatR;

namespace Infrastructure.EventBus
{
    /// <summary>
    /// The Envelope contains an event in order to decouple <see cref="INotification"/> from <see cref="Domain.Core"/>.
    /// </summary>
    /// <typeparam name="TEvent">The event.</typeparam>
    public interface IEnvelope<out TEvent> : INotification
        where TEvent : IEvent
    {
        TEvent Event { get; }
    }

    public abstract class Envelope<TEvent> : IEnvelope<TEvent>
        where TEvent : IEvent
    {
        public TEvent Event { get; }

        protected Envelope(TEvent @event)
        {
            Event = @event;
        }
    }

    public class DomainEnvelope<TDomainEvent> : Envelope<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public DomainEnvelope(TDomainEvent @event) : base(@event)
        {
        }
    }

    public class IntegrationEnvelope<TIntegrationEvent> : Envelope<TIntegrationEvent>
        where TIntegrationEvent : IIntegrationEvent
    {
        public IntegrationEnvelope(TIntegrationEvent @event) : base(@event)
        {
        }
    }


}