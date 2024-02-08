using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using LtAmpDotNet.ViewModels;
using LtAmpDotNet.Views;
using System;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Models;
using System.Reflection;

namespace LtAmpDotNet;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; set; }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView(){
                DataContext = (MainViewModel)App.Current.Services.GetService(typeof(MainViewModel))
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public void Register(ServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSingleton<AmpStateModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<DspUnitLists>();
        Services = services.BuildServiceProvider();
    }
}
