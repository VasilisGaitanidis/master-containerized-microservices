using Discount.Application.Dtos.Responses;
using MediatR;

namespace Discount.Application.UseCases.Queries.GetCouponByProductName
{
    /// <summary>
    /// The get coupon by product name query.
    /// </summary>
    public class GetCouponByProductNameQuery : IRequest<CouponDto>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="GetCouponByProductNameQuery"/>.
        /// </summary>
        /// <param name="productName">The coupon product name.</param>
        public GetCouponByProductNameQuery(string productName)
        {
            ProductName = productName;
        }

        /// <summary>
        /// The coupon product name.
        /// </summary>
        public string ProductName { get; }
    }
}