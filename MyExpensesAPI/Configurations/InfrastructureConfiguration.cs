using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using MyExpensesAPI.Services;

namespace MyExpensesAPI.Configurations
{
    public static class InfrastructureConfiguration
    {
        /// <summary>
        /// Регистрация служебных сервисов (mail, logger, etc.)
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
