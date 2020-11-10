namespace Application.Exceptions
{
    public abstract class EntityNotFoundException : AppException
    {
        protected EntityNotFoundException(string message)
            : base(message) { }
    }
}