using System.Windows;

namespace Wpf.Test.Framework.Sample
{
    public class UnderTestApplicationProxy : ApplicationProxy<ApplicationUnderTest.App>
    {
        public UnderTestApplicationProxy(ApplicationUnderTest.App app) : base(app)
        {

        }

        public MainWindowProxy MainWindowProxy
        {
            get
            {
                Window aboutWindow = this.FindWindow(window => window.GetType() == typeof(ApplicationUnderTest.MainWindow));

                return new MainWindowProxy(aboutWindow);
            }
        }

        public AboutWindowProxy AboutWindowProxy
        {
            get
            {
                Window aboutWindow = this.FindWindow(window => window.GetType() == typeof(ApplicationUnderTest.AboutWindow));

                return new AboutWindowProxy(aboutWindow);
            }
        }

        public LoginWindowProxy LoginWindowProxy
        {
            get
            {
                Window aboutWindow = this.FindWindow(window => window.GetType() == typeof(ApplicationUnderTest.Login));

                return new LoginWindowProxy(aboutWindow);
            }
        }
    }
}
