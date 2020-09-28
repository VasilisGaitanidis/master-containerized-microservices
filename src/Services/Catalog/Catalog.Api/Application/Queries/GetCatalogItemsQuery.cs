using System.Collections.Generic;
using Catalog.Api.Application.Dtos.Responses;
using MediatR;

namespace Catalog.Api.Application.Queries
{
    public class GetCatalogItemsQuery : IRequest<IEnumerable<CatalogItemDto>>
    {
    }
}