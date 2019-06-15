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

    public class SciTextBox : TextBox
    {

        static KeyConverter kc = new KeyConverter();

        public SciTextBox()
        {
            TextAlignment = TextAlignment.Right;
        }

        public void BeginEdit(RoutedEventArgs e)
        {
            if (e is KeyEventArgs)
            {
                var ke = (KeyEventArgs)e;

                var ch = kc.ConvertToInvariantString(ke.Key);

                Text = ch.ToString();

                Focus();
            }
        }

        #region Value [dppc]
        public static readonly DependencyProperty ValueProperty =
          DependencyProperty.Register("Value", typeof(Measure), typeof(SciTextBox),
              new FrameworkPropertyMetadata(null, OnValueChanged));

        public Measure Value
        {
            get
            {
                return (Measure)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        static void OnValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var obj = (SciTextBox)source;

            if (obj.Value != null)
            {
                if (obj.Value.MU.Equals(MUCollection.Adimensional.adim))
                {
                    var str = obj.Value.ToString(includePQ: false);

                    obj.Text = str;
                }
                else
                    obj.Text = obj.Value.ToString();
            }
            else
                obj.Text = "";
        }
        #endregion

        static Brush RedBrush = new SolidColorBrush(Colors.Red);

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            ParseText(Text);
        }

        void ParseText(string text)
        {
            if (Value == null) return;

            var measure = Sci.Measure.TryParse(text, Value.MU.PhysicalQuantity);

            if (measure == null)
            {
                var tval = text;
                if (Value.MU != MUCollection.Adimensional.adim)
                    tval = text + Value.MU.ToString();
                measure = Sci.Measure.TryParse(text + Value.MU.ToString(), Value.MU.PhysicalQuantity);
            }

            if (measure != null)
            {
                var changed = !Value.ConvertTo(measure.MU).Value.EqualsAutoTol(measure.Value);
                if (changed || Foreground == RedBrush)
                {
                    var curs = CaretIndex;
                    var cursBefore = curs;

                    var len_before = Text.Length;
                    if (changed)
                    {
                        var containsSpace = Text.IndexOf(' ') != -1;
                        Value = measure;

                        if (cursBefore == len_before && len_before == 1)
                        {
                            // ensure focus after number ( ex. select all and digit a number )                            
                            //if (containsSpace)
                            var sidx = Text.IndexOf(' ');
                            if (sidx != -1)
                                CaretIndex = sidx;
                            else
                                CaretIndex = Text.Length;
                        }
                        else
                        {
                            var len_after = Text.Length;
                            if (measure.MU == MUCollection.Adimensional.adim)
                            {
                                CaretIndex = curs + (len_after - len_before);
                            }
                            else
                                CaretIndex = curs + (len_after - len_before - (containsSpace ? 0 : 1));
                        }
                    }

                    Foreground = (Brush)ForegroundProperty.DefaultMetadata.DefaultValue;
                }
            }
            else
                Foreground = RedBrush;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            SelectAll();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            var str = Value.ToString(includePQ: false);
            if (Text != str)
            {
                Text = str; // ensure mu displayed
            }
        }
    }

}
