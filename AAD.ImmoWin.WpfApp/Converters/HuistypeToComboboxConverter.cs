using AAD.ImmoWin.Business;
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
    public class HuistypeToComboboxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Huistype)
            {
                return (int)value;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ComboBoxItem comboBoxItem && comboBoxItem.Tag is string tagValue && int.TryParse(tagValue, out int intValue))
            {
                if (intValue >= 0 && intValue <= Enum.GetValues(typeof(Huistype)).Length)
                {
                    return (Huistype)intValue;
                }
            }
            return null;
        }
    }
}
