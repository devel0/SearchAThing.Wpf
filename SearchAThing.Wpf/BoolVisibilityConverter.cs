using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SearchAThing.Wpf
{
    public class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var inverted = parameter != null && System.Convert.ToBoolean(parameter);

            if (inverted)
            {
                if (System.Convert.ToBoolean(value))
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }

            if (System.Convert.ToBoolean(value))
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
