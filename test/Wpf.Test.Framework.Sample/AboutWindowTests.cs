using ApplicationUnderTest;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Wpf.Test.Framework.Sample
{
    [TestFixture]
    [Serializable]
    public class AboutWindowTests : BaseTests
    {
        [Test]
        public void GetAboutText()
        {
            this.ExecuteInSeparateAppDomain(() =>
            {
                this.ExecuteApplicationTest(this.GetAboutText_Inner);
            });
        }

        private async Task GetAboutText_Inner(App app)
        {
            await Task.Delay(0);
            UnderTestApplicationWrapper applicationWrapper = new UnderTestApplicationWrapper(app);

            MainWindowWrapper mainWindowWrapper = applicationWrapper.MainWindowWrapper;
            FrameworkElementCapture.Capture(mainWindowWrapper.Window, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "main_window.png"));
            mainWindowWrapper.ClickAbout();

            Button aboutButton = mainWindowWrapper.FindElement<Button>(btn => btn.Name == "btnAbout");
            FrameworkElementCapture.Capture(aboutButton, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "about_button.png"));

            AboutWindowWrapper aboutWindowWrapper = applicationWrapper.AboutWindowWrapper;
            FrameworkElementCapture.Capture(aboutWindowWrapper.Window, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "about_window.png"));

            TextBlock aboutTextBlock = aboutWindowWrapper.FindElement<TextBlock>(t => true && !string.IsNullOrWhiteSpace(t.Text));
            FrameworkElementCapture.Capture(aboutTextBlock, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "about_text.png"));

            string aboutText = aboutWindowWrapper.GetAboutText();

            Assert.AreEqual(aboutText, "Applicaiton under test.");
        }
    }
}
