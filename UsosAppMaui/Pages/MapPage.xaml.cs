using UsosAppMaui.Service;
using UsosAppMaui.Model.Map;
namespace UsosAppMaui.Pages;

public partial class MapPage : ContentPage
{
	private UsosService usosService;
	public MapPage(UsosService usosService)
	{
		InitializeComponent();
		this.usosService = usosService;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        var result = usosService.getBuildings();

    }
}