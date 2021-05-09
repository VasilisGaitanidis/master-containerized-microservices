using System;
using Domain.Exceptions;

namespace Cart.Domain.Exceptions
{
    /// <inheritdoc />
    public class CartDomainException : DomainException
    {
        public CartDomainException(string message)
            : base(message) { }

        public CartDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}