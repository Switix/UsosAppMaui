namespace UsosAppMaui.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();
	}

    private void StackLayout_Loaded(object sender, EventArgs e)
    {
		Name.Text = App.User.first_name + " " + App.User.last_name;
    }
}