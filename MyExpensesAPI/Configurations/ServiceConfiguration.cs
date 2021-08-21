﻿using Microsoft.Extensions.DependencyInjection;
using MyExpensesAPI.Services.Implementation;
using MyExpensesAPI.Services.Interfaces;

namespace MyExpensesAPI.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //TODO: сервисы тут
            services.AddTransient<ICategoryService, CategoryService>();

            return services;
        }
    }
}
