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
            await Task.Delay(0);
            UnderTestApplicationProxy applicationProxy = new UnderTestApplicationProxy(app);
            LoginWindowProxy loginWindowProxy = applicationProxy.LoginWindowProxy;

            loginWindowProxy.Login("user", "password");
        }
    }
}
