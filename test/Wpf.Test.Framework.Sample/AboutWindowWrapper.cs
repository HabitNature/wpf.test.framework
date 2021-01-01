using System.Windows;
using System.Windows.Controls;

namespace Wpf.Test.Framework.Sample
{
    public class AboutWindowWrapper : WindowWrapper
    {
        public AboutWindowWrapper(Window window) : base(window)
        {

        }

        public string GetAboutText()
        {
            TextBlock aboutTextBlock = this.FindElement<TextBlock>(t => true && !string.IsNullOrWhiteSpace(t.Text));

            return aboutTextBlock.Text;
        }
    }
}
