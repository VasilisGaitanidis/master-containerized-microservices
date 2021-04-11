namespace Discount.Domain.Entities
{
    public class Coupon
    {
        /// <summary>
        /// Initializes a coupon entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="productName">The product name.</param>
        /// <param name="description">The coupon description.</param>
        /// <param name="amount">The amount.</param>
        public Coupon(int id, string productName, string description, int amount)
        {
            Id = id;
            ProductName = productName;
            Description = description;
            Amount = amount;
        }

        /// <summary>
        /// The coupon identifier.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The coupon product name.
        /// </summary>
        public string ProductName { get; protected set; }

        /// <summary>
        /// The coupon description.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The coupon amount.
        /// </summary>
        public int Amount { get; protected set; }
    }
}