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
        private CurrentPresetPanelViewModel _viewModel = new CurrentPresetPanelViewModel(Preset.New);

        public CurrentPresetPanelViewModel ViewModel
        {
            get { return _viewModel; }
            set {
                labelPresetName.Text = value.PresetName;
                _viewModel = value;
                _viewModel.PropertyChanged += _viewModel_PropertyChanged;
            }
        }

        private void _viewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public CurrentPresetPanel()
        {
            InitializeComponent();
        }
    }
}
