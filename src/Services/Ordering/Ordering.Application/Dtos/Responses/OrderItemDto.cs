namespace Ordering.Application.Dtos.Responses
{
    /// <summary>
    /// The order item DTO.
    /// </summary>
    public class OrderItemDto
    {
        /// <summary>
        /// The order item quantity.
        /// </summary>
        public int Quantity { get; set; }

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