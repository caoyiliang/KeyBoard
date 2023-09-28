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

namespace KeyBoard
{
    /// <summary>
    /// Q9.xaml 的交互逻辑
    /// </summary>
    public partial class Q9 : UserControl
    {
        static double w = 300;
        static double h = 180;
        public Q9()
        {
            InitializeComponent();
            Width = w;
            Height = h;
        }

        /// <summary>
        /// 键盘关闭事件
        /// </summary>
        public event EventHandler? ClosedEvent;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ClosedEvent?.Invoke(this, new EventArgs());
        }

        private void resizeThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;
            double newHeight = this.Height + e.VerticalChange;

            // Ensure the UserControl doesn't become too small
            if (newWidth >= this.MinWidth)
                this.Width = newWidth;

            if (newHeight >= this.MinHeight)
                this.Height = newHeight;
            w = Width;
            h = Height;
        }
    }
}
