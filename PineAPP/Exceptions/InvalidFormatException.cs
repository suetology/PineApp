namespace PineAPP.Exceptions;

public class InvalidFormatException : Exception
{
    public InvalidFormatException()
    {
    }

    public InvalidFormatException(string message)
        : base(message)
    {
    }

    public InvalidFormatException(string message, Exception innerException)
    {
    }
}