using Domain.Core.Messaging;
using MediatR;

namespace Infrastructure.EventBus.DomainEvents
{
    public class DomainNotificationEnvelope : INotification
    {
        public IDomainEvent Event { get; }

        public DomainNotificationEnvelope(IDomainEvent @event)
        {
            Event = @event;
        }
    }
}