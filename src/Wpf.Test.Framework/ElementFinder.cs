using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    public abstract class ElementFinder : IElementFinder
    {
        protected abstract DependencyObject Scope { get; }

        public TElement FindElement<TElement>(Predicate<TElement> predicate, int timeout = 10000) where TElement : FrameworkElement
        {
            return FrameworkElementFinder.Find<TElement>(this.Scope, predicate, timeout);
        }
    }
}
