using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    public class ElementProxy : ElementFinder
    {
        public FrameworkElement Element { get; private set; }

        protected override DependencyObject Scope => this.Element;

        public ElementProxy(FrameworkElement element)
        {
            if (null == element)
            {
                throw new ArgumentNullException(nameof(element));
            }

            this.Element = element;
        }
    }
}
