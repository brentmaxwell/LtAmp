using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitParameterViewModel : ViewModelBase
    {
        public DspUnitParameterViewModel(DspUnitParameterModel model)
        {
            Model = model;
        }

        private DspUnitParameterModel _model;

        public DspUnitParameterModel Model
        {
            get => _model;
            set => SetPropertyAnd(ref _model, value, (val) => val.PropertyChanged += Model_PropertyChanged);
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.Value))
            {
                OnPropertyChanged(nameof(DisplayValue));
            }
        }

        public NodeIdType DspUnitType { get; set; }
        public float? SmallChange => (Model.Max - Model.Min) / (Model.NumTicks - 1);
        public float? DisplaySmallChange => (Model.DisplayMax - Model.DisplayMin) / (Model.NumTicks - 1);
        public bool IsList => Model.ControlType is ControlType.list or ControlType.listBool;
        public bool IsNumeric => Model.ControlType == ControlType.continuous;

        public dynamic DisplayValue
        {
            get => Model.ControlType == ControlType.continuous
                ? ((float?)Model.Value).GetValueOrDefault().Remap(Model.Min.Value, Model.Max.Value, Model.DisplayMin.Value, Model.DisplayMax.Value)
                : Model.Value;
            set => Model.Value = Model.ControlType == ControlType.continuous
                ? ((float?)value).GetValueOrDefault().Remap(Model.DisplayMin.Value, Model.DisplayMax.Value, Model.Min.Value, Model.Max.Value)
                : value;
        }
    }
}