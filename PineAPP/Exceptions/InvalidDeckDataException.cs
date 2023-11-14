namespace PineAPP.Exceptions;

public class InvalidDeckDataException : Exception
{
    public InvalidDeckDataException()
    {
    }

    public InvalidDeckDataException(string message)
        : base(message)
    {
    }

    public InvalidDeckDataException(string message, Exception innerException)
    {
    }
}