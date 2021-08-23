using Serilog;
using System;
using System.IO;
using System.Windows;

namespace MyExpenses.WpfClient
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // инициализация DI
            DI.Setup();

            // настройки
            DI.Settings.Load();

            // конфигурация логгера
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(path: Path.Combine(DI.Settings.GetLogsFolderLocation(), "log.txt"), rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message} {NewLine}{Excepion}")
                .CreateLogger();

            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            Log.Debug("Application startup");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (DI.Settings.Current.General_FirstRun)
            {
                DI.Settings.Current.General_FirstRun = false;
            }
            if (DI.Settings.Current.IsSettingsChanged)
            {
                DI.Settings.Save();
            }

            Log.Debug("Application exit");
            Log.CloseAndFlush();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            DI.Logger.Error(e.Exception.StackTrace);

            e.Handled = true;
        }
    }
}
