using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyExpensesAPI.Models.Account;
using MyExpensesAPI.Models.Validation.Account;

namespace MyExpensesAPI.Configurations
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

            return services;
        }
    }
}
