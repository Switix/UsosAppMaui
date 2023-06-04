using UsosAppMaui.Service;
using UsosAppMaui.Model.Unit;
using UsosAppMaui.ViewModel;

namespace UsosAppMaui.Pages;

public partial class GradesPage : ContentPage
{
	private UsosService usosService;
	public GradesPage(UsosService usosService, GradeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        this.usosService = usosService;      
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
	{
        
    }
}