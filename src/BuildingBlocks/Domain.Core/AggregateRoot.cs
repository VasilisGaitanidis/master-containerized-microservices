using System.Collections.Generic;
using Domain.Core.Messaging;

namespace Domain.Core
{
    public interface IAggregateRoot<out TId> : IEntity<TId>
    {
        void AddDomainEvent(DomainEvent domainEvent);

        void RemoveDomainEvent(DomainEvent domainEvent);

        void ClearDomainEvents();
    }

    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    {
        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected AggregateRoot(TId id) : base(id)
        {
        }

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
            => _domainEvents?.Remove(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents?.Clear();
    }
}