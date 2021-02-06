using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Test.Framework
{
    public interface IViewProxy : IElementFinder
    {
        Task<IViewProxy> FindViewProxyAsync<TElement>(Predicate<TElement> predicate, int timeout = Wait.DEFAULT_TIMEOUT) where TElement : FrameworkElement;
    }
}
