using System.Collections.Generic;
using Catalog.Api.Application.Dtos;
using MediatR;

namespace Catalog.Api.Application.Queries
{
    public class GetCatalogItemsQuery : IRequest<IEnumerable<CatalogItemResponseDto>>
    {
        
    }
}