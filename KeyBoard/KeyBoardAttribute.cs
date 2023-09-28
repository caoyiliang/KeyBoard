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



        public static bool GetQ9(DependencyObject obj)
        {
            return (bool)obj.GetValue(Q9Property);
        }

        public static void SetQ9(DependencyObject obj, bool value)
        {
            obj.SetValue(Q9Property, value);
        }

        // Using a DependencyProperty as the backing store for Q9.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Q9Property =
            DependencyProperty.RegisterAttached("Q9", typeof(bool), typeof(KeyBoardAttribute), new PropertyMetadata(default));


    }
}
