using System;

namespace Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions.
    /// </summary>
    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message) { }

        protected DomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}