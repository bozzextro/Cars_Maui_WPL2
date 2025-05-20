using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace CoolCars.MAUI;

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
			})
			.ConfigureMauiHandlers(handlers =>
			{
				// Add any custom handlers here if needed
			});
		
		// Register the audio service
		builder.Services.AddSingleton(AudioManager.Current);

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
