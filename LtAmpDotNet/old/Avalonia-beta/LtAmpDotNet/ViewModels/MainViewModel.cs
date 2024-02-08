using LtAmpDotNet.Models;
using LtAmpDotNet.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;

namespace LtAmpDotNet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        PropertyChanged += MainViewModel_PropertyChanged;
        Views = new Dictionary<ViewNames, object>()
        {
            { ViewNames.Home, new HomeView() { DataContext = new HomeViewModel() } },
            { ViewNames.Settings, new SettingsView() { DataContext = new SettingsViewModel() } }
        };
        //CurrentViewName = ViewNames.Home;
    }

    private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    private HomeViewModel _homeViewModel;
    public HomeViewModel HomeViewModel
    {
        get => (Views[ViewNames.Home] as HomeView).DataContext as HomeViewModel;
    }

    private bool _isMenuOpen;
    public bool IsMenuOpen
    {
        get => _isMenuOpen;
        set => SetProperty(ref _isMenuOpen, value);
    }

    private Dictionary<ViewNames, object> _views;
    public Dictionary<ViewNames, object> Views
    {
        get => _views;
        set => SetProperty(ref _views, value);
    }

    private ViewNames _currentViewName;
    public ViewNames CurrentViewName
    {
        get => _currentViewName;
        set => SetPropertyAnd(ref _currentViewName, value,(x) => OnPropertyChanged(nameof(CurrentView)));
    }

    public object? CurrentView
    {
        get => Views[CurrentViewName];
    }

    public void ConnectCommand()
    {
        HomeViewModel.AmpState.Connect();
    }

    public void DisconnectCommand()
    {
        HomeViewModel.AmpState.Disconnect();
    }

    public void ToggleConnectCommand()
    {
        if (HomeViewModel.IsAmplifierConnected)
        {
            DisconnectCommand();
        }
        else
        {
            ConnectCommand();
        }
    }

    public void ShowDebugWindow()
    {
        var debugWindow = new DebugWindow(HomeViewModel.AmpState);
        debugWindow.Show();
    }

    public void ShowHidePresetMenu()
    {
        IsMenuOpen = !IsMenuOpen;
    }

    public void ToggleSettings()
    {
        switch (CurrentViewName)
        {
            case ViewNames.Settings: CurrentViewName = ViewNames.Home; break;
            default: CurrentViewName = ViewNames.Settings; break;
        }
    }

    public enum ViewNames
    {
        Home,
        Settings,
    }
}
