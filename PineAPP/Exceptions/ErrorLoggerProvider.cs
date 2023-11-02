using Microsoft.Extensions.Options;

namespace PineAPP.Exceptions;

[ProviderAlias("FileErrorLogger")]
public class ErrorLoggerProvider : ILoggerProvider
{
    public readonly ErrorLoggerConfigurations Config;
    public readonly StreamWriter Writer;
    
    public ErrorLoggerProvider(IOptions<ErrorLoggerConfigurations> config, IWebHostEnvironment hostingEnvironment)
    {
        Config = config.Value;
        Config.FileName = Path.Combine(hostingEnvironment.ContentRootPath, Config.FileName);

        Writer = new StreamWriter(Config.FileName, true);
    }
    
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ErrorLogger(this);
    }
}