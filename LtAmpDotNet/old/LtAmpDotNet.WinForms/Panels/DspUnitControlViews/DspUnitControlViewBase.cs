using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Panels.DspUnitControlViews
{
    public class DspUnitControlViewBase : UserControl, IDspUnitControlView
    {
        public DspUnitControlViewModel? ViewModel { get; set; }
        public DspUnitControlViewBase()
        {
            Dock = DockStyle.Fill;
        }

        private protected Node? _node;
        public virtual Node Node
        {
            get => ViewModel.Node;
            set
            {
                ViewModel.Node = value;
                Refresh();
            }
        }

        public event EventHandler? ChangesApplied;

        private protected void OnChangesApplied(object sender, EventArgs e)
        {
            ChangesApplied?.Invoke(sender, e);
        }

        public new virtual void Refresh()
        {

        }
    }
}
