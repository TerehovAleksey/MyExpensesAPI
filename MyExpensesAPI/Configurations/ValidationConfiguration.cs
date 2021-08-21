using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyExpensesAPI.Models.Account;
using MyExpensesAPI.Models.Models.Category;
using MyExpensesAPI.Models.Validation.Account;
using MyExpensesAPI.Models.Validation.Category;

namespace MyExpensesAPI.Configurations
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

            services.AddTransient<IValidator<CategoryApiModel>, CategoryApiModelValidator>();

            return services;
        }
    }
}
