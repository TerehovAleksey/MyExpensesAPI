using Microsoft.AspNetCore.Builder;
using MyExpensesAPI.Middlewares;

namespace MyExpensesAPI.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
            return builder;
        }

        public static IApplicationBuilder UseCulture(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CultureMiddleware>();
            return builder;
        }
    }
}
