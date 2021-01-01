using System;
using System.Windows;

namespace Wpf.Test.Framework
{
    public interface IElementFinder
    {
        TElement FindElement<TElement>(Predicate<TElement> predicate, int timeout = Wait.DEFAULT_TIMEOUT) where TElement : FrameworkElement;
    }
}
