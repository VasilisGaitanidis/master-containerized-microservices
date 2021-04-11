using System;
using Domain.Exceptions;

namespace Discount.Domain.Exceptions
{
    /// <inheritdoc />
    public class CouponDomainException : DomainException
    {
        /// <inheritdoc />
        public CouponDomainException(string message)
            : base(message) { }

        /// <inheritdoc />
        public CouponDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    {
        
    }
}