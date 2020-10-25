using System;

namespace Application.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}