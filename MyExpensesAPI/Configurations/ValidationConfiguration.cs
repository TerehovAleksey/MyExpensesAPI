﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyExpensesAPI.Models.User;
using MyExpensesAPI.Models.Models.Category;
using MyExpensesAPI.Models.Models.Currency;
using MyExpensesAPI.Models.Validation.User;
using MyExpensesAPI.Models.Validation.Category;
using MyExpensesAPI.Models.Validation.Currency;

namespace MyExpensesAPI.Configurations
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

            services.AddTransient<IValidator<CategoryApiModel>, CategoryApiModelValidator>();
            services.AddTransient<IValidator<CurrencyCreateApiModel>, CurrencyCreateApiModelValidator>();

            return services;
        }
    }
}
