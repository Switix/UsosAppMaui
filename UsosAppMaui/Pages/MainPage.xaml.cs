using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
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

    private void Listener()
    {
        // 1
        HttpListener listener = new HttpListener();
        // 2
        listener.Prefixes.Add(UsosProp.CALLBACK_URL);

        // 3
        listener.Start();
        Debug.WriteLine("Listening...");

        //4
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
    }

    public async void Link()
    {
        string link = await tokenService.getRequestToken();
        webView.Source = new Uri(link);
       
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Task.Run(() => Listener());
        Link();
    }
}

