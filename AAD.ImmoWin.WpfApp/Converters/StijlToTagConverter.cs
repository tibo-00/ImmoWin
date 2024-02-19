using System;
using System.Globalization;
using System.Windows.Data;

namespace AAD.ImmoWin.WpfApp.Converters
{
    public class StijlToTagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue + 1;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue - 1; 
            }
            return null;
        }
    }
}
