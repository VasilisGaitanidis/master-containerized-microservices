using Domain.Core.Messaging;

namespace Catalog.Domain.Events
{
    public class CatalogItemPriceChangedDomainEvent : DomainEvent
    {
        public decimal Price { get; }

        public CatalogItemPriceChangedDomainEvent(decimal price)
        {
            Price = price;
        }
    }
}