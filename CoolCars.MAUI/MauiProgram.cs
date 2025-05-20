using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using CoolCars.MAUI.Handlers;

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
				// Initialize the image handler for fallback images
				ImageHandler.Initialize();
			});
		
		// Register the audio service
		builder.Services.AddSingleton(AudioManager.Current);

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
