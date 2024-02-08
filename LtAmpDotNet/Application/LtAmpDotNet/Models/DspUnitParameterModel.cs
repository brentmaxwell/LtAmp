using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Lib.Model.Profile.ParameterValueTaper;
using System.Collections.Generic;

namespace LtAmpDotNet.Models
{
    public class DspUnitParameterModel : ObservableModel
    {
        public NodeIdType DspUnitType { get; set; }
        public ControlType? ControlType { get; set; }
        public string? ControlId { get; set; }
        public string? DisplayName { get; set; }
        public float? Min { get; set; }
        public float? Max { get; set; }
        public int? NumTicks { get; set; }
        public string? TaperType { get; set; }
        public IEnumerable<string>? ListItems { get; set; }
        public float? DisplayMin { get; set; }
        public float? DisplayMax { get; set; }
        public string? DisplayTaperType { get; set; }
        public string? DisplayFormat { get; set; }
        public IEnumerable<string>? DisplayListItems { get; set; }

        public Taper? Taper { get; set; }
        public Taper? DisplayTaper { get; set; }

        private dynamic _value;

        public dynamic Value
        {
            get => _value;
            set => SetProperty(ref _value, value, nameof(Value));
        }

        public DspUnitParameterModel()
        {
        }

        public DspUnitParameterModel(NodeIdType dspUnitType, DspUnitUiParameter model, dynamic value = default)
        {
            DspUnitType = dspUnitType;
            ControlType = model.ControlType;
            ControlId = model.ControlId;
            DisplayName = model.DisplayName;
            Min = model.Min;
            Max = model.Max;
            NumTicks = model.NumTicks;
            ListItems = model.ListItems;
            if (model.Taper != null)
            {
                TaperType = model.Taper;
                Taper = Taper.TaperTypes.GetValueOrDefault(model.Taper, new Taper(0));
            }
            if (model.Remap != null)
            {
                DisplayMin = model.Remap.Min;
                DisplayMax = model.Remap.Max;
                DisplayFormat = model.Remap.Format;
                DisplayListItems = model.Remap.ListItems;
                if (model.Remap.Taper != null)
                {
                    DisplayTaperType = model.Remap.Taper;
                    DisplayTaper = Taper.TaperTypes.GetValueOrDefault(model.Remap.Taper, new Taper(0));
                }
            }
            Value = value;
        }

        public DspUnitParameterModel Clone()
        {
            return new()
            {
                DspUnitType = DspUnitType,
                ControlType = ControlType,
                ControlId = ControlId,
                DisplayName = DisplayName,
                Min = Min,
                Max = Max,
                NumTicks = NumTicks,
                TaperType = TaperType,
                ListItems = ListItems,
                DisplayMin = DisplayMin,
                DisplayMax = DisplayMax,
                DisplayTaperType = DisplayTaperType,
                DisplayFormat = DisplayFormat,
                DisplayListItems = DisplayListItems,
                Taper = Taper,
                DisplayTaper = Taper,
                Value = Value
            };
        }
    }
}