using System;
using Domain.Exceptions;

namespace Catalog.Domain.Exceptions
{
    /// <inheritdoc />
    public class CatalogDomainException : DomainException
    {
        public CatalogDomainException() { }

        public CatalogDomainException(string message)
            : base(message) { }

        public CatalogDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}