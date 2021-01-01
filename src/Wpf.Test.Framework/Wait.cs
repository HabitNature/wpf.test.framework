using System;
using System.Threading;

namespace Wpf.Test.Framework
{
    public static class Wait
    {
        public const int DEFAULT_TIMEOUT = 10 * 1000;
        public const int INTERVAL = 200;

        public static bool Until(Func<bool> predicate, int timeout = DEFAULT_TIMEOUT, int interval = INTERVAL)
        {
            if (null == predicate)
            {
                return true;
            }

            timeout = CorrectTimeout(timeout);
            interval = CorrectInterval(interval);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeout);

            bool isExpected = predicate();

            while (!isExpected && !cancellationTokenSource.IsCancellationRequested)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    return isExpected;
                }

                Thread.Sleep(interval);
                isExpected = predicate();
            }

            return isExpected;
        }

        private static int CorrectTimeout(int timeout)
        {
            return Math.Max(0, timeout);
        }

        private static int CorrectInterval(int interval)
        {
            return Math.Max(200, interval);
        }
    }
}
