using UsosAppMaui.Service;
using UsosAppMaui.Model.Course;

namespace UsosAppMaui.Pages;

public partial class GroupsPage : ContentPage
{
    private UsosService usosService;
    public GroupsPage(UsosService usosService, GroupsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        this.usosService = usosService;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
    }

 

}