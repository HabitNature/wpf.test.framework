using System;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Test.Framework
{
    public class ViewProxy : ElementFinder, IViewProxy
    {
        public FrameworkElement Element { get; private set; }

        protected override DependencyObject Scope => this.Element;

        public ViewProxy(FrameworkElement element)
        {
            if (null == element)
            {
                throw new ArgumentNullException(nameof(element));
            }

            this.Element = element;
        }

        public Task<IViewProxy> FindViewProxyAsync<TElement>(Predicate<TElement> predicate, int timeout = 10000) where TElement : FrameworkElement
        {
            return this.FindElementAsync<TElement>(predicate, timeout)
                .ContinueWith<IViewProxy>(task =>
                {
                    TElement element = task.Result;

                    return new ViewProxy(element);
                });
        }
    }
}
