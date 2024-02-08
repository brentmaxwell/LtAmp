using AutoMapper;
using Avalonia.Controls.ApplicationLifetimes;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class AmpStateModel : ViewModelBase
    {
        private PresetList _presets = new PresetList();
        private int _currentPresetIndex;
        public PresetList Presets
        {
            get => _presets;
            set => SetProperty(ref _presets, value);
        }
        public int CurrentPresetIndex
        {
            get => _currentPresetIndex;
            set
            {
                SetProperty(ref _currentPresetIndex, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPreset)));
            }
        }
        
        public PresetModel CurrentPreset => Presets[CurrentPresetIndex];
        
        public uint[] FootswitchSettings { get; set; }
        
        public float UsbGain { get; set; }

        private ILtAmplifier _amplifier;
        private IMapper _mapper;

        public event EventHandler<ControlledApplicationLifetimeStartupEventArgs> Startup;
        public event EventHandler<ControlledApplicationLifetimeExitEventArgs> Exit;

        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;


        public AmpStateModel() :
            this(
                (ILtAmplifier)App.Current.Services.GetService(typeof(ILtAmplifier)),
                (IMapper)App.Current.Services.GetService(typeof(IMapper))
            )
        { }

        public AmpStateModel(ILtAmplifier amplifier, IMapper mapper)
        {
            _amplifier = amplifier;
            _mapper = mapper;
            for(int i = 0; i <= LtAmplifier.NUM_OF_PRESETS; i++)
            {
                Presets.Add(null);
            }
            _amplifier.CurrentDisplayedPresetIndexStatusMessageReceived += _amplifier_CurrentDisplayedPresetIndexStatusMessageReceived;
            _amplifier.CurrentLoadedPresetIndexStatusMessageReceived += _amplifier_CurrentLoadedPresetIndexStatusMessageReceived;
            _amplifier.CurrentPresetStatusMessageReceived += _amplifier_CurrentPresetStatusMessageReceived;
            _amplifier.DspUnitParameterStatusMessageReceived += _amplifier_DspUnitParameterStatusMessageReceived;
            _amplifier.NewPresetSavedStatusMessageReceived += _amplifier_NewPresetSavedStatusMessageReceived;
            _amplifier.PresetEditedStatusMessageReceived += _amplifier_PresetEditedStatusMessageReceived;
            _amplifier.PresetJSONMessageReceived += _amplifier_PresetJSONMessageReceived;
            _amplifier.PresetSavedStatusMessageReceived += _amplifier_PresetSavedStatusMessageReceived;
            _amplifier.QASlotsStatusMessageReceived += _amplifier_QASlotsStatusMessageReceived;
            _amplifier.ReplaceNodeStatusMessageReceived += _amplifier_ReplaceNodeStatusMessageReceived;
            _amplifier.SetDspUnitParameterStatusMessageReceived += _amplifier_SetDspUnitParameterStatusMessageReceived;
            _amplifier.ShiftPresetStatusMessageReceived += _amplifier_ShiftPresetStatusMessageReceived;
            _amplifier.SwapPresetStatusMessageReceived += _amplifier_SwapPresetStatusMessageReceived;
            _amplifier.UsbGainStatusMessageReceived += _amplifier_UsbGainStatusMessageReceived;
            _amplifier.Open();

            _amplifier.GetAllPresets();
            _amplifier.GetCurrentPreset();
        }

        private void _amplifier_CurrentDisplayedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_CurrentLoadedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            CurrentPresetIndex = e.Message.CurrentLoadedPresetIndexStatus.CurrentLoadedPresetIndex;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPresetIndex)));
        }

        private void _amplifier_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            CurrentPresetIndex = e.Message.CurrentPresetStatus.CurrentSlotIndex;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPresetIndex)));
        }

        private void _amplifier_DspUnitParameterStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_NewPresetSavedStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_PresetEditedStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_PresetJSONMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            var index = e.Message.PresetJSONMessage.SlotIndex;
            var preset = Preset.FromString(e.Message.PresetJSONMessage.Data);
            var presetModel = _mapper.Map<PresetModel>(preset);
            Presets[index] = presetModel;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Presets)));
        }

        private void _amplifier_PresetSavedStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_QASlotsStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            FootswitchSettings = e.Message.QASlotsStatus.Slots.ToArray();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FootswitchSettings)));
        }

        private void _amplifier_ReplaceNodeStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_SetDspUnitParameterStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_ShiftPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_SwapPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_UsbGainStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            UsbGain = e.Message.UsbGainStatus.ValueDB;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UsbGain)));
        }

        public void Shutdown(int exitCode = 0)
        {
            throw new NotImplementedException();
        }
    }
}
