using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomersDesktop.Converters
{
    public class IsCheckedToSortingOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string val && parameter is string p)
            {
                return val.Equals(p, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            var valueAsBool = (bool)value;

            if (parameterString == null || !valueAsBool)
            {
                return "asc";
            }

            return parameterString;
        }
    }
}
