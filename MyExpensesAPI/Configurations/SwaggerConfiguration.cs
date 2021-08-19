using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MyExpensesAPI.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "MyExpensesAPI", 
                    Version = "v1" 
                });
            });

            return services;
        }
    }
}
