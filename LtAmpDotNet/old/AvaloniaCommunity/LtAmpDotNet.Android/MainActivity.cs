using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.ViewModels;
using LtAmpDotNet.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LtAmpDotNet.Android;

[Activity(
    Label = "LtAmpDotNet.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        builder = AppBuilder.Configure<App>(() =>
        {
            var app = new App();
            app.Register(RegisterServices());
            return app;
        });
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    public static ServiceCollection RegisterServices()
    {
        var services = new ServiceCollection();
        if (OperatingSystem.IsWindows())
        {
            services.AddSingleton<MockDeviceState>(MockDeviceState.Load());
            services.AddSingleton<IAmpDevice, MockHidDevice>();
            services.AddSingleton<ILtAmplifier, LtAmplifier>();
        }
        return services;
    }
}
