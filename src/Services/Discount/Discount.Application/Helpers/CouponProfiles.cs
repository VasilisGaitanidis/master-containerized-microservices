using AutoMapper;
using Discount.Application.Dtos.Responses;
using Discount.Domain.Entities;

namespace Discount.Application.Helpers
{
    public class CouponProfiles : Profile
    {
        public CouponProfiles()
        {
            CreateMap<Coupon, CouponDto>();
        }
    }
}