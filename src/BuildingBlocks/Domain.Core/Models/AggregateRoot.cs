using System.Collections.Generic;
using Domain.Core.Messaging;

namespace Domain.Core.Models
{
    /// <summary>
    /// The aggregate root interface.
    /// </summary>
    /// <typeparam name="TId">The generic identifier.</typeparam>
    public interface IAggregateRoot<out TId> : IEntity<TId>
    {
        /// <summary>
        /// Add the <paramref name="domainEvent"/> on the aggregate root.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        void AddDomainEvent(DomainEvent domainEvent);

        /// <summary>
        /// Remove a domain event from the aggregate root.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        void RemoveDomainEvent(DomainEvent domainEvent);

        /// <summary>
        /// Clears all domain events from the aggregate root.
        /// </summary>
        void ClearDomainEvents();
    }

    /// <summary>
    /// The aggregate root base class.
    /// </summary>
    /// <typeparam name="TId">The generic identifier.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    {
        private List<DomainEvent> _domainEvents;

        /// <summary>
        /// The aggregate root domain events.
        /// </summary>
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected AggregateRoot(TId id) : base(id)
        {
        }

        /// <inheritdoc />
        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        /// <inheritdoc />
        public void RemoveDomainEvent(DomainEvent domainEvent)
            => _domainEvents?.Remove(domainEvent);

        /// <inheritdoc />
        public void ClearDomainEvents()
            => _domainEvents?.Clear();
    }
}