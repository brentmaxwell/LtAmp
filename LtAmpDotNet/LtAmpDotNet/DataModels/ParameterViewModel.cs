using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.DataModels
{
    public class ParameterViewModel<T>
    {
        public string DisplayName { get; set; }
        public string ControlId { get; set; }
        public string? ControlType { get; set; }
        public float? Min { get; set; }
        public float? Max { get; set; }
        public int? NumTicks { get; set; }
        public dynamic Value { get; set; }

        public float? DisplayMin { get; set; }
        public float? DisplayMax { get; set; }
        public dynamic DisplayValue
        {
            get => Remap(Value, Min.Value, Max.Value, DisplayMin.Value, DisplayMax.Value);
            set => Value = Remap(value, DisplayMin.Value, DisplayMax.Value, Min.Value, Max.Value);
        }

        private dynamic Remap(dynamic from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = (from - fromMin);
            var fromMaxAbs = (fromMax - fromMin);
            var normal = (fromAbs / fromMaxAbs);
            var toMaxAbs = (toMax - toMin);
            var toAbs = (toMaxAbs * normal);
            var to = (toAbs + toMin);
            return to;
        }
    }
}
