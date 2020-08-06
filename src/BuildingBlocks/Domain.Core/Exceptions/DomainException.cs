using System;

namespace Domain.Core.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions.
    /// </summary>
    public abstract class DomainException : Exception
    {
        protected DomainException() { }

        protected DomainException(string message)
            : base(message) { }

        protected DomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}