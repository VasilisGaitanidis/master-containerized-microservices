using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Events;
using MediatR;

namespace Catalog.Application.UseCases.Events.CatalogItemPriceChanged
{
    public class CatalogItemPriceChangedDomainEventHandler : INotificationHandler<CatalogItemPriceChangedDomainEvent>
    {
        public Task Handle(CatalogItemPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}