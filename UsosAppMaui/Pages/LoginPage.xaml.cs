namespace UsosAppMaui.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        User.login = login.Text;
        User.password = password.Text;
    }
}