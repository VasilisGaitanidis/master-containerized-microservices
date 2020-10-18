using System;
using Domain.Core.Messaging;

namespace Catalog.Domain.Events
{
    public class CatalogItemCreatedDomainEvent : DomainEvent
    {
        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Stock { get; }

        public Guid CatalogTypeId { get; }

        public CatalogItemCreatedDomainEvent(string name, string description, decimal price, int stock, Guid catalogTypeId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CatalogTypeId = catalogTypeId;
        }
    }
}