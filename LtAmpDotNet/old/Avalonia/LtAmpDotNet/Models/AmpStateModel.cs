using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class AmpStateModel : ObservableObject
    {
        private PresetModelCollection _presets =
        [
            new PresetModel(){ DisplayName = "Test1"},
            new PresetModel(){ DisplayName = "Test2"},
            new PresetModel(){ DisplayName = "Test3"},
        ];

        public PresetModelCollection Presets
        {
            get => _presets;
            set => SetProperty(ref _presets, value);
        } 

        private int _currentPresetIndex;
        public int CurrentPresetIndex
        {
            get => _currentPresetIndex;
            set
            {
                SetProperty(ref _currentPresetIndex, value);
                OnPropertyChanged(nameof(CurrentPreset));
            }
        }

        public PresetModel CurrentPreset => Presets[CurrentPresetIndex];

        public AmpStateModel()
        {
            
        }
    }
}
