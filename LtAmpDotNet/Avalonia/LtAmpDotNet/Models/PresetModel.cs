using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Models
{
    public class PresetModel : ObservableObject
    {
        private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private DspUnitModel _ampUnit;
        public DspUnitModel AmpUnit
        {
            get => _ampUnit;
            set => SetProperty(ref _ampUnit, value);
        }

        private DspUnitModel _stompUnit;
        public DspUnitModel StompUnit
        {
            get => _stompUnit;
            set => SetProperty(ref _stompUnit, value);
        }

        private DspUnitModel _modUnit;
        public DspUnitModel ModUnit
        {
            get => _modUnit;
            set => SetProperty(ref _modUnit, value);
        }

        private DspUnitModel _delayUnit;
        public DspUnitModel DelayUnit
        {
            get => _delayUnit;
            set => SetProperty(ref _delayUnit, value);
        }

        private DspUnitModel _reverbUnit;
        public DspUnitModel ReverbUnit
        {
            get => _reverbUnit;
            set => SetProperty(ref _reverbUnit, value);
        }
    }

    public class PresetModelCollection : ObservableCollection<PresetModel> { }
}
