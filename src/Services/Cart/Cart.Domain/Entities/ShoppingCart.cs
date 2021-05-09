using System.Collections.Generic;
using System.Linq;

namespace Cart.Domain.Entities
{
    public class ShoppingCart
    {
        private readonly List<ShoppingCartItem> _items;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="username">The shopping cart username.</param>
        public ShoppingCart(string username)
        {
            Username = username;
            _items = new List<ShoppingCartItem>();
        }

        /// <summary>
        /// The shopping cart's username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The shopping cart items.
        /// </summary>
        public IEnumerable<ShoppingCartItem> Items => _items;

        /// <summary>
        /// The shopping cart total price.
        /// </summary>
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        /// <summary>
        /// Add shopping cart items to a shopping cart.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="color">The color.</param>
        /// <param name="price">The price.</param>
        /// <param name="productName">The product name.</param>
        public void AddShoppingCartItems(int quantity, string color, decimal price, string productName)
        {
            var shoppingCartItem = new ShoppingCartItem
            {
                Quantity = quantity,
                Color = color,
                Price = price,
                ProductName = productName
            };

            _items.Add(shoppingCartItem);
        }
    }
}