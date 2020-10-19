using System.Threading.Tasks;

namespace Infrastructure.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync();
    }
}