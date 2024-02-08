using Avalonia.Controls;
using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.ViewModels
{
    public class PresetViewModel : ViewModelBase
    {
        public PresetViewModel()
        {
            DspUnits = new() {
                { NodeIdType.stomp, new DspUnitViewModel() },
                { NodeIdType.mod, new DspUnitViewModel() },
                { NodeIdType.delay, new DspUnitViewModel() },
                { NodeIdType.reverb, new DspUnitViewModel() },
                { NodeIdType.amp, new DspUnitViewModel() },
            };
        }
        private ObservableDictionary<int,string> _presetList = new();
        public ObservableDictionary<int,string> PresetList
        {
            get => _presetList;
            set => SetProperty(ref _presetList, value);
        }

        private int _currentPreset;
        public int CurrentPreset
        {
            get => _currentPreset;
            set => SetProperty(ref _currentPreset, value);
        }

        private ObservableDictionary<NodeIdType,DspUnitViewModel> _dspUnits = new();
        public ObservableDictionary<NodeIdType, DspUnitViewModel> DspUnits
        {
            get => _dspUnits;
            set => SetProperty(ref _dspUnits, value);
        }
    }
}
