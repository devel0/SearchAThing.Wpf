using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SearchAThing.Wpf
{
    public class MathOpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (parameter == null) return false;
            if (value is string && string.IsNullOrEmpty((string)value)) return false;

            var pargs = ((string)parameter).Split(' ');

            try
            {
                var varg1 = System.Convert.ToDouble(value);
                var varg2 = double.Parse((string)pargs[1].Trim());

                switch (pargs[0].ToLower())
                {
                    case "eq": return varg1 == varg2;
                    case "neq": return varg1 != varg2;
                    case "gt": return varg1 > varg2;
                    case "gte": return varg1 >= varg2;
                    case "lt": return varg1 < varg2;
                    case "lte": return varg1 <= varg2;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid number [{value}]");
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
