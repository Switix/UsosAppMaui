namespace UsosAppMaui.Pages;

public partial class MailPage : ContentPage
{
    public MailPage()
    {
        InitializeComponent();
        webView.Navigated += (s, e) =>
        {
            // Execute JavaScript after the WebView has finished loading
            string scriptFill = "document.getElementById('rcmloginuser').value = '" + User.login + "'; document.getElementById('rcmloginpwd').value ='" + User.password + "'; document.getElementById('rcmloginsubmit').click();";
            webView.Dispatcher.DispatchAsync(async () =>
            {
                await webView.EvaluateJavaScriptAsync(scriptFill);
            });
        };
    }
}