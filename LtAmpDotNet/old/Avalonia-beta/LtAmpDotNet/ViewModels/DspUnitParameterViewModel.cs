using LtAmpDotNet.Base;
using LtAmpDotNet.Extensions;
using LtAmpDotNet.Models;
using LtAmpDotNet.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitParameterViewModel : ViewModelBase
    {
        public DspUnitParameterViewModel()
        {

        }

        public DspUnitParameterViewModel(DspUnitParameterModel model)
        {
            Model = model;
        }

        private DspUnitParameterModel _model;
        private DspUnitParameterModel Model
        {
            get => _model;
            set
            {
                if (SetProperty(ref _model, value))
                {
                    _model.PropertyChanged += model_PropertyChanged;
                }
            }
        }

        public string ControlId => _model.ControlId;

        public string? DisplayName => _model.DisplayName;

        public DspUnitParameterType ParameterType => _model.ParameterType;

        public float? Min => _model.Min;

        public float? Max => _model.Max;

        public int? NumTicks => _model.NumTicks;

        public float? DisplayMin => _model.DisplayMin;

        public float? DisplayMax => _model.DisplayMax;

        public IEnumerable<string>? ListItems => _model.ListItems;

        public float? SmallChange => (Max - Min) / (NumTicks - 1);

        public float? DisplaySmallChange => (DisplayMax - DisplayMin) / (NumTicks - 1);

        public bool IsList => ParameterType == DspUnitParameterType.List || ParameterType == DspUnitParameterType.BooleanList;

        public bool IsNumeric => ParameterType == DspUnitParameterType.Continuous;

        public dynamic Value
        {
            get => _model.Value;
            set
            {
                if (SetProperty(
                    oldValue: _model.Value,
                    newValue: value,
                    model: _model,
                    callback: (Action<DspUnitParameterModel, dynamic>)((model, val) => model.Value = val)
                ))
                {
                    OnPropertyChanged(nameof(DisplayValue));
                }
            }
        }

        public dynamic DisplayValue
        {
            get
            {

                if (ParameterType == DspUnitParameterType.Continuous)
                {
                    return ((float?)Value).GetValueOrDefault().Remap(Min.Value, Max.Value, DisplayMin.Value, DisplayMax.Value);
                }
                return Value;
            }
            set
            {
                if (ParameterType == DspUnitParameterType.Continuous)
                {
                    value = ((float?)value).GetValueOrDefault().Remap(DisplayMin.Value, DisplayMax.Value, Min.Value, Max.Value);
                }
                Value = value;
            }
        }

        private void model_PropertyChanged(object? sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

        public static implicit operator DspUnitParameterViewModel(DspUnitParameterModel model)
        {
            return new DspUnitParameterViewModel(model);
        }
    }

    public class DspUnitParameterViewModelCollection : ObservableDictionary<string, DspUnitParameterViewModel>
    {
        public DspUnitParameterViewModelCollection() { }

        public DspUnitParameterViewModelCollection(IEnumerable<DspUnitParameterViewModel> parameters)
        {
            foreach (var item in parameters)
            {
                Add(item);
            }
        }

        public DspUnitParameterViewModelCollection(DspUnitParameterModelCollection model)
        {
            foreach (var item in model)
            {
                Add(item.Key, item.Value);
            }
        }

        public void Add(DspUnitParameterViewModel value)
        {
            Add(key: value.ControlId, value: value);
        }

        public static implicit operator DspUnitParameterViewModelCollection(DspUnitParameterModelCollection model)
        {
            return new DspUnitParameterViewModelCollection(model);
        }
    }
}
