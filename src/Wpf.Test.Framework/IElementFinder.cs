using System;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Test.Framework
{
    public interface IElementFinder
    {
        Task<TElement> FindElementAsync<TElement>(Predicate<TElement> predicate, int timeout = Wait.DEFAULT_TIMEOUT) where TElement : FrameworkElement;
    }
}
