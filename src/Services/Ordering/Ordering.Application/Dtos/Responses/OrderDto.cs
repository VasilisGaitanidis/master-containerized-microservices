using System.Collections.Generic;

namespace Ordering.Application.Dtos.Responses
{
    /// <summary>
    /// The order DTO.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// The order's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The order's total price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The order's shipping address.
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// The order's buyer.
        /// </summary>
        public virtual BuyerDto Buyer { get; set; }

        /// <summary>
        /// The order's items.
        /// </summary>
        public virtual IEnumerable<OrderItemDto> Items { get; set; }
    }
}