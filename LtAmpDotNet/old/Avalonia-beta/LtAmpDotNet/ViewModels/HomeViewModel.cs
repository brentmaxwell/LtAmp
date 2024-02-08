using LtAmpDotNet.Models;
using LtAmpDotNet.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LtAmpDotNet.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    public HomeViewModel()
    {
        AmpState = ActivatorUtilities.CreateInstance<AmpStateModel>(App.Current.Services);
        PropertyChanged += HomeViewModel_PropertyChanged;
    }

    private void HomeViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    private AmpStateModel _ampState;
    public AmpStateModel AmpState
    {
        get => _ampState;
        set => SetPropertyAnd(ref _ampState, value, (x) =>
        {
            x.PropertyChanged += AmpStateModel_PropertyChanged;
        });
    }

    public List<string> Presets
    {
        get => _ampState?.Presets.Select(y => y?.DisplayName).ToList();
    }

    private PresetViewModel _currentPreset;
    public PresetViewModel CurrentPreset
    {
        get => _currentPreset;
        set => SetPropertyAnd(ref _currentPreset, value, (x) =>
            {
                SelectedAmpIndex = AmpUnits.IndexOf(AmpUnits.SingleOrDefault(y => y.FenderId == x.AmpUnit?.FenderId, null));
                SelectedStompIndex = StompUnits.IndexOf(StompUnits.SingleOrDefault(y => y.FenderId == x.StompUnit?.FenderId, null));
                SelectedModIndex = ModUnits.IndexOf(ModUnits.SingleOrDefault(y => y.FenderId == x.ModUnit?.FenderId, null));
                SelectedDelayIndex = DelayUnits.IndexOf(DelayUnits.SingleOrDefault(y => y.FenderId == x.DelayUnit?.FenderId, null));
                SelectedReverbIndex = ReverbUnits.IndexOf(ReverbUnits.SingleOrDefault(y => y.FenderId == x.ReverbUnit?.FenderId, null));
            });
    }

    public int CurrentPresetIndex
    {
        get => _ampState.CurrentPresetIndex;
        set => SetPropertyAnd(_ampState.CurrentPresetIndex, value, _ampState, (model, val) => model.CurrentPresetIndex = val, (model, val) =>
            {
                CurrentPreset = model.CurrentPreset;
            });
    }

    public static DspUnitDefinitionModelCollection AmpUnits => DspUnitLists.Amps;
    private int _selectedAmpIndex = 0;
    public int SelectedAmpIndex
    {
        get => Math.Max(0, _selectedAmpIndex);
        set => SetPropertyAnd(ref _selectedAmpIndex, value, (x) =>
            {
                var newAmpUnit = DspUnitLists.Amps[x].FenderId;
                if (CurrentPreset.SelectedAmpFenderId != newAmpUnit)
                {
                    CurrentPreset.SelectedAmpFenderId = newAmpUnit;
                }
            });
    }

    public static DspUnitDefinitionModelCollection StompUnits => DspUnitLists.Stomp;
    private int _selectedStompIndex = 0;
    public int SelectedStompIndex
    {
        get => Math.Max(0, _selectedStompIndex);
        set => SetPropertyAnd(ref _selectedStompIndex, value, (x) =>
            {
                var newStompUnit = DspUnitLists.Stomp[x].FenderId;
                if (CurrentPreset.SelectedStompFenderId != newStompUnit)
                {
                    CurrentPreset.SelectedStompFenderId = newStompUnit;
                }
            });
    }

    public static DspUnitDefinitionModelCollection ModUnits => DspUnitLists.Mod;
    private int _selectedModIndex = 0;
    public int SelectedModIndex
    {
        get => Math.Max(0, _selectedModIndex);
        set => SetPropertyAnd(ref _selectedModIndex, value, (x) =>
            {
                var newModUnit = DspUnitLists.Mod[x].FenderId;
                if (CurrentPreset.SelectedModFenderId != newModUnit)
                {
                    CurrentPreset.SelectedModFenderId = newModUnit;
                }
            });
    }

    public static DspUnitDefinitionModelCollection DelayUnits => DspUnitLists.Delay;
    private int _selectedDelayIndex;
    public int SelectedDelayIndex
    {
        get => Math.Max(0, _selectedDelayIndex);
        set => SetPropertyAnd(ref _selectedDelayIndex, value, (x) =>
            {
                var newDelayUnit = DspUnitLists.Delay[x].FenderId;
                if (CurrentPreset.SelectedDelayFenderId != newDelayUnit)
                {
                    CurrentPreset.SelectedDelayFenderId = newDelayUnit;
                }
            });
    }

    public static DspUnitDefinitionModelCollection ReverbUnits => DspUnitLists.Reverb;
    private int _selectedReverbIndex = 0;
    public int SelectedReverbIndex
    {
        get => Math.Max(0, _selectedReverbIndex);
        set => SetPropertyAnd(ref _selectedReverbIndex, value, (x) =>
            {
                var newReverbUnit = DspUnitLists.Reverb[x].FenderId;
                if (CurrentPreset.SelectedReverbFenderId != newReverbUnit)
                {
                    CurrentPreset.SelectedReverbFenderId = newReverbUnit;
                }
            });
    }

    private bool _isAmplifierConnected;
    public bool IsAmplifierConnected
    {
        get => _ampState.IsAmplifierConnected;
        set => SetProperty(_ampState.IsAmplifierConnected, value, _ampState, (model, val) => model.IsAmplifierConnected = value);
    }

    private void AmpStateModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(AmpStateModel.Presets):
                OnPropertyChanged(nameof(Presets));
                OnPropertyChanged(nameof(CurrentPresetIndex));
                break;
            case nameof(AmpStateModel.CurrentPreset):
                CurrentPreset = AmpState.CurrentPreset;
                OnPropertyChanged(nameof(CurrentPresetIndex));
                break;
            case nameof(AmpStateModel.CurrentPresetIndex):
                OnPropertyChanged(nameof(CurrentPresetIndex));
                OnPropertyChanged(nameof(CurrentPreset));
                break;
            case nameof(AmpStateModel.IsAmplifierConnected):
                OnPropertyChanged(nameof(IsAmplifierConnected));
                OnPropertyChanged(nameof(Presets));
                OnPropertyChanged(nameof(CurrentPresetIndex));
                break;
        }
    }
}
