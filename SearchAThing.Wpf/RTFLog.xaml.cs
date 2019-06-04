using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchAThing.Wpf
{
    /// <summary>
    /// Interaction logic for RTFLog.xaml
    /// </summary>
    public partial class RTFLog : UserControl
    {
        public RTFLog()
        {
            InitializeComponent();

            doc = new FlowDocument();            
            rtf.Document = doc;
        }

        #region AutoScroll
        public static readonly DependencyProperty AutoScrollProperty =
            DependencyProperty.Register("ÄutoScroll",
                typeof(bool), typeof(RTFLog), new FrameworkPropertyMetadata(true));

        public bool AutoScroll
        {
            get { return (bool)GetValue(AutoScrollProperty); }
            set { SetValue(AutoScrollProperty, value); }
        }
        #endregion

        FlowDocument doc;
        Paragraph para;

        Brush black = new SolidColorBrush(Colors.Black);
        Brush darkgreen = new SolidColorBrush(Colors.DarkGreen);
        Brush darkorange = new SolidColorBrush(Colors.DarkOrange);
        Brush gray = new SolidColorBrush(Colors.Gray);
        Brush red = new SolidColorBrush(Colors.Red);

        public enum LogColor
        {
            trace,
            normal,
            warning,
            error,
            success
        }

        /// <summary>
        /// clear log content
        /// </summary>
        public void Clear()
        {
            doc.Blocks.Clear();
        }

        /// <summary>
        /// append log text
        /// </summary>        
        public void Append(string msg, bool newline = false, LogColor color = LogColor.normal)
        {
            Brush brush = black;
            if (color != LogColor.normal)
            {
                switch (color)
                {
                    case LogColor.trace: brush = gray; break;
                    case LogColor.warning: brush = darkorange; break;
                    case LogColor.error: brush = red; break;
                    case LogColor.success: brush = darkgreen; break;
                }
            }

            Append(msg, newline, brush);
        }

        public void Append(string msg, bool newline = false, Brush color = null)
        {
            var run = new Run() { Text = msg };

            run.Foreground = color;

            if (para == null)
            {
                para = new Paragraph(run) { Margin = new Thickness(0) };
                doc.Blocks.Add(para);
            }
            else
                para.Inlines.Add(run);

            if (newline) para = null;
            if (AutoScroll) rtf.ScrollToEnd();
        }

        public void Append(string msg)
        {
            Append(msg, false, LogColor.normal);
        }

        public void AppendLine(string msg)
        {
            Append(msg, true, LogColor.normal);
        }

    }
}
