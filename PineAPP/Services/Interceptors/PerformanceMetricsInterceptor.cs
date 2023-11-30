using System.Diagnostics;
using Castle.DynamicProxy;

namespace PineAPP.Services.Interceptors;

public class PerformanceMetricsInterceptor : IInterceptor
{
    private readonly ILogger<PerformanceMetricsInterceptor> _logger;

    public PerformanceMetricsInterceptor(ILogger<PerformanceMetricsInterceptor> logger)
    {
        _logger = logger;
    }
    
    public void Intercept(IInvocation invocation)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            invocation.Proceed();
        }
        finally
        {
            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;
            _logger.LogInformation($"Execution of {invocation.Method.Name} took {elapsedTime} ms");
        }
    }
}