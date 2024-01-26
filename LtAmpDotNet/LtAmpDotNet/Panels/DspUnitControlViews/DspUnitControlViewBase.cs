using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LtAmpDotNet.Panels.DspUnitControlViews
{
    public class DspUnitControlViewBase : UserControl, IDspUnitControlView
    {
        public DspUnitControlViewModel ViewModel { get; set; }
        public DspUnitControlViewBase()
        {
            Dock = DockStyle.Fill;
        }

        private protected Node _node;
        public virtual Node Node
        {
            get => ViewModel.Node;
            set
            {
                ViewModel.Node = value;
                Refresh();
            }
        }

        public event EventHandler ChangesApplied;

        private protected void OnChangesApplied(object sender, EventArgs e)
        {
            ChangesApplied?.Invoke(sender, e);
        }

        public virtual void Refresh()
        {

        }
    }
}
