using Newtonsoft.Json;
using System.Net;
using UsosAppMaui.Controls;
using UsosAppMaui.Dto;
using UsosAppMaui.Service;
using UsosAppMaui.Utility;
using UsosAppMaui.Model;

namespace UsosAppMaui.Pages;

public partial class LoadingPage : ContentPage
{

    private TokenService tokenService ;
    private UsosService usosService;
	public LoadingPage(TokenService tokenService, UsosService usosService)
	{
		InitializeComponent();
        this.tokenService = tokenService;
        this.usosService = usosService;
	}



    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        CheckAccessToken(); 
    }

    private void CheckAccessToken()
    {
        string accessTokenString = Preferences.Get(nameof(TokenDto), "");

        if (!string.IsNullOrEmpty(accessTokenString))
        {
            var accessToken = JsonConvert.DeserializeObject<TokenDto>(accessTokenString);
            App.AccessToken = accessToken;
            if (CheckExpired())
            {
                App.FlyOutMenu = new FlyoutMenuControl();
                AppShell.Current.Items.Add(App.FlyOutMenu);
                AppShell.Current.CurrentItem = AppShell.Current.Items.Last();
                AppShell.Current.FlyoutFooter = new FlyoutFooterControl(usosService);
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                return;
            }     
        }
        AcquireAccessToken();
        
    }

    private bool CheckExpired()
    {
        Person user =  usosService.getUserData().Result;
        if (user.id == null) { return false; }
        App.User = user;
        return true;
    }

    private async void AcquireAccessToken()
    {
        webView.Navigating += (s, e) =>
        {
            getCredentials();
            if (e.Url.Contains("callback"))
            {
                webView.IsVisible = false;
            }
        };
        Link();
      
        await Task.Run(() => Listener());
       
        CheckAccessToken();
    }
    public async void Link()
    {
        string link = await tokenService.getRequestToken();
        webView.Source = new Uri(link);
        webView.Navigated += (s, e) =>
        {
            webView.Dispatcher.DispatchAsync(async () =>
            {
                getData();
            });
        };

    }
    private bool Listener()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(UsosProp.CALLBACK_URL);
        listener.Start();

        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        string oauth_token = "", oauth_verifier = "";
        string parameters = request.Url.Query.Remove(0, 1);

        string[] split = parameters.Split('&');
        foreach (string s in split)
        {
            string[] temp = s.Split("=");
            if (temp[0].Equals("oauth_token"))
                oauth_token = temp[1];
            else if (temp[0].Equals("oauth_verifier"))
                oauth_verifier = temp[1];
        }
        listener.Stop();
        tokenService.getAccessToken(oauth_token, oauth_verifier);
        return true;

    }

    public async void getData()
    {
        string script = "document.getElementsByName('submit')[0].addEventListener('click', function() {    var login = document.getElementById('username').value;  var password = document.getElementById('password').value;  localStorage.setItem('login', login);   localStorage.setItem('password',password);});";       
        await webView.EvaluateJavaScriptAsync(script);     
    }

 

    public async void getCredentials()
    {
        string scriptLogin = "localStorage.getItem('login');";
        string scriptPassword = "localStorage.getItem('password');";
        var login = await webView.EvaluateJavaScriptAsync(scriptLogin);
        var password = await webView.EvaluateJavaScriptAsync(scriptPassword);

        if (login != "null" && password != "null")
        {
            User.login = User.prepare(login.ToString());
            User.password = User.prepare(password.ToString());
            Preferences.Set(nameof(login), User.login);
            Preferences.Set(nameof(password), User.password);
            
        }
    }
}