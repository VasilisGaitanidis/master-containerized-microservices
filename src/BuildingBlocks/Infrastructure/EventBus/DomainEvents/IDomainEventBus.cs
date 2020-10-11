using System.Threading.Tasks;
using Domain.Core.Messaging;

namespace Infrastructure.EventBus.DomainEvents
{
    public interface IDomainEventBus
    {
        Task Handle(IDomainEvent domainEvent);
    }
}