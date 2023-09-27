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



        /// <summary>
        /// 触发键盘的控件所在的容器
        /// </summary>
        public Panel? Panel { get; set; }

        Popup popup = new();

        public KeyboardBehavior()
        {
            popup.AllowsTransparency = true;
            popup.AllowDrop = true;
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
                var keyboard = new Qwerty();
                keyboard.ClosedEvent += Keyboard_ClosedEvent;
                popup.Child = keyboard;
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
