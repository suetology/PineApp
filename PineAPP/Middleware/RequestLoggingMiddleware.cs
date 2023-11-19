namespace PineAPP.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next, 
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation($"Request received: {httpContext.Request.Method} {httpContext.Request.Path}");
        await _next(httpContext);
        _logger.LogInformation($"Response sent: {httpContext.Response.StatusCode}");
    }
}