namespace PineAPP.Exceptions;

public class ErrorLogger : ILogger
{
    private readonly string _logFilePath;

    public ErrorLogger(string logFilePath)
    {
        _logFilePath = logFilePath;

        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Close();
        }
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string logMessage = formatter(state, exception);
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] - {logMessage}";

        File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
    }
}