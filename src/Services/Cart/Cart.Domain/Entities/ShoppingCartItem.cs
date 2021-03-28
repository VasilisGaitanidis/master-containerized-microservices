namespace Cart.Domain.Entities
{
    public class ShoppingCartItem
    {
        /// <summary>
        /// The shopping cart item quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The shopping cart item color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The shopping cart price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The shopping cart product name.
        /// </summary>
        public string ProductName { get; set; }
    }
}