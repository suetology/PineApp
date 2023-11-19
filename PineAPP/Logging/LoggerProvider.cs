using Microsoft.Extensions.Options;

namespace PineAPP.Exceptions;

[ProviderAlias("FileErrorLogger")]
public class LoggerProvider : ILoggerProvider
{
    public readonly LoggerConfigurations Config;
    public readonly StreamWriter Writer;
    
    public LoggerProvider(IOptions<LoggerConfigurations> config, IWebHostEnvironment hostingEnvironment)
    {
        Config = config.Value;
        Config.FileName = Path.Combine(hostingEnvironment.ContentRootPath, Config.FileName);

        Writer = new StreamWriter(Config.FileName, true);
    }
    
    public void Dispose()
    {
        Writer.Flush();
        Writer.Close();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new Logger(this);
    }
}