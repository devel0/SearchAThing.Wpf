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
    public class MathOpConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (parameter == null) return false;
            if (value is string && string.IsNullOrEmpty((string)value)) return false;

            var pargs = ((string)parameter).Split(' ');

            var toVis = pargs.Length > 0 && pargs.Last() == "tovis";

            try
            {
                var varg1 = System.Convert.ToDouble(value);
                var varg2 = double.Parse((string)pargs[1].Trim());
                var resbool = false;

                switch (pargs[0].ToLower())
                {
                    case "eq": resbool = varg1 == varg2; break;
                    case "neq": resbool = varg1 != varg2; break;
                    case "gt": resbool = varg1 > varg2; break;
                    case "gte": resbool = varg1 >= varg2; break;
                    case "lt": resbool = varg1 < varg2; break;
                    case "lte": resbool = varg1 <= varg2; break;
                }

                if (toVis)
                    return resbool ? Visibility.Visible : Visibility.Collapsed;
                else
                    return resbool;
            }
            catch (Exception ex)
            {
                throw new Exception($"invalid number [{value}]");
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
