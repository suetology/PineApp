namespace PineAPP.Exceptions;

public class ErrorLogger : ILogger
{
    private readonly ErrorLoggerProvider _errorLoggerProvider;

    public ErrorLogger(ErrorLoggerProvider errorLoggerProvider)
    {
        _errorLoggerProvider = errorLoggerProvider;

        if (!File.Exists(errorLoggerProvider.Config.FileName))
        {
            File.Create(errorLoggerProvider.Config.FileName).Close();
        }
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string logMessage = formatter(state, exception);
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] - {logMessage}";

        File.AppendAllText(_errorLoggerProvider.Config.FileName, logEntry + Environment.NewLine);
    }
}