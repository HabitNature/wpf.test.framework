using System;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Test.Framework.Exceptions;

namespace Wpf.Test.Framework
{
    public class ApplicationProxy<TApplication> where TApplication : Application
    {
        public TApplication Application { get; protected set; }

        public ApplicationProxy(TApplication application)
        {
            if (null == application)
            {
                throw new ArgumentNullException(nameof(application));
            }

            this.Application = application;
        }

        public async Task<WindowProxy> FindWindowProxyAsync(Predicate<Window> predicate, int timeout = Wait.DEFAULT_TIMEOUT)
        {
            Window targetWindow = await this.FindWindowAsync(predicate, timeout);

            if (null == targetWindow)
            {
                throw new WindowNotFoundException();
            }

            return new WindowProxy(targetWindow);
        }

        protected Window GetWindow(Predicate<Window> predicate)
        {
            if (null == predicate)
            {
                return this.Application.MainWindow;
            }

            foreach (Window window in this.Application.Windows)
            {
                if (predicate(window))
                {
                    return window;
                }
            }

            return null;
        }

        protected async Task<Window> FindWindowAsync(Predicate<Window> predicate, int timeout = Wait.DEFAULT_TIMEOUT)
        {
            Window targetWindow = null;

            await Wait.UntilAsync(() =>
            {
                targetWindow = this.GetWindow(predicate);

                return null != targetWindow;
            }, timeout);

            return targetWindow;
        }
    }
}
