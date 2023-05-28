using Newtonsoft.Json;
using UsosAppMaui.Controls;
using UsosAppMaui.Dto;

namespace UsosAppMaui.Pages;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
	}



    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        string accessTokenString = Preferences.Get(nameof(TokenDto), "");

        if (!string.IsNullOrEmpty(accessTokenString))
        {
            //TODO: sprawdz czy token jest wazny
            var accessToken = JsonConvert.DeserializeObject<TokenDto>(accessTokenString);
            App.AccessToken = accessToken;
            AppShell.Current.Items.Add(new FlyoutMenuControl());
            AppShell.Current.CurrentItem = AppShell.Current.Items.Last();
        }
        else
        {
            Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}