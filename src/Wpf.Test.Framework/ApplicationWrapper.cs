﻿using System;
using System.Windows;
using Wpf.Test.Framework.Exceptions;

namespace Wpf.Test.Framework
{
    public class ApplicationWrapper<TApplication> where TApplication : Application
    {
        public TApplication Application { get; protected set; }

        public ApplicationWrapper(TApplication application)
        {
            if (null == application)
            {
                throw new ArgumentNullException(nameof(application));
            }

            this.Application = application;
        }

        public WindowWrapper FindWindowWrapper(Predicate<Window> predicate, int timeout = Wait.DEFAULT_TIMEOUT)
        {
            Window targetWindow = this.FindWindow(predicate, timeout);

            if (null == targetWindow)
            {
                throw new WindowNotFoundException();
            }

            return new WindowWrapper(targetWindow);
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

        protected Window FindWindow(Predicate<Window> predicate, int timeout = Wait.DEFAULT_TIMEOUT)
        {
            Window targetWindow = null;

            Wait.Until(() =>
            {
                targetWindow = this.GetWindow(predicate);

                return null != targetWindow;
            }, timeout);

            return targetWindow;
        }
    }
}
