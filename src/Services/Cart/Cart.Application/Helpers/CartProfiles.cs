using AutoMapper;
using Cart.Application.Dtos.Responses;
using Cart.Domain.Entities;

namespace Cart.Application.Helpers
{
    public class CartProfiles : Profile
    {
        public CartProfiles()
        {
            #region Responses

            CreateMap<ShoppingCartItem, ShoppingCartItemDto>();

            CreateMap<ShoppingCart, ShoppingCartDto>();

            #endregion
        }
    }
}