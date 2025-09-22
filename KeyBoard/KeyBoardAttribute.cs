using System.Windows;
using System.Windows.Media;

namespace KeyBoard
{
    public class KeyBoardAttribute
    {
        public static bool GetShow(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowProperty);
        }

        public static void SetShow(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowProperty, value);
        }

        public static readonly DependencyProperty ShowProperty =
            DependencyProperty.RegisterAttached("Show", typeof(bool), typeof(KeyBoardAttribute), new PropertyMetadata(default));


        public static bool GetQ9(DependencyObject obj)
        {
            return (bool)obj.GetValue(Q9Property);
        }

        public static void SetQ9(DependencyObject obj, bool value)
        {
            obj.SetValue(Q9Property, value);
        }

        public static readonly DependencyProperty Q9Property =
            DependencyProperty.RegisterAttached("Q9", typeof(bool), typeof(KeyBoardAttribute), new PropertyMetadata(default));


        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(KeyBoardAttribute), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

    }
}
