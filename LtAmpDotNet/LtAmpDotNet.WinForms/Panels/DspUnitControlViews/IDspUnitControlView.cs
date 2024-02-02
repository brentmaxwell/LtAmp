using LtAmpDotNet.Lib.Model.Preset;

namespace LtAmpDotNet.Panels.DspUnitControlViews
{
    public interface IDspUnitControlView
    {
        Node Node { get; set; }
        event EventHandler ChangesApplied;
    }
}
