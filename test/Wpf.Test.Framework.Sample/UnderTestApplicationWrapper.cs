using System.Windows;

namespace Wpf.Test.Framework.Sample
{
    public class UnderTestApplicationWrapper : ApplicationWrapper<ApplicationUnderTest.App>
    {
        public UnderTestApplicationWrapper(ApplicationUnderTest.App app) : base(app)
        {

        }

        public MainWindowWrapper MainWindowWrapper
        {
            get
            {
                return new MainWindowWrapper(this.Application.MainWindow);
            }
        }

        public AboutWindowWrapper AboutWindowWrapper
        {
            get
            {
                Window aboutWindow = this.FindWindow(window => window.GetType() == typeof(ApplicationUnderTest.AboutWindow));

                return new AboutWindowWrapper(aboutWindow);
            }
        }
    }
}
