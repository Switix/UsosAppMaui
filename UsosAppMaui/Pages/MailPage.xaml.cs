namespace UsosAppMaui.Pages;

public partial class MailPage : ContentPage
{
    public MailPage()
    {
        User.login = Preferences.Get(nameof(User.login), "");
        User.password = Preferences.Get(nameof(User.password), "");
        InitializeComponent();
        webView.Navigated += (s, e) =>
        {
                   
            webView.Dispatcher.DispatchAsync(async () =>
            {
                
                string scriptFill = "document.getElementById('rcmloginuser').value = '" + User.login + "'; document.getElementById('rcmloginpwd').value ='" + User.password + "'; document.getElementById('rcmloginsubmit').click();";

                await webView.EvaluateJavaScriptAsync(scriptFill);
            });
        };
    }
}