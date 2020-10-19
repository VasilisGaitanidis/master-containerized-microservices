using System.Threading.Tasks;

namespace Infrastructure.EventBus.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync();
    }
}