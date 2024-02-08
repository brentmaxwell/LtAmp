using LtAmpDotNet.Lib.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    internal class DspUnitParameterModel : ObservableModel
    {
        public ControlType? ControlType { get; set; }
        public string? ControlId { get; set; }
        public string? DisplayName { get; set; }

        public float? Min { get; set; }
        public float? Max { get; set; }
        public int NumTicks { get; set; }
        public string? Taper { get; set; }
        public IEnumerable<string>? ListItems { get; set; }

        public float? DisplayMin { get; set; }
        public float? DisplayMax { get; set; }
        public string? DisplayTaper { get; set; }
        public string? DisplayFormat { get; set; }
        public IEnumerable<string>? DisplayListItems { get; set; }

        private dynamic _value;
        public dynamic Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public DspUnitParameterModel(DspUnitUiParameter model, dynamic val = null)
        {
            ControlType = model.ControlType;
            ControlId = model.ControlId;
            DisplayName = model.DisplayName;
            Min = model.Min;
            Max = model.Max;
            NumTicks = model.NumTicks;
            Taper = model.Taper;
            ListItems = model.ListItems;
            if (model.Remap != null)
            {
                DisplayMin = model.Remap.Min;
                DisplayMax = model.Remap.Max;
                DisplayTaper = model.Remap.Taper;
                DisplayFormat = model.Remap.Format;
                DisplayListItems = model.Remap.ListItems;
            }
            Value = val;
        }
    }
}
