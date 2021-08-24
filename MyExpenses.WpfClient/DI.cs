using Autofac;
using AutofacSerilogIntegration;
using MyExpenses.ClientCore.Helpers;
using MyExpenses.ClientCore.Repository.Implementations;
using MyExpenses.ClientCore.Repository.Interfaces;
using MyExpenses.WpfClient.Services;
using MyExpenses.WpfClient.ViewModels;
using Serilog;

namespace MyExpenses.WpfClient
{
    public static class DI
    {
        public static IContainer Container { get; private set; }

        public static void Setup()
        {
            var builder = new ContainerBuilder();

            // логгер
            builder.RegisterLogger(autowireProperties: true);
            // информация о сборке
            builder.RegisterType<AssemblyService>().As<IAssemblyService>().SingleInstance();
            // настройки приложения
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            // данные для http-клиента
            builder.RegisterType<HttpInitData>().As<IHttpInitData>();
            // сервисы
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<UserService>().As<IUserService>();
            // приложение
            builder.RegisterType<ApplicationService>().As<IApplicationService>().SingleInstance();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<DashboardViewModel>();

            Container = builder.Build();
        }

        public static T Get<T>() => Container.Resolve<T>();

        /// <summary>
        /// Логгер
        /// </summary>
        public static ILogger Logger => Get<ILogger>();

        /// <summary>
        /// Настройки приложения
        /// </summary>
        public static ISettingsService Settings => Get<ISettingsService>();

        /// <summary>
        /// Основное окно приложения
        /// </summary>
        public static IApplicationService Application => Get<IApplicationService>();
    }
}
