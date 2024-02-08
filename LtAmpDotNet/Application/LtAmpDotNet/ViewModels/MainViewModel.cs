using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services.Messages;
using LtAmpDotNet.Views;
using System.Collections.Generic;

namespace LtAmpDotNet.ViewModels;

[MessageReceiverChannel<MessageChannelEnum>(MessageChannelEnum.FromAmplifier)]
public partial class MainViewModel : ViewModelBase, IRecipient<ConnectionStatusMessage>
{
    #region Constructors

    public MainViewModel(AmpStateModel ampState) : base()
    {
        _presetViewModel = new PresetViewModel(ampState);
        _qaSlotsViewModel = new QaSlotsViewModel(ampState.Presets);
        _viewList = new Dictionary<ViewNames, object>()
        {
            { ViewNames.Preset, new PresetView() { DataContext = PresetViewModel } },
            { ViewNames.Settings, new SettingsView() },
        };
        IsActive = true;
    }

    #endregion Constructors

    #region Fields and properties

    private DebugWindow? debugWindow;

    private bool _isMenuOpen;

    public bool IsMenuOpen
    {
        get => _isMenuOpen;
        set => SetProperty(ref _isMenuOpen, value);
    }

    private Dictionary<ViewNames, object> _viewList;

    public Dictionary<ViewNames, object> ViewList
    {
        get => _viewList;
        set => SetProperty(ref _viewList, value);
    }

    private ViewNames _currentViewName;

    public ViewNames CurrentViewName
    {
        get => _currentViewName;
        set => SetPropertyAnd(ref _currentViewName, value, (x) => OnPropertyChanged(nameof(CurrentView)));
    }

    public object? CurrentView => ViewList[CurrentViewName];

    private ConnectionStatus _connectionStatus;

    public ConnectionStatus ConnectionStatus
    {
        get => _connectionStatus;
        set => SetProperty(ref _connectionStatus, value);
    }

    private bool _isLoaded;

    public bool IsLoaded
    {
        get => _isLoaded;
        set => SetProperty(ref _isLoaded, value);
    }

    private bool _isTunerEnabled;

    public bool IsTunerEnabled
    {
        get => _isTunerEnabled;
        set => SetProperty(ref _isTunerEnabled, value);
    }

    private float _usbGain;

    public float UsbGain
    {
        get => _usbGain;
        set => SetProperty(ref _usbGain, value);
    }

    private PresetViewModel _presetViewModel;

    public PresetViewModel PresetViewModel
    {
        get => _presetViewModel;
        set => SetProperty(ref _presetViewModel, value);
    }

    private QaSlotsViewModel _qaSlotsViewModel;

    public QaSlotsViewModel QaSlotsViewModel
    {
        get => _qaSlotsViewModel;
        set => SetProperty(ref _qaSlotsViewModel, value);
    }

    #endregion Fields and properties

    #region Methods

    public void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen;
    }

    public void ToggleDebug()
    {
        debugWindow ??= new DebugWindow();
        if (!debugWindow.IsVisible)
        {
            debugWindow.Show();
        }
        else if (debugWindow.IsVisible)
        {
            debugWindow.Hide();
            debugWindow = null;
        }
    }

    public void ToggleConnect()
    {
        if (ConnectionStatus == ConnectionStatus.Connected)
        {
            Disconnect();
        }
        else if (ConnectionStatus == ConnectionStatus.Disconnected)
        {
            Connect();
        }
    }

    public void Connect()
    {
        Send(new ConnectionStatusMessage(ConnectionStatus.Connected), MessageChannelEnum.ToAmplifier);
    }

    public void Disconnect()
    {
        Send(new ConnectionStatusMessage(ConnectionStatus.Disconnected), MessageChannelEnum.ToAmplifier);
    }

    #endregion Methods

    #region Message receivers

    public void Receive(ConnectionStatusMessage message)
    {
        if (message.Connected == ConnectionStatus.Connected)
        {
            IsLoaded = true;
        }
        ConnectionStatus = message.Connected;
    }

    #endregion Message receivers
}

public enum ViewNames
{
    Preset,
    Settings,
}