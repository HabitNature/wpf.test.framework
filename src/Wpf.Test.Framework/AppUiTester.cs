using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Test.Framework
{
    [Serializable]
    public abstract class AppUiTester<TApplication> where TApplication : Application
    {
        protected abstract string ApplicationConfigPath { get; }

        protected void ExecuteApplicationTest(Func<TApplication, Task> test)
        {
            if (null == Application.ResourceAssembly)
            {
                Application.ResourceAssembly = Assembly.GetAssembly(typeof(TApplication));
            }

            TApplication app = (TApplication)Activator.CreateInstance(typeof(TApplication));

            dynamic appDynamic = app;
            appDynamic.InitializeComponent();

            EventHandler handler = null;
            handler = async (sender, e) =>
            {
                app.Activated -= handler;

                try
                {
                    await test(app);
                }
                catch (Exception exp)
                {
                    AppDomain.CurrentDomain.SetData("Exception", exp);
                }
                finally
                {
                    app.Dispatcher.InvokeShutdown();
                    app.Shutdown();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            };

            app.Activated += handler;

            app.Run();
        }

        protected void ExecuteInSeparateAppDomain(CrossAppDomainDelegate testDelegate)
        {
            if (null == Application.ResourceAssembly)
            {
                Application.ResourceAssembly = Assembly.GetAssembly(typeof(TApplication));
            }

            string domainName = Guid.NewGuid().ToString("D");
            AppDomainSetup appDomainSetup = new AppDomainSetup();
            appDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            appDomainSetup.ConfigurationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.ApplicationConfigPath);
            AppDomain appDomain = AppDomain.CreateDomain(domainName, null, appDomainSetup);

            appDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs e)
            {
                throw e.ExceptionObject as Exception;
            };

            try
            {
                appDomain.DoCallBack(testDelegate);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Exception exp = appDomain.GetData("Exception") as Exception;

                if (null != exp)
                {
                    throw exp;
                }
            }
        }
    }
}
