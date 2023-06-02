using UsosAppMaui.Service;
using UsosAppMaui.Model.Course;

namespace UsosAppMaui.Pages;

public partial class GroupsPage : ContentPage
{
    private UsosService usosService;
    public GroupsPage(UsosService usosService)
	{
		InitializeComponent();
        this.usosService = usosService;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
       var result=  Task.Run(() => usosService.getUserCourses()); 
    }
}