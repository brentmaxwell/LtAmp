using LtAmpDotNet.Lib.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitParameterViewModel : ViewModelBase
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
    }
}
