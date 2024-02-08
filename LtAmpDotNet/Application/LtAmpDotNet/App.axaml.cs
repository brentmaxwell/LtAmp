using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services;
using LtAmpDotNet.Services.Midi;
using LtAmpDotNet.ViewModels;
using LtAmpDotNet.Views;
using Microsoft.Extensions.DependencyInjection;
using RtMidi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LtAmpDotNet
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current!;

        public IServiceProvider Services { get; set; }

        public T ResolveService<T>()
        {
            return ActivatorUtilities.GetServiceOrCreateInstance<T>(this.Services);
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
                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
                desktop.MainWindow = new MainWindow(ResolveService<MainViewModel>());
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView(ResolveService<MainViewModel>());
            }

            base.OnFrameworkInitializationCompleted();
        }

        public void Register(ServiceCollection services)
        {
            services.AddSingleton<IAmpDevice>(new MockHidDevice());
            //services.AddSingleton<IAmpDevice, UsbAmpDevice>();
            services.AddSingleton<ILtAmplifier, LtAmplifier>();
            services.AddSingleton<IMidiService>(sp =>
            {
                MidiApi midiServiceApi = OperatingSystem.IsWindows() ? MidiApi.WindowsMultimediaMidi :
                    OperatingSystem.IsLinux() ? MidiApi.LinuxAlsa :
                    OperatingSystem.IsAndroid() ? MidiApi.Android :
                    MidiApi.Unspecified;
                return new MidiService(midiServiceApi);
            });

            services.AddSingleton<DebugWindow>();
            services.AddSingleton<AmpStateModel>();
            services.AddSingleton<AmplifierService>();

            Services = CheckAndBuildRequiredServices(services, [
                typeof(IAmpDevice),
                typeof(ILtAmplifier),
                typeof(IStorageService),
                typeof(IMidiService),
            ]);
        }

        public static IServiceProvider CheckAndBuildRequiredServices(ServiceCollection serviceCollection, List<Type> requiredServices)
        {
            List<ServiceDescriptor> registerdServices = serviceCollection.IntersectBy(requiredServices, x => x.ServiceType).ToList();
            if (registerdServices.Count != requiredServices.Count)
            {
                List<Type> missingServices = requiredServices.Except(registerdServices.Select(x => x.ServiceType)).ToList();
                TypeLoadException exception = new("The following services were not registered with dependency injection:");
                missingServices.ForEach(x => exception.Data.Add(x, x));
                throw exception;
            }
            return serviceCollection.BuildServiceProvider();
        }

        public static AppBuilder Build(ServiceCollection services)
        {
            return AppBuilder.Configure<App>(() =>
            {
                App app = new();
                app.Register(services);
                return app;
            });
        }
    }
}