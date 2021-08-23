using Serilog;
using System;
using System.Windows;

namespace MyExpenses.WpfClient
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // конфигурация логгера
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path: "log.txt", rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message} {NewLine}{Excepion}")
                .CreateLogger();

            // инициализация DI
            DI.Setup();

            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            Log.Debug("Application startup");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.Debug("Application exit");
            Log.CloseAndFlush();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }
    }
}
