using LtAmpDotNet.Base;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Panels
{
    public partial class CurrentPresetPanel : UserControl
    {
        private CurrentPresetPanelViewModel viewModel;

        public CurrentPresetPanelViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                if (viewModel != null)
                {
                    labelPresetName.Text = viewModel.PresetName;
                    dspUnitControlAmp.ViewModel = viewModel.AmpViewModel;
                    dspUnitControlStomp.ViewModel = viewModel.StompViewModel;
                    dspUnitControlMod.ViewModel = viewModel.ModViewModel;
                    dspUnitControlDelay.ViewModel = viewModel.DelayViewModel;
                    dspUnitControlReverb.ViewModel = viewModel.ReverbViewModel;
                    viewModel.ValueChanged += viewModel_ValueChanged;
                }
            }
        }

        private void viewModel_ValueChanged(object? sender, ValueChangedEventArgs e)
        {

        }

        public CurrentPresetPanel()
        {
            InitializeComponent();
        }
    }
}
