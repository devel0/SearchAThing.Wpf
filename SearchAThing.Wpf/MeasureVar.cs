using SearchAThing.Sci;
using SearchAThing.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SearchAThing.Wpf
{

    public class MeasureVar : DependencyObject
    {

        #region Measure [dppc]
        public static readonly DependencyProperty MeasureProperty =
          DependencyProperty.Register("Measure", typeof(Measure), typeof(MeasureVar), new FrameworkPropertyMetadata(null, OnMeasureChanged));

        public Measure Measure
        {
            get
            {
                return (Measure)GetValue(MeasureProperty);
            }
            set
            {
                SetValue(MeasureProperty, value);
            }
        }

        static void OnMeasureChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MeasureVar)source;
        }
        #endregion

        public MeasureVar(Measure _measure)
        {
            Measure = _measure;
        }

    }

}
