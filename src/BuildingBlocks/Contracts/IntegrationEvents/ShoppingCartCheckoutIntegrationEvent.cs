using System.Collections.Generic;

namespace Contracts.IntegrationEvents
{
    public interface ShoppingCartCheckoutIntegrationEvent
    {
        string Username { get; }

        decimal TotalPrice { get; }

        string ShippingAddress { get; }

        Buyer Buyer { get; }

        IEnumerable<ShoppingCartItem> Items { get; }
    }
}