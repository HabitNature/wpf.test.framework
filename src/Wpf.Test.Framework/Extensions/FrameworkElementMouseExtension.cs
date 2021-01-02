using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Wpf.Test.Framework.Extensions
{
    public static class FrameworkElementMouseExtension
    {
        public static void Click(this FrameworkElement element)
        {
            if(element is ButtonBase)
            {
                element.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            else if(element is MenuItem)
            {
                element.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            }
            else
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
                args.RoutedEvent = UIElement.MouseLeftButtonDownEvent;
                args.Source = element;
                element.RaiseEvent(args);

                element.RaiseEvent(MouseButton.Left, FrameworkElement.MouseLeftButtonDownEvent);
                element.RaiseEvent(MouseButton.Left, FrameworkElement.MouseLeftButtonUpEvent);
            }
        }

        public static void DoubleClick(this FrameworkElement element)
        {
            if(element is Control)
            {
                element.RaiseEvent(new RoutedEventArgs(Control.MouseDoubleClickEvent));
            }
            else
            {
                Click(element);
                Thread.Sleep(50);
                Click(element);
            }
        }

        public static void RightClick(this FrameworkElement element)
        {
            element.RaiseEvent(MouseButton.Right, FrameworkElement.MouseRightButtonDownEvent);
            element.RaiseEvent(MouseButton.Right, FrameworkElement.MouseRightButtonUpEvent);
        }

        public static void Hover(this FrameworkElement element)
        {
            element.RaiseEvent(MouseButton.Left, FrameworkElement.MouseEnterEvent);
            element.RaiseEvent(MouseButton.Left, FrameworkElement.MouseMoveEvent);
        }

        public static void RaiseEvent(this FrameworkElement element, MouseButton mouseButton, RoutedEvent routedEvent)
        {
            MouseButtonEventArgs args = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            args.RoutedEvent = UIElement.MouseLeftButtonDownEvent;
            args.Source = element;
            element.RaiseEvent(args);
        }
    }
}
