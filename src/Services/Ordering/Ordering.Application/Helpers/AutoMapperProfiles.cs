using AutoMapper;
using Ordering.Application.Dtos.Responses;
using Ordering.Domain.Entities;

namespace Ordering.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Responses

            CreateMap<Order, OrderDto>();

            CreateMap<Buyer, BuyerDto>();

            CreateMap<OrderItem, OrderItemDto>();

            #endregion
        }
    }
}