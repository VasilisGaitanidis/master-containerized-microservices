namespace Discount.Application.Dtos.Responses
{
    public class CouponDto
    {
        /// <summary>
        /// The coupon identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The coupon product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The coupon description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The coupon amount.
        /// </summary>
        public int Amount { get; set; }
    }
}