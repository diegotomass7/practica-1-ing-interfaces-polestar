using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Polestar;

public partial class LoginView : UserControl
{
    public LoginView() => InitializeComponent();

    private void SignIn(object sender, RoutedEventArgs e)
    {
        var (message, field) = DemoAccess.Check(Email.Text, Secret.Password);
        Secret.Clear();
        Feedback.Text = message;
        if (UIElementAutomationPeer.CreatePeerForElement(Feedback) is AutomationPeer peer)
            peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);

        if (field == AccessField.Email)
            Email.Focus();
        else if (field == AccessField.Password)
            Secret.Focus();
    }

    public void Reset()
    {
        Email.Clear();
        Secret.Clear();
        Feedback.Text = "";
    }
}
