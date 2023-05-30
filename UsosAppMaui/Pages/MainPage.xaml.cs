using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks.Sources;
using UsosAppMaui.Controls;
using UsosAppMaui.Service;
using UsosAppMaui.Utility;

namespace UsosAppMaui.Pages;

public partial class MainPage : ContentPage
{

    private TokenService tokenService = new TokenService();
	public MainPage()
	{
		InitializeComponent();
        //this.tokenService = tokenService;
       
    }

    private bool Listener()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(UsosProp.CALLBACK_URL);
        listener.Start();
   
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        string oauth_token="", oauth_verifier="";
        string parameters = request.Url.Query.Remove(0,1);
        
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
        tokenService.getAccessToken(oauth_token,oauth_verifier);
        return true;

    }

    public async void Link()
    {
        string link = await tokenService.getRequestToken();
        webView.Source = new Uri(link);
       
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        webView.Navigating += (s, e) =>
        {
            if (e.Url.Contains("callback"))
            {
                webView.IsVisible = false;
            }
        };      
        Link();

        await Task.Run(() => Listener());
        AppShell.Current.Items.Add(new FlyoutMenuControl());
        AppShell.Current.CurrentItem = AppShell.Current.Items.Last();

    }
}

