using System;
using Catalog.Api.Application.Dtos.Responses;
using MediatR;

namespace Catalog.Api.Application.Queries
{
    public class GetCatalogItemByIdQuery : IRequest<CatalogItemDto>
    {
        /// <summary>
        /// The catalog item identifier
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id">The catalog item identifier.</param>
        public GetCatalogItemByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}