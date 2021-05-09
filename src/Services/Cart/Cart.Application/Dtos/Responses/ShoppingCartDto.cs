using System.Collections.Generic;

namespace Cart.Application.Dtos.Responses
{
    public class ShoppingCartDto
    {
        /// <summary>
        /// The shopping cart user's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The shopping cart total price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The shopping cart items.
        /// </summary>
        public IEnumerable<ShoppingCartItemDto> Items { get; set; }
    }
}