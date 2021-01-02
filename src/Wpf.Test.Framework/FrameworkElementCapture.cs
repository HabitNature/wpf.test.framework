using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf.Test.Framework
{
    public static class FrameworkElementCapture
    {
        public static Image Capture(FrameworkElement element, string filePath = null)
        {
            if (null == element)
            {
                throw new ArgumentNullException(nameof(element));
            }

            RenderTargetBitmap renderTargetBitmap = BuildRenderTargetBitmap(element);

            BitmapEncoder bitmapEncoder = GetBitmapEncoderByFileExtension(filePath);
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (Stream fileStream = File.Create(filePath))
                {
                    bitmapEncoder.Save(fileStream);
                }

                return Image.FromFile(filePath);
            }
            else
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmapEncoder.Save(memoryStream);
                    return Image.FromStream(memoryStream);
                }
            }
        }

        private static RenderTargetBitmap BuildRenderTargetBitmap(FrameworkElement element)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(element);

            System.Drawing.Size dpiSize = GetDpi();

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                pixelWidth: (int)bounds.Width,
                pixelHeight: (int)bounds.Height,
                dpiX: dpiSize.Width,
                dpiY: dpiSize.Height,
                pixelFormat: PixelFormats.Pbgra32);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(element);
                ctx.DrawRectangle(vb, null, bounds);
            }

            renderTargetBitmap.Render(dv);

            return renderTargetBitmap;
        }

        private static System.Drawing.Size GetDpi()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                return new System.Drawing.Size((int)graphics.DpiX, (int)graphics.DpiY);
            }
        }

        private static BitmapEncoder GetBitmapEncoderByFileExtension(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return new PngBitmapEncoder();
            }

            string fileName = Path.GetFileName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = fileName.Substring(fileNameWithoutExtension.Length).ToLower();

            switch (fileExtension)
            {
                case ".gif":
                    return new GifBitmapEncoder();

                case ".jpg":
                    return new JpegBitmapEncoder();
                case ".png":
                default:
                    return new PngBitmapEncoder();
            }
        }
    }
}
