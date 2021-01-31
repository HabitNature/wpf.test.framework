using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    public class WindowProxy : ElementFinder
    {
        public Window Window { get; protected set; }

        protected override DependencyObject Scope => this.Window;

        public WindowProxy(Window window)
        {
            if (null == window)
            {
                throw new ArgumentNullException(nameof(window));
            }

            this.Window = window;
        }
    }
}
