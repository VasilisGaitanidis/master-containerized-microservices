using FluentValidation;

namespace Discount.Application.UseCases.Queries.GetCouponByProductName
{
    /// <summary>
    /// The get coupon by product name query validator.
    /// </summary>
    public class GetCouponByProductNameQueryValidator : AbstractValidator<GetCouponByProductNameQuery>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="GetCouponByProductNameQueryValidator"/>.
        /// </summary>
        public GetCouponByProductNameQueryValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();
        }
    }
}