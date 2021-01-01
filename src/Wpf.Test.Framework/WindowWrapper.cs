using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    public class WindowWrapper : ElementFinder
    {
        public Window Window { get; protected set; }

        protected override DependencyObject Scope => this.Window;

        public WindowWrapper(Window window)
        {
            if (null == window)
            {
                throw new ArgumentNullException(nameof(window));
            }

            this.Window = window;
        }
    }
}
