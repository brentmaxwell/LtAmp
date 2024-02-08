using LtAmpDotNet.Models.Enums;

namespace LtAmpDotNet.Models.Events
{
    public interface IDspUnitParameterChangedEvent
    {
        event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;
    }
    public class DspUnitParameterValueChangedEventArgs
    {
        public string ControlId { get; set; }
        public DspUnitType DspUnitType { get; set; }
        public int PresetIndex { get; set; }
        public dynamic NewValue { get; set; }
    }

    public delegate void DspUnitParameterValueChangedEventHandler(object? sender, DspUnitParameterValueChangedEventArgs e);
}
