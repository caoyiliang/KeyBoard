using System;
using System.Windows;
using System.Windows.Controls;

namespace KeyBoard
{
    /// <summary>
    /// Qwerty.xaml 的交互逻辑
    /// </summary>
    public partial class Qwerty : UserControl
    {
        static double w = 710;
        static double h = 220;
        public Qwerty()
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
