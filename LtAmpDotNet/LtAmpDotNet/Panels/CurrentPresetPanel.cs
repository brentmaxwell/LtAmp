using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LtAmpDotNet.Panels
{
    public partial class CurrentPresetPanel : UserControl
    {
        private CurrentPresetPanelViewModel viewModel;

        public CurrentPresetPanelViewModel ViewModel
        {
            get { return viewModel; }
            set {
                viewModel = value;
                if(viewModel != null)
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
