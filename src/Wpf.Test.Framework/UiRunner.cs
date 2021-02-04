using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    internal static class UiRunner
    {
        public static void RunOnUi(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public static T RunOnUi<T>(Func<T> func)
        {
            return Application.Current.Dispatcher.Invoke<T>(func);
        }
    }
}
