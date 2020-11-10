using FluentValidation;

namespace Application.Exceptions
{
    public class ValidationAppException : AppException
    {
        public ValidationAppException(string message)
            : base(message) { }

        public ValidationAppException(string message, ValidationException innerException)
            : base(message, innerException) { }
    }
}