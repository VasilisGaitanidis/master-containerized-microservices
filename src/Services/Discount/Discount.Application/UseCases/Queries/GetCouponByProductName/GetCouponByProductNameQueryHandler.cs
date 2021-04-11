using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discount.Application.Dtos.Responses;
using Discount.Application.Exceptions;
using Discount.Domain.Repositories;
using MediatR;

namespace Discount.Application.UseCases.Queries.GetCouponByProductName
{
    public class GetCouponByProductNameQueryHandler : IRequestHandler<GetCouponByProductNameQuery, CouponDto>
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public GetCouponByProductNameQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CouponDto> Handle(GetCouponByProductNameQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetByProductNameAsync(request.ProductName) ??
                         throw new CouponNotFoundException(request.ProductName);

            return _mapper.Map<CouponDto>(coupon);
        }
    }
}