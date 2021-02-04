using System;

namespace Wpf.Test.Framework.Sample
{
    [Serializable]
    public abstract class BaseTests : AppUiTester<ApplicationUnderTest.App>
    {
        protected override string ApplicationConfigPath => "ApplicationUnderTest.exe.config";
    }
}
