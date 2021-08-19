using Microsoft.Extensions.DependencyInjection;

namespace MyExpensesAPI.Configurations
{
    public static class CacheConfiguration
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();
            return services;
        }
    }
}
