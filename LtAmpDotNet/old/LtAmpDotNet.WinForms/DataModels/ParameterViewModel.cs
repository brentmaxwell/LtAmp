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
            dynamic fromAbs = from - fromMin;
            float fromMaxAbs = fromMax - fromMin;
            dynamic normal = fromAbs / fromMaxAbs;
            float toMaxAbs = toMax - toMin;
            dynamic toAbs = toMaxAbs * normal;
            dynamic to = toAbs + toMin;
            return to;
        }
    }
}
