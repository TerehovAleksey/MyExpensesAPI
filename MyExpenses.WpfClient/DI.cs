using Autofac;
using AutofacSerilogIntegration;
using Serilog;

namespace MyExpenses.WpfClient
{
    public static class DI
    {
        public static IContainer Container { get; private set; }

        public static void Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterLogger(autowireProperties: true);

            //TODO: тут регистрации

            Container = builder.Build();
        }

        public static T Get<T>() => Container.Resolve<T>();

        /// <summary>
        /// Логгер
        /// </summary>
        public static ILogger Logger => Get<ILogger>();
    }
}
