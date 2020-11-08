using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Domain.Events;
using Contracts.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Catalog.Application.UseCases.Events.CatalogItemCreated
{
    public class CatalogItemCreatedDomainEventHandler : INotificationHandler<CatalogItemCreatedDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CatalogItemCreatedDomainEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task Handle(CatalogItemCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish<CatalogItemCreatedIntegrationEvent>(new
            {
                notification.Name,
                notification.Description,
                notification.Price,
                notification.Stock,
                notification.CatalogTypeId
            }, cancellationToken);
        }
    }
}