using Microsoft.Extensions.DependencyInjection;

namespace MyExpensesAPI.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCORS(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            return services;
        }
    }
}
