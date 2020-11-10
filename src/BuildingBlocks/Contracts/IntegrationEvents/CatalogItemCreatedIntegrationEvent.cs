using System;

namespace Contracts.IntegrationEvents
{
    // ReSharper disable once InconsistentNaming
    public interface CatalogItemCreatedIntegrationEvent
    {
        string Name { get; }

        string Description { get; }

        decimal Price { get; }

        int Stock { get; }

        Guid CatalogTypeId { get; }
    }
}