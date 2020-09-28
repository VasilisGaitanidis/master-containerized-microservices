using AutoMapper;
using Catalog.Api.Application.Dtos.Responses;
using Catalog.Domain.Models;

namespace Catalog.Api.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Responses

            CreateMap<CatalogItem, CatalogItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom("_name"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom("_description"))
                .ForMember(dest => dest.Price, opt => opt.MapFrom("_price"))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom("_stock"));

            CreateMap<CatalogType, CatalogTypeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom("_name"));

            #endregion
        }
    }
}