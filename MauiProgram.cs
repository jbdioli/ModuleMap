using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using ModuleMap.Helpers.Components.Maps;

namespace ModuleMap
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .UseMauiMaps()
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(MapComponent), typeof(MapComponentHandler));
                //handlers.AddHandler(typeof(Map), typeof(MapComponentHandler));

            });


            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
