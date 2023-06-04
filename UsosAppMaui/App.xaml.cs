using UsosAppMaui.Controls;
using UsosAppMaui.Dto;
using UsosAppMaui.Model;
using UsosAppMaui.Pages;

namespace UsosAppMaui;

public partial class App : Application
{
    public static Person User { get; set; }
	public static TokenDto AccessToken {  get; set; }
    public static FlyoutMenuControl FlyOutMenu { get; internal set; }

    public App(AppShell appShell)
	{
		InitializeComponent();

		MainPage = appShell;

		RegisterRoutes();
		
	}

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(GroupDetailPage), typeof(GroupDetailPage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
        Routing.RegisterRoute(nameof(GradesPage), typeof(GradesPage));
        Routing.RegisterRoute(nameof(MailPage), typeof(MailPage));
        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
        Routing.RegisterRoute(nameof(GroupsPage), typeof(GroupsPage));
    }
}
