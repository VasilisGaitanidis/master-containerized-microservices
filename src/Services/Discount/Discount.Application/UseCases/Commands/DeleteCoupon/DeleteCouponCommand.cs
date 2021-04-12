using MediatR;

namespace Discount.Application.UseCases.Commands.DeleteCoupon
{
    /// <summary>
    /// The delete coupon command.
    /// </summary>
    public class DeleteCouponCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteCouponCommand"/>.
        /// </summary>
        /// <param name="productName">The coupon product name.</param>
        public DeleteCouponCommand(string productName)
        {
            ProductName = productName;
        }

        /// <summary>
        /// The coupon product name.
        /// </summary>
        public string ProductName { get; }
    }
}