using Domain.Messaging;

namespace Catalog.Domain.Events
{
    public class CatalogItemStockChangedDomainEvent : DomainEvent
    {
        public int Stock { get; }

        public CatalogItemStockChangedDomainEvent(int stock)
        {
            Stock = stock;
        }
    }
}