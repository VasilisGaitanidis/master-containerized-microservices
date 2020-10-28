using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Events;
using MediatR;

namespace Catalog.Application.UseCases.CreateCatalogItem
{
    public class CatalogItemCreatedDomainEventHandler /*: INotificationHandler<CatalogItemCreatedDomainEvent>*/
    {
        public Task Handle(CatalogItemCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}