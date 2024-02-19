using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AAD.ImmoWin.WpfApp.Converters
{
    public class ComboBoxItemToIntegerConverter : IValueConverter
    {
        private object ConvertToInteger(object value)
        {
            if (value is ComboBoxItem comboBoxItem)
            {
                return System.Convert.ToInt32(comboBoxItem.Tag);
            }

            return value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToInteger(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToInteger(value);
        }
    }
}
