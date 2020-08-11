using AutoMapper;
using Catalog.Application.Dtos;
using Catalog.Domain.Models;

namespace Catalog.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CatalogItem, CatalogItemResponseDto>();
        }
    }
}