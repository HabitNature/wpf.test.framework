using ApplicationUnderTest;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Test.Framework.Extensions;

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

        /// <summary>
        /// 测试多个测试可以顺序执行
        /// </summary>
        [Test]
        public void GetAboutText_test2()
        {
            this.ExecuteInSeparateAppDomain(() =>
            {
                this.ExecuteApplicationTest(this.GetAboutText_Inner);
            });
        }

        private async Task GetAboutText_Inner(App app)
        {
            await Task.Delay(0);
            UnderTestApplicationProxy applicationWrapper = new UnderTestApplicationProxy(app);
            MainWindowProxy mainWindowWrapper = applicationWrapper.MainWindowProxy;
            FrameworkElementCapture.Capture(mainWindowWrapper.Window, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "main_window.png"));
            await Task.Delay(1000);
            ScreenCapture.CaptureDesktop(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "desktop.png"));
            ScreenCapture.CaptureForegroundWindow(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "foreground_window.png"));
            ScreenCapture.Capture(new System.Drawing.Rectangle(1500, 400, 300, 200), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "fix_rect.png"));
            mainWindowWrapper.ClickAbout();

            Button aboutButton = mainWindowWrapper.FindElement<Button>(btn => btn.Name == "btnAbout");
            FrameworkElementCapture.Capture(aboutButton, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "about_button.png"));

            AboutWindowProxy aboutWindowWrapper = applicationWrapper.AboutWindowProxy;
            FrameworkElementCapture.Capture(aboutWindowWrapper.Window, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "about_window.png"));

            TextBlock aboutTextBlock = aboutWindowWrapper.FindElement<TextBlock>(t => true && !string.IsNullOrWhiteSpace(t.Text));
            FrameworkElementCapture.Capture(aboutTextBlock, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImages", "about_text.png"));
            aboutTextBlock.DoHover();
            bool isHover = aboutTextBlock.IsMouseOver;
            string aboutText = aboutWindowWrapper.GetAboutText();

            Assert.AreEqual(aboutText, "Applicaiton under test.");
        }
    }
}
