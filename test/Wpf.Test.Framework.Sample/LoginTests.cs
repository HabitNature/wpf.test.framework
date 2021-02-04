using ApplicationUnderTest;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Wpf.Test.Framework.Sample
{
    [TestFixture]
    [Serializable]
    public class LoginTests : BaseTests
    {
        [Test]
        public void Login()
        {
            this.ExecuteInSeparateAppDomain(() =>
            {
                this.ExecuteApplicationTest(this.Login_Inner);
            });
        }

        private async Task Login_Inner(App app)
        {
            UnderTestApplicationProxy applicationProxy = new UnderTestApplicationProxy(app);
            LoginWindowProxy loginWindowProxy = await applicationProxy.LoginWindowProxyAsync();

            await loginWindowProxy.LoginAsync("user", "password");
        }
    }
}
