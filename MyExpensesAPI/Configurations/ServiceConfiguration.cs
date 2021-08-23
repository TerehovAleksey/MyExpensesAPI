using Microsoft.Extensions.DependencyInjection;
using MyExpensesAPI.Services.Implementation;
using MyExpensesAPI.Services.Interfaces;

namespace MyExpensesAPI.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IJournalService, JournalService>();

            return services;
        }
    }
}
