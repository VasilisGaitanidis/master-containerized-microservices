using System.Collections.Generic;
using Catalog.Application.Dtos;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogItemsQuery : IRequest<IEnumerable<CatalogItemResponseDto>>
    {
        
    }
}