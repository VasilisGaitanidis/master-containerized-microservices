using FluentValidation;

namespace Discount.Application.UseCases.Commands.CreateCoupon
{
    /// <summary>
    /// The create coupon command validator.
    /// </summary>
    public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="CreateCouponCommandValidator"/>.
        /// </summary>
        public CreateCouponCommandValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();

            RuleFor(c => c.Amount)
                .GreaterThan(0);
        }
    }
}