using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using UraniumUI;

namespace prestamosLibrosTFG;

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
                fonts.AddFont("KaolyDemo-Regular.ttf", "Kaoly");
                fonts.AddFont("Minecraft.ttf", "Minecraft");
                fonts.AddFont("LetterMagic.ttf", "Magic");
                fonts.AddFont("Notted.ttf", "Notted");
                fonts.AddFont("okemo.ttf", "okemo");
                fonts.AddFont("StretchPro.ttf", "stretch");
                fonts.AddFont("AkiraExpandedDemo.otf", "akira");
                fonts.AddFont("LEMONMILK-Regular.otf", "lemon");
            }).UseMauiCommunityToolkit()
			.ConfigureMopups()
            .UseFFImageLoading()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .UseUraniumUIBlurs();
            

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
