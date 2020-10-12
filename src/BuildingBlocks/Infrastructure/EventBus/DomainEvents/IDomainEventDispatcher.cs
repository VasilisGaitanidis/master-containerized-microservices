using System.Threading.Tasks;
using Domain.Core.Messaging;

namespace Infrastructure.EventBus.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}