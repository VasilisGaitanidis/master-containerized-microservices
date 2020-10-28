using System;
using Application.Exceptions;

namespace Catalog.Application.Exceptions
{
    public class CatalogItemNotFoundException : EntityNotFoundException
    {
        public Guid Id { get; }

        public CatalogItemNotFoundException(Guid id)
            : base($"Catalog item with identifier '{id}' not found.")
        {
            Id = id;
        }
        
    }
}