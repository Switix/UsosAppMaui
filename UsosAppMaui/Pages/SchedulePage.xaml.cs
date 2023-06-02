using UsosAppMaui.Service;

namespace UsosAppMaui.Pages;

public partial class SchedulePage : ContentPage
{
	private UsosService usosService;
	public SchedulePage(UsosService usosService)
	{
		InitializeComponent();
		this.usosService = usosService;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		var result = Task.Run(()=> usosService.getTimeTable());
    }
}