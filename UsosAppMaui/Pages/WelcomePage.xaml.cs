using UsosAppMaui.Controls;

namespace UsosAppMaui.Pages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		AppShell.Current.Items.Add(new FlyoutMenuControl());
		AppShell.Current.CurrentItem = AppShell.Current.Items.Last();
    }
}