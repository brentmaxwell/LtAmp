using LtAmpDotNet.Base;
using LtAmpDotNet.Extensions;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Models.Enums;
using LtAmpDotNet.Models.Events;
using System.Collections.Generic;

namespace LtAmpDotNet.Models
{
    public class DspUnitParameterModel : ObservableModel, IDspUnitParameterChangedEvent
    {
        public virtual event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;

        event DspUnitParameterValueChangedEventHandler? IDspUnitParameterChangedEvent.DspUnitParameterValueChanged
        {
            add => DspUnitParameterValueChanged += value;
            remove => DspUnitParameterValueChanged -= value;
        }

        private string _controlId;
        public string ControlId
        {
            get => _controlId;
            set => SetProperty(ref _controlId, value);
        }

        private string? _displayName;
        public string? DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }
        private DspUnitParameterType _parameterType;

        public DspUnitParameterType ParameterType
        {
            get => _parameterType;
            set => SetProperty(ref _parameterType, value);
        }

        private DspUnitParameterDataType _dataType;
        public DspUnitParameterDataType DataType
        {
            get => _dataType;
            set => SetProperty(ref _dataType, value);
        }

        private float? _min;
        public float? Min
        {
            get => _min;
            set => SetProperty(ref _min, value);
        }

        private float? _max;

        public float? Max
        {
            get => _max;
            set => SetProperty(ref _max, value);
        }

        private int? _numTicks;

        public int? NumTicks
        {
            get => _numTicks;
            set => SetProperty(ref _numTicks, value);
        }

        private float? _displayMin;
        public float? DisplayMin
        {
            get => _displayMin;
            set => SetProperty(ref _displayMin, value);
        }

        private float? _displayMax;
        public float? DisplayMax
        {
            get => _displayMax;
            set => SetProperty(ref _displayMax, value);
        }

        private IEnumerable<string>? _listItems;
        public IEnumerable<string>? ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }

        private dynamic _value;
        public dynamic Value
        {
            get => _value;
            set
            {
                if (SetProperty(ref _value, value))
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

        public float? SmallChange => (_max - _min) / (_numTicks - 1);
        public float? DisplaySmallChange => (_displayMax - _displayMin) / (_numTicks - 1);


        public new void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(Value) || propertyName == nameof(DisplayValue))
            {
                OnDspUnitParameterValueChanged(ControlId, Value);
            }
            base.OnPropertyChanged(propertyName);
        }

        public void OnDspUnitParameterValueChanged(string controlId, dynamic value)
        {
            DspUnitParameterValueChanged?.Invoke(this, new DspUnitParameterValueChangedEventArgs() { ControlId = controlId, NewValue = value });
        }
    }


    public class DspUnitParameterModelCollection : ObservableDictionary<string, DspUnitParameterModel>, IDspUnitParameterChangedEvent
    {
        public event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;

        event DspUnitParameterValueChangedEventHandler? IDspUnitParameterChangedEvent.DspUnitParameterValueChanged
        {
            add => DspUnitParameterValueChanged += value;
            remove => DspUnitParameterValueChanged -= value;
        }

        public DspUnitParameterModelCollection()
        {
            CollectionChanged += OnCollectionChanged;
        }

        public DspUnitParameterModelCollection(IEnumerable<DspUnitParameterModel> parameters)
        {
            foreach (var item in parameters)
            {
                Add(item);
            }
        }

        public void Add(DspUnitParameterModel value)
        {
            Add(key: value.ControlId, value: value);
        }

        private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (KeyValuePair<string, DspUnitParameterModel> item in e.NewItems)
            {
                this[item.Key].DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            }
        }

        private void OnDspUnitParameterValueChanged(object? sender, DspUnitParameterValueChangedEventArgs e)
        {
            DspUnitParameterValueChanged?.Invoke(sender, e);
        }
    }
}
