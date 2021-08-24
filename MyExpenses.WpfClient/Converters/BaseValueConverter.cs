using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MyExpenses.WpfClient.Converters
{
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        private static T converter = null;

        #region Markup Extensions Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ??= new T();
        }

        #endregion

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
