using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Panels.DspUnitControlViews;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Panels
{
    public partial class DspUnitControl : UserControl
    {
        private DspUnitControlViewBase unitControl;
        private DspUnitControlViewModel viewModel = new();


        public NodeIdType NodeId
        {
            get => viewModel.NodeId;
            set
            {
                viewModel.NodeId = value;
                labelDspUnitType.Text = viewModel.NodeId.ToString();
            }
        }

        public DspUnitControlViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                if (viewModel != null)
                {
                    unitControl.Node = viewModel.Node;
                    labelDspUnitType.Text = viewModel.NodeId.ToString();
                    viewModel.ValueChanged += viewModel_ValueChanged;
                }
            }
        }

        public DspUnitControl()
        {
            InitializeComponent();
            unitControl = new PropertyGridUnitControlView();
            panelControl.Controls.Add(unitControl);

        }

        private void viewModel_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "DspUnitControlViewModel.Node":
                    unitControl.Node = viewModel.Node;
                    break;
                case "DspUnitControlViewModel.NodeType":
                    labelDspUnitType.Text = (string)e.NewValue;
                    break;
            }
        }

        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unitControl = new JsonDspUnitControlView();
        }

        private void propertyGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unitControl = new PropertyGridUnitControlView();
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void checkBoxBypass_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxBypass.ForeColor = checkBoxBypass.Checked ? Color.Green : Color.FromKnownColor(KnownColor.ControlText);
            ViewModel.Bypass = checkBoxBypass.Checked;
            unitControl.Refresh();
        }
    }
}
