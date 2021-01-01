using System;
using System.Windows;
using System.Windows.Media;
using Wpf.Test.Framework.Exceptions;

namespace Wpf.Test.Framework
{
    internal static class FrameworkElementFinder
    {
        public static TElement Find<TElement>(DependencyObject scope, Predicate<TElement> predicate, int timeout = Wait.DEFAULT_TIMEOUT) where TElement : FrameworkElement
        {
            if (null == scope)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            TElement targetElement = null;

            Wait.Until(() =>
            {
                targetElement = Find<TElement>(scope, predicate);

                return null != targetElement;
            }, timeout);

            if (null == targetElement)
            {
                throw new ElementNotFoundException();
            }

            return targetElement;
        }

        private static TElement Find<TElement>(DependencyObject scope, Predicate<TElement> predicate) where TElement : DependencyObject
        {
            TElement targetElement = scope as TElement;
            if (null != targetElement
                && (null == predicate || predicate(targetElement)))
            {
                return targetElement;
            }

            int childrenCount = VisualTreeHelper.GetChildrenCount(scope);
            {
                for (int i = 0; i < childrenCount; i++)
                {
                    DependencyObject childObject = VisualTreeHelper.GetChild(scope, i);

                    targetElement = childObject as TElement;

                    if (null != targetElement
                        && (null == predicate || predicate(targetElement)))
                    {
                        break;
                    }
                    else
                    {
                        targetElement = Find<TElement>(childObject, predicate);

                        if (null != targetElement)
                        {
                            break;
                        }
                    }
                }
            }

            return targetElement;
        }
    }
}
