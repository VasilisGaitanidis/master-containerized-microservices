﻿using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Events;
using MediatR;

namespace Catalog.Application.UseCases.Events.CatalogItemStockChanged
{
    public class CatalogItemStockChangedDomainEventHandler : INotificationHandler<CatalogItemStockChangedDomainEvent>
    {
        public Task Handle(CatalogItemStockChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}