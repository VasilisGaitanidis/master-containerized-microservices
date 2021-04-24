using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Application.Dtos.Responses;
using Discount.Application.Exceptions;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.UseCases.Commands.UpdateCoupon
{
    /// <summary>
    /// The update coupon command handler.
    /// </summary>
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, CouponDto>
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of a <see cref="UpdateCouponCommandHandler"/>.
        /// </summary>
        /// <param name="couponRepository">The coupon repository.</param>
        /// <param name="mapper">The mapper.</param>
        public UpdateCouponCommandHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<CouponDto> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByProductNameAsync(request.ProductName) ??
                throw new CouponNotFoundException(request.ProductName);

            coupon.ChangeDescription(request.Description);
            coupon.ChangeAmount(request.Amount);

            _couponRepository.Update(coupon);

            return _mapper.Map<CouponDto>(coupon);
        }
    }
}