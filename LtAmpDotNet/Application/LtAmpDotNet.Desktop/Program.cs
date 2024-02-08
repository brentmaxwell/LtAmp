using Avalonia;
using LtAmpDotNet.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;

namespace LtAmpDotNet.Desktop;

internal static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
#if DEBUG
        WaitForDebugger();
#endif
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return App.Build(RegisterServices())
                    .UsePlatformDetect()
                    .WithInterFont()
                    .LogToTrace();
    }

    public static ServiceCollection RegisterServices()
    {
        ServiceCollection services = new();
        services.AddSingleton<IStorageService>(new StorageService());
        return services;
    }

    private static void WaitForDebugger()
    {
        Console.WriteLine("Waiting for debugger to attach (or press any key to continue)");
        while (!Debugger.IsAttached && !Console.KeyAvailable)
        {
            Thread.Sleep(100);
        }
        Console.WriteLine("Debugger attached");
    }
}