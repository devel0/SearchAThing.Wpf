using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SearchAThing.Wpf
{
    /// <summary>
    /// return true if string is nullorempty
    /// If parameter == true inverted behavior
    /// </summary>
    public class StringNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var invertedMode = false;

            if (parameter != null) invertedMode = System.Convert.ToBoolean(parameter);

            if (invertedMode) // inverted mode
            {
                return !string.IsNullOrEmpty((string)value);
            }
            else
            {
                return string.IsNullOrEmpty((string)value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
