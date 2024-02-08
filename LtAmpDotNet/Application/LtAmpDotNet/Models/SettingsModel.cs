using CommunityToolkit.Mvvm.ComponentModel;
using LtAmpDotNet.Services.Midi;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LtAmpDotNet.Models
{
    public class SettingsModel : ObservableObject
    {
        private List<MidiDevice> _selectedMidiDevices = [];

        [JsonPropertyName("SelectedMidiDevices")]
        public List<MidiDevice> SelectedMidiDevices
        {
            get => _selectedMidiDevices;
            set => SetProperty(ref _selectedMidiDevices, value);
        }

        private MidiCommandDefinitionsModel midiCommandDefinitions = [];

        [JsonPropertyName("MidiCommandDefinitions")]
        public MidiCommandDefinitionsModel MidiCommandDefinitions
        {
            get => midiCommandDefinitions;
            set => SetProperty(ref midiCommandDefinitions, value);
        }
    }
}