namespace Cart.Application.Dtos.Requests
{
    public class CheckoutShoppingCartDto
    {
        /// <summary>
        /// The shopping cart's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The shopping cart's total price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The shipping address.
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// The shopping cart's buyer.
        /// </summary>
        public BuyerDto Buyer { get; set; }
    }
}