using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace PineAPP.Exceptions;

public static class ErrorLoggerExtensions
{
    public static ILoggingBuilder AddErrorLogger(this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ErrorLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions<ErrorLoggerConfigurations, ErrorLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddErrorLogger(this ILoggingBuilder builder, Action<ErrorLoggerConfigurations> configure)
    {
        builder.AddErrorLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}