using UsosAppMaui.Dto;
using UsosAppMaui.Pages;

namespace UsosAppMaui;

public partial class App : Application
{
	public static TokenDto AccessToken {  get; set; }
	public App(AppShell appShell)
	{
		InitializeComponent();

		MainPage = appShell;

		RegisterRoutes();
		
	}

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(Pages.MainPage), typeof(Pages.MainPage));
        Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
        Routing.RegisterRoute(nameof(GradesPage), typeof(GradesPage));
        Routing.RegisterRoute(nameof(MailPage), typeof(MailPage));
        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
        Routing.RegisterRoute(nameof(GroupsPage), typeof(GroupsPage));
    }
}
