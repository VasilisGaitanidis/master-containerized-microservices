using AutoMapper;
using Discount.Application.Dtos.Responses;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Helpers
{
    public class CouponProfiles : Profile
    {
        public CouponProfiles()
        {
            CreateMap<CouponDto, CouponResponse>();
        }
    }
}