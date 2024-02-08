using LtAmpDotNet.Base;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services;
using LtAmpDotNet.Services.Midi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtAmpDotNet.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IStorageService _storageService;
        private readonly IMidiService _midiService;
        private SettingsModel? _settings = new();

        public SettingsModel? Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        private List<MidiDevice> _availableMidiDevices = [];

        public List<MidiDevice> AvailableMidiDevices
        {
            get => _availableMidiDevices;
            set => SetProperty(ref _availableMidiDevices, value);
        }

        public SettingsViewModel(IStorageService storageService, IMidiService midiService)
        {
            _storageService = storageService;
            _midiService = midiService;
            AvailableMidiDevices = midiService.ListDevices().Select(x => (MidiDevice)x).ToList();
        }

        public async Task LoadAsync()
        {
            Settings = await _storageService.LoadAsync<SettingsModel>("config.json");
            OnPropertyChanged(nameof(Settings));
        }

        public async Task SaveAsync()
        {
            _midiService.SetCommands(Settings?.MidiCommandDefinitions);
            await _storageService.SaveAsync<SettingsModel>("config.json", Settings);
        }
    }
}