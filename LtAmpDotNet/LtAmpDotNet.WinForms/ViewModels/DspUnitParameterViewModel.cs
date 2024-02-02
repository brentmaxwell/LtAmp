using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using System.ComponentModel;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitParameterViewModel : ViewModelBase
    {
        private DspUnitParameter _parameter;
        private DspUnitUiParameter _definition;

        [Browsable(false)]
        public DspUnitParameter Parameter
        {
            get => _parameter;
            set => SetProperty(ref _parameter, value);
        }

        [Browsable(false)]
        public DspUnitUiParameter Definition
        {
            get => _definition;
            set => SetProperty(ref _definition, value);
        }

        public string? Name => Parameter?.Name;

        [Browsable(false)]
        public dynamic Value
        {
            get => Parameter.Value;
            set
            {
                dynamic oldValue = Parameter.Value;
                Parameter.Value = value;
                OnValueChanged("Value", oldValue, Value);
            }
        }

        [Browsable(false)]
        public DspUnitParameterType ParameterType => Parameter.ParameterType;

        [Browsable(false)]
        public float? Min => (ParameterType & DspUnitParameterType.Numeric) == ParameterType ? Definition.Min : null;

        [Browsable(false)]
        public float? Max => (ParameterType & DspUnitParameterType.Numeric) == ParameterType ? Definition.Max : null;

        [Browsable(false)]
        public int? Ticks => (ParameterType & DspUnitParameterType.Numeric) == ParameterType ? Definition.NumTicks : null;

        [Browsable(false)]
        public float? MappedMin => (ParameterType & DspUnitParameterType.Numeric) == ParameterType && Definition.Remap != null ? Definition.Remap.Min : null;

        [Browsable(false)]
        public float? MappedMax => (ParameterType & DspUnitParameterType.Numeric) == ParameterType && Definition.Remap != null ? Definition.Remap.Max : null;

        [EditorBrowsable(EditorBrowsableState.Always)]
        public dynamic MappedValue
        {
            get => Min != null && Max != null && MappedMin != null && MappedMax != null
                    ? Remap(Value, Min.Value, Max.Value, MappedMin.Value, MappedMax.Value)
                    : Value;
            set
            {
                dynamic oldValue = MappedValue;
                Parameter.Value = Min != null && Max != null && MappedMin != null && MappedMax != null
                    ? Remap(value, Min.Value, Max.Value, MappedMin.Value, MappedMax.Value)
                    : value;
                OnValueChanged("MappedValueValue", oldValue, value);
            }
        }

        public DspUnitParameterViewModel(DspUnitUiParameter definition, DspUnitParameter parameter)
        {
            Parameter = parameter;
            Definition = Definition;
        }

        public float Remap(dynamic from, float fromMin, float fromMax, float toMin, float toMax)
        {
            dynamic fromAbs = from.Value - fromMin;
            float fromMaxAbs = fromMax - fromMin;
            dynamic normal = fromAbs / fromMaxAbs;
            float toMaxAbs = toMax - toMin;
            dynamic toAbs = toMaxAbs * normal;
            dynamic to = toAbs + toMin;
            return to;
        }

    }
}
