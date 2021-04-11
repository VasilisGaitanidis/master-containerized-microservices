using MediatR;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    /// <summary>
    /// The update coupon command.
    /// </summary>
    public class UpdateCouponCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UpdateCouponCommand"/>.
        /// </summary>
        /// <param name="productName">The coupon product name.</param>
        /// <param name="description">The coupon description.</param>
        /// <param name="amount">The coupon amount.</param>
        public UpdateCouponCommand(string productName, string description, int amount)
        {
            ProductName = productName;
            Description = description;
            Amount = amount;
        }

        /// <summary>
        /// The coupon product name.
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// The coupon description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The coupon amount.
        /// </summary>
        public int Amount { get; }
    }
}