using Microsoft.Extensions.Options;

namespace PineAPP.Exceptions;

[ProviderAlias("FileErrorLogger")]
public class ErrorLoggerProvider : ILoggerProvider
{
    public readonly ErrorLoggerConfigurations Config;
    
    public ErrorLoggerProvider(IOptions<ErrorLoggerConfigurations> config)
    {
        Config = config.Value;
    }
    
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ErrorLogger(this);
    }
}