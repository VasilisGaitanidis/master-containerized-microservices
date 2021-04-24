using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Application.Dtos.Responses;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.UseCases.Commands.CreateCoupon
{
    /// <summary>
    /// The create coupon command handler.
    /// </summary>
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, CouponDto>
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of a <see cref="CreateCouponCommandHandler"/>.
        /// </summary>
        /// <param name="couponRepository">The coupon repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CreateCouponCommandHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<CouponDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = new Coupon(request.ProductName, request.Description, request.Amount);

            await _couponRepository.AddAsync(coupon);

            return _mapper.Map<CouponDto>(coupon);
        }
    }
}