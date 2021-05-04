using System;
using System.Collections.Generic;
using Domain.Models;

namespace Ordering.Domain.Entities
{
    /// <summary>
    /// The order domain entity.
    /// </summary>
    public class Order : AggregateRoot<Guid>
    {
        protected Order() : base(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/>.
        /// </summary>
        /// <param name="username">The order's username.</param>
        /// <param name="totalPrice">The order's total price.</param>
        /// <param name="shippingAddress">The order's shipping address.</param>
        /// <param name="buyer">The order's buyer. </param>
        /// <param name="items">The order's items.</param>
        public Order(string username, decimal totalPrice, string shippingAddress, Buyer buyer, IEnumerable<OrderItem> items) : this()
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));
            Buyer = buyer ?? throw new ArgumentNullException(nameof(buyer));
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        /// <summary>
        /// The order's username.
        /// </summary>
        public string Username { get; protected set; }

        /// <summary>
        /// The order's total price.
        /// </summary>
        public decimal TotalPrice { get; protected set; }
        
        /// <summary>
        /// The order's shipping address.
        /// </summary>
        public string ShippingAddress { get; protected set; }

        /// <summary>
        /// The order's buyer.
        /// </summary>
        public Buyer Buyer { get; protected set; }

        /// <summary>
        /// The order's items.
        /// </summary>
        public IEnumerable<OrderItem> Items { get; protected set; }
    }
}