using LtAmpDotNet.Base;
using LtAmpDotNet.Models;
using LtAmpDotNet.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitViewModel : ViewModelBase
    {
        public DspUnitViewModel()
        {
        }

        public DspUnitViewModel(DspUnitModel model)
        {
            Model = model;
        }

        private DspUnitModel _model;
        public DspUnitModel Model
        {
            get => _model;
            set
            {
                if (SetProperty(ref _model, value))
                {
                    _model.PropertyChanged += Model_PropertyChanged;
                }
            }
        }

        public bool HasBypass { get; set; }

        public bool IsBypass
        { 
            get;
            set;
        }

        public DspUnitType DspUnitType => _model.DspUnitType;

        public string DisplayName => _model.DisplayName;

        public string FenderId => _model.FenderId;

        public DspUnitParameterViewModelCollection Parameters => _model.Parameters;

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

        public static implicit operator DspUnitViewModel(DspUnitModel model)
        {
            return new DspUnitViewModel(model);
        }
    }

    public class DspUnitViewModelCollection : ObservableDictionary<DspUnitType, DspUnitViewModel>
    {
        public DspUnitViewModelCollection()
        {
            foreach (DspUnitType item in Enum.GetValues(typeof(DspUnitType)))
            {
                Add(item, new DspUnitViewModel());
            }
        }

        public DspUnitViewModelCollection(IEnumerable<DspUnitViewModel> parameters)
        {
            foreach (var item in parameters)
            {
                Add(item);
            }
        }

        public DspUnitViewModelCollection(DspUnitModelCollection model)
        {
            foreach (var item in model)
            {
                Add(item.Key, item.Value);
            }
        }

        public void Add(DspUnitViewModel value)
        {
            Add(key: value.DspUnitType, value: value);
        }

        public static implicit operator DspUnitViewModelCollection(DspUnitModelCollection model)
        {
            return new DspUnitViewModelCollection(model);
        }
    }
}
