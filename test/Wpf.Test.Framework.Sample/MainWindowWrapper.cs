﻿using System.Windows;
using System.Windows.Controls;
using Wpf.Test.Framework.Extensions;

namespace Wpf.Test.Framework.Sample
{
    public class MainWindowWrapper : WindowWrapper
    {
        public MainWindowWrapper(Window window) : base(window)
        {
        }

        public void ClickAbout()
        {
            Button aboutButton = this.FindElement<Button>(btn => btn.Name == "btnAbout");

            FrameworkElementMouseExtension.Click(aboutButton);
        }
    }
}
