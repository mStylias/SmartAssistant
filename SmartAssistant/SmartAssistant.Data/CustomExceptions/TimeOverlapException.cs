namespace SmartAssistant.Data.CustomExceptions;

public class TimeOverlapException : Exception
{
    public TimeOverlapException()
    {
    }

    public TimeOverlapException(string? message) : base(message)
    {
    }

    public TimeOverlapException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}