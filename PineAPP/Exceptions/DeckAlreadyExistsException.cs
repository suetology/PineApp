namespace PineAPP.Exceptions;

public class DeckAlreadyExistsException : Exception
{
    public DeckAlreadyExistsException()
    {
    }

    public DeckAlreadyExistsException(string message)
        : base(message)
    {
    }

    public DeckAlreadyExistsException(string message, Exception innerException)
    {
    }
}