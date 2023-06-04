using Microsoft.Extensions.Logging;
using UsosAppMaui.Pages;
using UsosAppMaui.Service;
using UsosAppMaui.ViewModel;

namespace UsosAppMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddTransient<GradeViewModel>();
        builder.Services.AddTransient<GroupDetailViewModel>();
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddSingleton<TokenService>();
		builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<UsosService>();

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<WelcomePage>();

		builder.Services.AddSingleton<GroupDetailPage>();
		builder.Services.AddSingleton<LoadingPage>();
		builder.Services.AddSingleton<GroupsPage>();
		builder.Services.AddSingleton<MapPage>();
		builder.Services.AddSingleton<SchedulePage>();
        builder.Services.AddTransient<GroupsViewModel>();
		builder.Services.AddSingleton<GradesPage>();




#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
