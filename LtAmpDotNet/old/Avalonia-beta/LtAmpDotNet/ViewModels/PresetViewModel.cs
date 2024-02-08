using LtAmpDotNet.Models;

namespace LtAmpDotNet.ViewModels
{
    public class PresetViewModel : ViewModelBase
    {

        private PresetModel _model;

        public PresetModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public string DisplayName
        {
            get => _model.DisplayName;
            set => SetProperty(
                    oldValue: _model.DisplayName,
                    newValue: value,
                    model: _model,
                    callback: (model, val) => model.DisplayName = val
                );
        }

        public int SlotIndex
        {
            get => _model.SlotIndex;
            set => SetProperty(
                    oldValue: _model.SlotIndex,
                    newValue: value,
                    model: _model,
                    callback: (model, val) => model.SlotIndex = val
                );
        }

        public DspUnitViewModelCollection DspUnits
        {
            get => _model.DspUnits;
            //set => SetProperty(ref _dspUnits, value);
        }

        public string SelectedAmpFenderId
        {
            get => _model.SelectedAmpFenderId;
            set
            {
                if (SetProperty(_model.SelectedAmpFenderId, value, _model, (model, val) => model.SelectedAmpFenderId = val))
                {
                    OnPropertyChanged(nameof(AmpUnit));
                }
            }
        }
        public DspUnitViewModel AmpUnit => _model.AmpUnit;

        public string SelectedStompFenderId
        {
            get => _model.SelectedStompFenderId;
            set
            {
                if (SetProperty(_model.SelectedStompFenderId, value, _model, (model, val) => model.SelectedStompFenderId = val))
                {
                    OnPropertyChanged(nameof(StompUnit));
                }
            }
        }
        public DspUnitViewModel StompUnit
        {
            get => _model.StompUnit;
            //set => SetProperty(_model.StompUnit, value, _model, (model, val) => model.StompUnit = val);
        }

        public string SelectedModFenderId
        {
            get => _model.SelectedModFenderId;
            set
            {
                if (SetProperty(_model.SelectedModFenderId, value, _model, (model, val) => model.SelectedModFenderId = val))
                {
                    OnPropertyChanged(nameof(ModUnit));
                }
            }
        }
        public DspUnitViewModel ModUnit
        {
            get => _model.ModUnit;
            //			set => SetProperty(DspUnits[DspUnitType.mod], value, DspUnits, (model, value) => model[DspUnitType.mod] = value);
        }

        public string SelectedDelayFenderId
        {
            get => _model.SelectedDelayFenderId;
            set
            {
                if (SetProperty(_model.SelectedDelayFenderId, value, _model, (model, val) => model.SelectedDelayFenderId = val))
                {
                    OnPropertyChanged(nameof(DelayUnit));
                }
            }
        }
        public DspUnitViewModel DelayUnit
        {
            get => _model.DelayUnit;
            //set => SetProperty(DspUnits[DspUnitType.delay], value, DspUnits, (model, value) => model[DspUnitType.delay] = value);
        }

        public string SelectedReverbFenderId
        {
            get => _model.SelectedReverbFenderId;
            set
            {
                if (SetProperty(_model.SelectedReverbFenderId, value, _model, (model, val) => model.SelectedReverbFenderId = val))
                {
                    OnPropertyChanged(nameof(DelayUnit));
                }
            }
        }

        public DspUnitViewModel ReverbUnit
        {
            get => _model.ReverbUnit;
            //set => SetProperty(DspUnits[DspUnitType.reverb], value, DspUnits, (model, value) => model[DspUnitType.reverb] = value);
        }

        public PresetViewModel()
        {
            //DspUnits = new DspUnitViewModelCollection();
        }
        public PresetViewModel(PresetModel model)
        {
            _model = model;
        }

        public static implicit operator PresetViewModel(PresetModel model)
        {
            if (model != null)
            {
                return new PresetViewModel(model);
            }
            return new PresetViewModel();
        }
    }
}
