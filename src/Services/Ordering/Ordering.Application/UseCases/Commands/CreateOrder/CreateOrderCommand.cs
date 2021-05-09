using System;
using System.Collections.Generic;
using MediatR;
using Ordering.Application.Dtos.Requests;

namespace Ordering.Application.UseCases.Commands.CreateOrder
{
    /// <summary>
    /// The create order command.
    /// </summary>
    public class CreateOrderCommand : IRequest<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommand"/>.
        /// </summary>
        /// <param name="username">The order's username.</param>
        /// <param name="totalPrice">The order's total price.</param>
        /// <param name="shippingAddress">The order's shipping address.</param>
        /// <param name="buyer">The order's buyer.</param>
        /// <param name="items">The order items.</param>
        public CreateOrderCommand(string username, decimal totalPrice, string shippingAddress, BuyerDto buyer, IEnumerable<ShoppingCartItemDto> items)
        {
            Username = username;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
            Buyer = buyer;
            Items = items;
        }

        /// <summary>
        /// The order's username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The order's total price.
        /// </summary>
        public decimal TotalPrice { get; }

        /// <summary>
        /// The order's shipping address.
        /// </summary>
        public string ShippingAddress { get; }

        /// <summary>
        /// The order's buyer.
        /// </summary>
        public BuyerDto Buyer { get; }

        /// <summary>
        /// The order items.
        /// </summary>
        public IEnumerable<ShoppingCartItemDto> Items { get; }
    }
}