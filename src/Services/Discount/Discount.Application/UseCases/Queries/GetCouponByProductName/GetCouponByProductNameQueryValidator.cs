using FluentValidation;

namespace Discount.Application.UseCases.Queries.GetCouponByProductName
{
    public class GetCouponByProductNameQueryValidator : AbstractValidator<GetCouponByProductNameQuery>
    {
        public GetCouponByProductNameQueryValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty();
        }
    }
}