using LtAmpDotNet.Base;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LtAmpDotNet.ViewModels
{
    public class MainPageViewModel : ExtendedBindableObject
    {
        private ILtAmplifier _amplifier;

        private Preset _currentPreset;
        public Preset CurrentPreset
        {
            get { return _currentPreset; }
            set
            {
                _currentPreset = value;
                RaisePropertyChanged(() => CurrentPreset);
            }
        }

        private List<Preset?> _presetList = [];
        public List<Preset?> PresetList
        {
            get { return _presetList; }
            set
            {
                _presetList = value;
                RaisePropertyChanged(() => PresetList);
            }
        }

        public MainPageViewModel(ILtAmplifier amplifier)
        {
            _amplifier = amplifier;
            _amplifier.MessageReceived += _amplifier_MessageReceived;
            _amplifier.CurrentPresetStatusMessageReceived += _amplifier_CurrentPresetStatusMessageReceived;
            _amplifier.PresetJSONMessageReceived += _amplifier_PresetJSONMessageReceived;
            _amplifier.CurrentLoadedPresetIndexStatusMessageReceived += _amplifier_CurrentLoadedPresetIndexStatusMessageReceived;
            _ = Open();

        }

        private void _amplifier_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
        }

        private void _amplifier_PresetJSONMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            int slotIndex = e.Message.PresetJSONMessage.SlotIndex;
            Lib.Model.Preset.Preset preset = Lib.Model.Preset.Preset.FromString(e.Message.PresetJSONMessage.Data);
            PresetList[slotIndex] = Preset.New(preset, slotIndex);
        }

        private void _amplifier_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            int slotIndex = e.Message.CurrentPresetStatus.CurrentSlotIndex;
            Lib.Model.Preset.Preset preset = Lib.Model.Preset.Preset.FromString(e.Message.CurrentPresetStatus.CurrentPresetData);
            CurrentPreset = Preset.New(preset, slotIndex);
        }

        private void _amplifier_CurrentLoadedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            _amplifier.GetCurrentPreset();
        }

        public async Task Open()
        {
            await _amplifier.OpenAsync();
            if (_presetList.Count == 0)
            {
                for (int i = 0; i <= LtAmplifier.NUM_OF_PRESETS; i++)
                {
                    _presetList.Add(null);
                }
            }
            _amplifier.GetAllPresets();
            _amplifier.GetCurrentPreset();
        }

        public void LoadPreset(int presetIndex)
        {
            _amplifier.LoadPreset(presetIndex);
        }
    }
}
