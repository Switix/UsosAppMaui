using UsosAppMaui.Service;
using UsosAppMaui.Model.Unit;

namespace UsosAppMaui.Pages;

public partial class GradesPage : ContentPage
{
	private UsosService usosService;
	public GradesPage(UsosService usosService)
	{
		InitializeComponent();
		this.usosService = usosService;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
	{
        List<Unit> units = usosService.getUserGrades();
    }
}