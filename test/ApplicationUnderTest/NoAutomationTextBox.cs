using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace ApplicationUnderTest
{
    public class NoAutomationTextBox : TextBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            //return base.OnCreateAutomationPeer();

            return null;
        }
    }
}
