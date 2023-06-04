using UsosAppMaui.Dto;
using UsosAppMaui.Pages;
using UsosAppMaui.Service;

namespace UsosAppMaui.Controls;

public partial class FlyoutFooterControl : StackLayout
{
	private UsosService usosService;
	public FlyoutFooterControl(UsosService usosService)
	{
		InitializeComponent();
		this.usosService = usosService;
	}

    private void LogOut(object sender, EventArgs e)
    {
		//usosService.logOut();
        //Preferences.Remove(nameof(TokenDto));
        //AppShell.Current.FlyoutHeader = null;
        ///AppShell.Current.FlyoutFooter = null;
        //App.User = null;
        //App.AccessToken = null;
        //foreach ( var i in AppShell.Current.Items)
        //{
        //    Console.WriteLine(i.ToString());    
        //}
       // AppShell.Current.CurrentItem = AppShell.Current.Items.First();
       //AppShell.Current.FlyoutIsPresented = false;
    }
}