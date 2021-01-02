using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace Wpf.Test.Framework
{
    public static class ScreenCapture
    {
        public static Image CaptureDesktop(string filePath = null)
        {
            IntPtr hWnd = GetDesktopWindow();

            return CaptureWindow(hWnd, filePath);
        }

        public static Image CaptureWindow(IntPtr hWnd, string filePath = null)
        {
            Rect rect = new Rect();

            GetWindowRect(hWnd, ref rect);

            Rectangle bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

            return CaptureRect(bounds, filePath);
        }

        public static Image CaptureForegroundWindow(string filePath = null)
        {
            IntPtr hWnd = GetForegroundWindow();

            return CaptureWindow(hWnd, filePath);
        }

        public static Image Capture(Rectangle rect, string filePath = null)
        {
            return CaptureRect(rect, filePath);
        }

        private static Image CaptureRect(Rectangle rect, string filePath = null)
        {
            Rectangle bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            Bitmap capture = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(capture))
            {
                graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                capture.Save(filePath);
            }

            return capture;
        }

        #region pinvoke user32

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        #endregion
    }
}
