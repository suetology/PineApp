namespace PineAPP.Exceptions;

public class InvalidUserDataException : Exception
{
    public InvalidUserDataException()
    {
    }

    public InvalidUserDataException(string message)
        : base(message)
    {
    }

    public InvalidUserDataException(string message, Exception innerException)
    {
    }
}