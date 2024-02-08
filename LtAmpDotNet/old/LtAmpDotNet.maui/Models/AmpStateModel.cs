using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class AmpStateModel
    {
        public List<Preset> Presets;

        public int CurrentPresetIndex;
        public Preset CurrentPreset => Presets[CurrentPresetIndex];

        public int[] FootswitchSettings;

        private ILtAmplifier _amplifier;

        public AmpStateModel(ILtAmplifier amplifier)
        {
            _amplifier = amplifier;
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
        }

        private void _amplifier_CurrentDisplayedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_CurrentLoadedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void _amplifier_PresetSavedStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _amplifier_QASlotsStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }


    }
}
