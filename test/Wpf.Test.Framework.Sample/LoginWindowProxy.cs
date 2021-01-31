using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Test.Framework.Extensions;

namespace Wpf.Test.Framework.Sample
{
    public class LoginWindowProxy : WindowProxy
    {
        public LoginWindowProxy(Window window) : base(window)
        {
        }

        public void Login(string userName, string password)
        {
            TextBox userTextBox = this.FindElement<TextBox>(t => t.Name == "txbUser");
            userTextBox.Text = "admin";

            TextBox passwordTextBox = this.FindElement<TextBox>(t => t.Name == "txbPassword");
            userTextBox.Text = "password";

            Button loginButton = this.FindElement<Button>(btn => btn.Name == "btnLogin");
            loginButton.DoClick();
        }
    }
}
