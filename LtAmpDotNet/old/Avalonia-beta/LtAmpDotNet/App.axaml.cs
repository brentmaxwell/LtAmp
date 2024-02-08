using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Live.Avalonia;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models;
using LtAmpDotNet.ViewModels;
using LtAmpDotNet.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace LtAmpDotNet;

public partial class App : Application
{

    public static AppBuilder Build(ServiceCollection services)
    {
        return AppBuilder.Configure<App>(() =>
        {
            var app = new App();
            app.Register(services);
            return app;
        });
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = ActivatorUtilities.CreateInstance<MainViewModel>(Services),
                Content = new MainView()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView()
            {
                DataContext = ActivatorUtilities.CreateInstance<MainViewModel>(Services)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public new static App Current => (App)Application.Current!;

    public IServiceProvider Services { get; set; }

    public void Register(ServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSingleton<IAmpDevice>(new MockHidDevice(MockDeviceState.Load()));
        //services.AddSingleton<IAmpDevice, UsbAmpDevice>();
        services.AddSingleton<ILtAmplifier, LtAmplifier>();
        services.AddSingleton<AmpStateModel>();
        Services = services.BuildServiceProvider();
    }

    private static bool IsProduction()
    {
#if DEBUG
        return false;
#else
        return true;
#endif
    }
}
