using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Models;
using LtAmpDotNet.ViewModels;
using Microsoft.Extensions.Logging;

namespace LtAmpDotNet
{
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
                .RegisterAppServices()
                .RegisterModels()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
        {
            builder.Services
                .AddSingleton<IAmpDevice, MockHidDevice>()
                .AddSingleton<ILtAmplifier, LtAmplifier>();
            return builder;
        }

        public static MauiAppBuilder RegisterModels(this MauiAppBuilder builder)
        {
            builder.Services
                .AddSingleton<AmpStateModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services
                .AddSingleton<MainPageViewModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }

        public static void RegisterRoutes()
        {
            Routing.RegisterRoute("MainPage", typeof(MainPage));
        }
    }
}
