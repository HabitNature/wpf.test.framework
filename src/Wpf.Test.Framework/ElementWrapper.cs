using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    public class ElementWrapper : ElementFinder
    {
        public FrameworkElement Element { get; private set; }

        protected override DependencyObject Scope => this.Element;

        public ElementWrapper(FrameworkElement element)
        {
            if (null == element)
            {
                throw new ArgumentNullException(nameof(element));
            }

            this.Element = element;
        }
    }
}
