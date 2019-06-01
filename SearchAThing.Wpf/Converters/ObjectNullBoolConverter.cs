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
    /// return false if value null, true otherwise.
    /// If parameter == true inverted behavior
    /// </summary>
    public class ObjectNullBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var invertedMode = false;

            if (parameter != null) invertedMode = System.Convert.ToBoolean(parameter);

            if (invertedMode) // inverted mode
            {
                if (value == null) return true;
                return false;
            }
            else
            {
                if (value == null) return false;
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
