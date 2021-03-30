using System.Collections.Generic;
using System.Linq;

namespace Cart.Domain.Entities
{
    public class ShoppingCart
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="username">The shopping cart username.</param>
        public ShoppingCart(string username)
        {
            Username = username;
            Items = new List<ShoppingCartItem>();
        }

        /// <summary>
        /// The username 
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The shopping cart items.
        /// </summary>
        public IEnumerable<ShoppingCartItem> Items { get; set; }

        /// <summary>
        /// The shopping cart total price.
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return Items.Sum(item => item.Price * item.Quantity);
            }
        }
    }
}