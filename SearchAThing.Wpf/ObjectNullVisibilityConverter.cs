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
    /// <summary>
    /// return Collapsed if value null, true otherwise.
    /// If parameter == true inverted behavior
    /// </summary>
    public class ObjectNullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var invertedMode = false;

            if (parameter != null) invertedMode = System.Convert.ToBoolean(parameter);

            if (invertedMode) // inverted mode
            {
                if (value == null)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
            {
                if (value == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
