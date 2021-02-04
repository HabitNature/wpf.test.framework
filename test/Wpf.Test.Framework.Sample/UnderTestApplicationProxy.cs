using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Test.Framework.Sample
{
    public class UnderTestApplicationProxy : ApplicationProxy<ApplicationUnderTest.App>
    {
        public UnderTestApplicationProxy(ApplicationUnderTest.App app) : base(app)
        {

        }

        public async Task<MainWindowProxy> MainWindowProxyAsync()
        {
            Window aboutWindow = await this.FindWindowAsync(window => window.GetType() == typeof(ApplicationUnderTest.MainWindow));

            return new MainWindowProxy(aboutWindow);
        }

        public async Task<LoginWindowProxy> LoginWindowProxyAsync()
        {
            Window aboutWindow = await this.FindWindowAsync(window => window.GetType() == typeof(ApplicationUnderTest.Login));

            return new LoginWindowProxy(aboutWindow);
        }
    }
}
