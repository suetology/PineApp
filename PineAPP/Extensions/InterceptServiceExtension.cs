using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using PineAPP.Services.Interceptors;

namespace PineAPP.Extensions
{
    public static class InterceptServiceExtension
    {
        public static void AddInterceptedTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService>(serviceProvider =>
            {
                var actualService = ActivatorUtilities.CreateInstance<TImplementation>(serviceProvider);
                
                var interceptor = serviceProvider.GetRequiredService<PerformanceMetricsInterceptor>();
                
                var proxyGenerator = new ProxyGenerator();
                return (TService)proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TService), actualService, interceptor);
            });
        }
    }
}