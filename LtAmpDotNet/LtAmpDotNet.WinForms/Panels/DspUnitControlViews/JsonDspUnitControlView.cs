using LtAmpDotNet.Lib.Model.Preset;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LtAmpDotNet.Panels.DspUnitControlViews
{
    public partial class JsonDspUnitControlView : DspUnitControlViewBase, IDspUnitControlView
    {
        public JsonDspUnitControlView(): base()
        {
            InitializeComponent();
        }

        public override void Refresh()
        {
            textBoxNode.Text = _node.ToString(Formatting.Indented);
            toolStrip1.Visible = false;
        }

        private void textBoxNode_TextChanged(object sender, EventArgs e)
        {
            toolStrip1.Visible = true;
        }

        private void toolStripButtonApply_Click(object sender, EventArgs e)
        {
            try
            {
                Node = Node.FromString(textBoxNode.Text );
                toolStrip1.Visible = false;
                OnChangesApplied(this, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Cannot apply json: {ex.Message}","Error applying node",MessageBoxButtons.OK);
            }
        }

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            Refresh();
            toolStrip1.Visible = false;
        }
    }
}
