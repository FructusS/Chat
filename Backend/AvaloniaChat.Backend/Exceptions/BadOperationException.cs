namespace Application.Exceptions;

public class BadOperationException : Exception
{
    public BadOperationException()
    {
    }

    public BadOperationException(string? message) : base(message)
    {
    }

    public BadOperationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}