using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace KeyBoard
{
    public class KeyboardBehavior : Behavior<Control>
    {
        public bool Show
        {
            get { return (bool)GetValue(ShowProperty); }
            set { SetValue(ShowProperty, value); }
        }

        public static readonly DependencyProperty ShowProperty =
            DependencyProperty.Register("Show", typeof(bool), typeof(KeyboardBehavior), new PropertyMetadata(default));

        public bool Q9
        {
            get { return (bool)GetValue(Q9Property); }
            set { SetValue(Q9Property, value); }
        }

        public static readonly DependencyProperty Q9Property =
            DependencyProperty.Register("Q9", typeof(bool), typeof(KeyboardBehavior), new PropertyMetadata(default));

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(KeyboardBehavior), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        /// <summary>
        /// 触发键盘的控件所在的容器
        /// </summary>
        public Panel? Panel { get; set; }

        Popup popup = new();

        public KeyboardBehavior()
        {
            popup.AllowsTransparency = true;
            popup.AllowDrop = false;
        }

        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
            base.OnDetaching();
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (popup != null)
            {
                popup.IsOpen = false;
            }
            Panel?.Children.Remove(popup);
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Show)
            {
                Panel = GetPanel(AssociatedObject);
                if (Panel == null)
                {
                    return;
                }
                if (VisualTreeHelper.GetParent(popup) != null)
                {
                    return;
                }
                Panel.Children.Add(popup);
                if (Q9)
                {
                    var keyboard = new Q9();
                    ((KbViewModel)keyboard.DataContext).Background = Background;
                    keyboard.ClosedEvent += Keyboard_ClosedEvent;
                    popup.Child = keyboard;
                }
                else
                {
                    var keyboard = new Qwerty();
                    ((KbViewModel)keyboard.DataContext).Background = Background;
                    keyboard.ClosedEvent += Keyboard_ClosedEvent;
                    popup.Child = keyboard;
                }
                popup.IsOpen = true;
                popup.StaysOpen = true;
                popup.Placement = PlacementMode.Bottom;
                popup.PlacementTarget = AssociatedObject;
            }
        }

        private void Keyboard_ClosedEvent(object? sender, EventArgs e)
        {
            if (popup != null)
            {
                popup.IsOpen = false;
            }
            Panel?.Children.Remove(popup);
        }

        private Panel? GetPanel(DependencyObject dependencyObject)
        {
            dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            return dependencyObject switch
            {
                null => null,
                Panel pl => pl,
                _ => GetPanel(dependencyObject)
            };
        }
    }
}
