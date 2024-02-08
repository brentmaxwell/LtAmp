using System;

using Avalonia;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using Microsoft.Extensions.DependencyInjection;

namespace LtAmpDotNet.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = BuildAvaloniaApp();
        builder.StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        var builder = AppBuilder.Configure<App>(() =>
        {
            var app = new App();
            app.Register(RegisterServices());
            return app;
        })
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
        return builder;
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
