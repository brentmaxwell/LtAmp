using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;
using Microsoft.Extensions.DependencyInjection;

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
        builder = App.Build(RegisterServices());
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    public static ServiceCollection RegisterServices()
    {
        ServiceCollection services = new();
        return services;
    }
}