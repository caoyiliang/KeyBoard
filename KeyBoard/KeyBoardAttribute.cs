using System.Windows;

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
    }
}
