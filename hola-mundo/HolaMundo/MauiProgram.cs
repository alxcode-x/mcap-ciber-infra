using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace HolaMundo;

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
			.ConfigureLifecycleEvents(events =>
			{
#if WINDOWS
                events.AddWindows(w =>
                    w.OnWindowCreated(window =>
                    {
						// Set window size
                        var nativeWindow = window.Handler.PlatformView;
                        nativeWindow.Width = 400;
                        nativeWindow.Height = 300;
                        nativeWindow.Title = "Mi App MAUI :)";

                        // Prevent resizing
                        nativeWindow.ResizeMode = Microsoft.UI.Xaml.WindowResizeMode.NoResize;
                    }));
#endif

#if MACCATALYST
                events.AddiOS(macos =>
                    macos.SceneWillConnect((scene, session, options) =>
                    {
                        if (scene is UIKit.UIWindowScene windowScene)
                        {
                            var size = new CoreGraphics.CGSize(400, 300);
                            var restrictions = windowScene.SizeRestrictions;

							// Set window size and prevent resizing
                            if (restrictions != null)
                            {
                                restrictions.MinimumSize = size;
                                restrictions.MaximumSize = size;
                            }
                        }
                    }));
#endif
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
