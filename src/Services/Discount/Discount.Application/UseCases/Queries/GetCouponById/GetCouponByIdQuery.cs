using Discount.Application.Dtos.Responses;
using MediatR;

namespace Discount.Application.UseCases.Queries.GetCouponById
{
    public class GetCouponByIdQuery : IRequest<CouponDto>
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id">The coupon identifier.</param>
        public GetCouponByIdQuery(int id)
        {
            Id = id;
        }

        /// <summary>
        /// The coupon identifier.
        /// </summary>
        public int Id { get; }
    }
}