using System;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Test.Framework
{
    public abstract class ElementFinder : IElementFinder
    {
        protected abstract DependencyObject Scope { get; }

        public Task<TElement> FindElementAsync<TElement>(Predicate<TElement> predicate, int timeout = 10000) where TElement : FrameworkElement
        {
            return FrameworkElementFinder.FindAsync<TElement>(this.Scope, predicate, timeout);
        }
    }
}
