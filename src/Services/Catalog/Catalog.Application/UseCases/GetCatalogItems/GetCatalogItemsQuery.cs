using System.Collections.Generic;
using Catalog.Application.Dtos.Responses;
using MediatR;

namespace Catalog.Application.UseCases.GetCatalogItems
{
    public class GetCatalogItemsQuery : IRequest<IEnumerable<CatalogItemDto>>
    {
    }
}