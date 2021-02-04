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

        public async Task SetUserNameAsync(string userName)
        {
            TextBox userTextBox = await this.FindElementAsync<TextBox>(t => null != t.Name && t.Name.StartsWith("txbUser"));
            userTextBox.Text = userName;
        }

        public async Task SetPasswordAsync(string password)
        {
            TextBox passwordTextBox = await this.FindElementAsync<TextBox>(t => t.Name == "txbPassword");
            passwordTextBox.Text = password;
        }

        public async Task ClickLoginAsync()
        {
            Button loginButton = await this.FindElementAsync<Button>(btn => btn.Name == "btnLogin");
            loginButton.DoClick();
        }

        public async Task LoginAsync(string userName, string password)
        {
            await this.SetUserNameAsync(userName);
            await this.SetPasswordAsync(password);
            await this.ClickLoginAsync();
        }
    }
}
