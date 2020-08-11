using AutoMapper;
using Catalog.Api.Application.Dtos;
using Catalog.Domain.Models;

namespace Catalog.Api.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CatalogItem, CatalogItemResponseDto>();
        }
    }
}