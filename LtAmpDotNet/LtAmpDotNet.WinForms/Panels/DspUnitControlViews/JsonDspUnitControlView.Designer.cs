namespace LtAmpDotNet.Panels.DspUnitControlViews
{
    partial class JsonDspUnitControlView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonDspUnitControlView));
            this.textBoxNode = new System.Windows.Forms.TextBox();
            this.toolStripButtonApply = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxNode
            // 
            this.textBoxNode.AcceptsReturn = true;
            this.textBoxNode.AcceptsTab = true;
            this.textBoxNode.AllowDrop = true;
            this.textBoxNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNode.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxNode.Location = new System.Drawing.Point(0, 0);
            this.textBoxNode.Multiline = true;
            this.textBoxNode.Name = "textBoxNode";
            this.textBoxNode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxNode.Size = new System.Drawing.Size(1394, 787);
            this.textBoxNode.TabIndex = 0;
            this.textBoxNode.WordWrap = false;
            this.textBoxNode.TextChanged += new System.EventHandler(this.textBoxNode_TextChanged);
            // 
            // toolStripButtonApply
            // 
            this.toolStripButtonApply.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonApply.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonApply.Image")));
            this.toolStripButtonApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonApply.Name = "toolStripButtonApply";
            this.toolStripButtonApply.Size = new System.Drawing.Size(42, 22);
            this.toolStripButtonApply.Text = "Apply";
            this.toolStripButtonApply.Click += new System.EventHandler(this.toolStripButtonApply_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancel.Image")));
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripButtonCancel.Size = new System.Drawing.Size(47, 22);
            this.toolStripButtonCancel.Text = "Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonApply,
            this.toolStripButtonCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 762);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(1394, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // JsonDspUnitControlView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxNode);
            this.Controls.Add(this.toolStrip1);
            this.Name = "JsonDspUnitControlView";
            this.Size = new System.Drawing.Size(1394, 787);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxNode;
        private ToolStripButton toolStripButtonApply;
        private ToolStripButton toolStripButtonCancel;
        private ToolStrip toolStrip1;
    }
}
