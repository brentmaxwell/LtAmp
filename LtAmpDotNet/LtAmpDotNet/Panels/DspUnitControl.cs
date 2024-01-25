using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.ViewModels;
using NUnit.Framework.Constraints;
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
    public partial class DspUnitControl : UserControl
    {
        private DspUnitControlViewModel _viewModel;

        public bool HasBypass {
            get
            {
                return _viewModel.DspUnitDefinition.Ui.HasBypass;
            }
        }
        public DspUnitControlViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                _viewModel.PropertyChanged += _viewModel_PropertyChanged;
            }
        }

        public string DspUnitType
        {
            get => _viewModel.NodeType;
            set => _viewModel.NodeType = value;
        }

        public DspUnitDefinition DspUnitDefinition
        {
            get => _viewModel.DspUnitDefinition;
            set
            {
                _viewModel.DspUnitDefinition = value;
                propertyGrid1.Refresh();
            }
        }

        public Node Node
        {
            get => _viewModel.Node;
            set {
                _viewModel.Node = value;
                propertyGrid1.Refresh();
            }
        }

        public bool Bypass
        {
            get => HasBypass ? checkBoxBypass.Checked : false;
            set => checkBoxBypass.Checked = value;
        }

        public DspUnitControl()
        {
            InitializeComponent();
            checkBoxBypass.CheckedChanged += CheckBoxBypass_CheckedChanged;
            propertyGrid1.SelectedObject = _viewModel;
        }

        private void CheckBoxBypass_CheckedChanged(object? sender, EventArgs e)
        {
            checkBoxBypass.ForeColor = checkBoxBypass.Checked ? Color.Green : Color.FromKnownColor(KnownColor.ControlText);
        }

        private void _viewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            propertyGrid1.Refresh();
        }
    }
}
