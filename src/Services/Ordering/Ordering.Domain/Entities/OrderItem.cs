using System;
using Domain.Models;

namespace Ordering.Domain.Entities
{
    /// <summary>
    /// The order item domain entity.
    /// </summary>
    public class OrderItem : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/>.
        /// </summary>
        /// <param name="quantity">The order item quantity.</param>
        /// <param name="price">The shopping cart price.</param>
        /// <param name="productName">The shopping cart product name.</param>
        public OrderItem(int quantity, decimal price, string productName) : base(Guid.NewGuid())
        {
            Quantity = quantity;
            Price = price;
            ProductName = productName;
        }

        /// <summary>
        /// The order item quantity.
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// The shopping cart price.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// The shopping cart product name.
        /// </summary>
        public string ProductName { get; }
    }
}
