using FluentValidation;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    /// <summary>
    /// The update coupon command validator.
    /// </summary>
    public class UpdateCouponCommandValidator : AbstractValidator<UpdateCouponCommand>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="UpdateCouponCommandValidator"/>.
        /// </summary>
        public UpdateCouponCommandValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();

            RuleFor(c => c.Amount)
                .GreaterThan(0);
        }
    }
}