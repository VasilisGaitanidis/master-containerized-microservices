using System;
using Domain.Exceptions;

namespace Ordering.Domain.Exceptions
{
    /// <inheritdoc />
    public class OrderingDomainException : DomainException
    {
        public OrderingDomainException(string message)
            : base(message) { }

        public OrderingDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}