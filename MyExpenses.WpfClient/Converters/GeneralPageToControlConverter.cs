using MyExpenses.WpfClient.Views;
using System;
using System.Globalization;

namespace MyExpenses.WpfClient.Converters
{
    public sealed class GeneralPageToControlConverter : BaseValueConverter<GeneralPageToControlConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var page = (GeneralPageType)value;
            return page switch
            {
                GeneralPageType.Login => new LoginView(),
                GeneralPageType.Dashboard => new DashboardView(),
                GeneralPageType.Settings => new SettingsView(),
                _ => new LoginView(),
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
