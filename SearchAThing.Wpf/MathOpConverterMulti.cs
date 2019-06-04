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
    /*
    * example:
    * 
    * follow enable the Run textblock only if
    * - the project results a not-null object
    * - AND
    * - the IsRunning property is not true
    * 
    * <TextBlock Text="Run" Style="{DynamicResource HyperlinkTextBlk}" MouseLeftButtonDown="runProject_click">
    *   <TextBlock.IsEnabled>
    *     <MultiBinding Converter="{StaticResource MathOpConverterMulti}" ConverterParameter="and">
    *       <Binding Path="CurrentProject" ConverterParameter="false" Converter="{StaticResource ObjectNullBoolConverter}" ElementName="window"/>
    *       <Binding Path="IsRunning" ConverterParameter="false" Converter="{StaticResource BoolInvertConverter}" ElementName="window"/>
    *     </MultiBinding>
    *   </TextBlock.IsEnabled>
    * </TextBlock>
    */
    public class MathOpConverterMulti : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return false;
            if (parameter == null) return false;

            if (values.Any(r => r == DependencyProperty.UnsetValue)) return false;

            var pars = ((string)parameter).ToLower().Split(' ');

            var vi = 0;
            bool res = true;

            foreach (var p in pars)
            {
                switch (p)
                {
                    case "istrue":
                        {
                            res = res && (bool)values[vi];
                            ++vi;
                        }
                        break;

                    case "isfalse":
                        {
                            res = res && !(bool)values[vi];
                            ++vi;
                        }
                        break;

                    case "and":
                        {
                            res = res && ((bool)values[vi] && (bool)values[vi + 1]);
                            vi += 2;
                        }
                        break;

                    case "or":
                        {
                            res = res && ((bool)values[vi] || (bool)values[vi + 1]);
                            vi += 2;
                        }
                        break;

                    case "eq":
                        {
                            if (values[vi] == null || values[vi + 1] == null)
                                res = false;
                            else
                                res = res && (values[vi].Equals(values[vi + 1]));
                            vi += 2;
                        }
                        break;

                    case "neq":
                        {
                            if (values[vi] == null || values[vi + 1] == null)
                                res = true;
                            else
                                res = res && !(values[vi].Equals(values[vi + 1]));
                            vi += 2;
                        }
                        break;

                    case "gt":
                        {
                            res = res && (System.Convert.ToDouble(values[vi]) > System.Convert.ToDouble(values[vi + 1]));
                            vi += 2;
                        }
                        break;

                    case "gte":
                        {
                            res = res && (System.Convert.ToDouble(values[vi]) >= System.Convert.ToDouble(values[vi + 1]));
                            vi += 2;
                        }
                        break;

                    case "lt":
                        {
                            res = res && (System.Convert.ToDouble(values[vi]) < System.Convert.ToDouble(values[vi + 1]));
                            vi += 2;
                        }
                        break;

                    case "lte":
                        {
                            res = res && (System.Convert.ToDouble(values[vi]) <= System.Convert.ToDouble(values[vi + 1]));
                            vi += 2;
                        }
                        break;

                }

            }

            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
