using System;
using MediatR;

namespace Domain.Messaging
{
    /// <summary>
    /// The event interface.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// The event identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The event/aggregate root version
        /// </summary>
        int EventVersion { get; }

        /// <summary>
        /// The date the <see cref="Event"/> occurred on.
        /// </summary>
        DateTime OccurredOn { get; }
    }

    /// <summary>
    /// The integration event interface.
    /// </summary>
    public interface IIntegrationEvent : IEvent
    {
    }

    /// <summary>
    /// The domain event interface.
    /// </summary>
    public interface IDomainEvent : IEvent, INotification
    {
    }

    /// <inheritdoc cref="IEvent"/>
    public abstract class Event : IEvent
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public int EventVersion { get; protected set; } = 1;

        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    }

    public abstract class IntegrationEvent : Event, IIntegrationEvent
    {
    }

    public abstract class DomainEvent : Event, IDomainEvent
    {
    }
}