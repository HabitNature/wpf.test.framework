using ApplicationUnderTest;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
            mainWindowWrapper.ClickAbout();

            AboutWindowWrapper aboutWindowWrapper = applicationWrapper.AboutWindowWrapper;

            string aboutText = aboutWindowWrapper.GetAboutText();

            Assert.AreEqual(aboutText, "Applicaiton under test.");
        }
    }
}
