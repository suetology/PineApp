namespace PineAPP.Exceptions;

public class ErrorLogger : ILogger
{
    private readonly ErrorLoggerProvider _errorLoggerProvider;

    public ErrorLogger(ErrorLoggerProvider errorLoggerProvider)
    {
        _errorLoggerProvider = errorLoggerProvider;
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
        
        Thread thread = new Thread(new ThreadStart( () =>
        {
            string logMessage = formatter(state, exception);
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] - {logMessage}";
            
            try
            {
                Monitor.Enter(_errorLoggerProvider.Writer);
                _errorLoggerProvider.Writer.WriteLine(logEntry);
                _errorLoggerProvider.Writer.Flush();
            }
            finally
            {
                Monitor.Exit(_errorLoggerProvider.Writer);
            }
        }));
        thread.Start();
    }
}