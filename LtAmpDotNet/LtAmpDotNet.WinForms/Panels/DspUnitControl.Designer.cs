namespace LtAmpDotNet.Panels
{
    partial class DspUnitControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DspUnitControl));
            this.comboBoxDspUnit = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelDspUnitType = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuView = new System.Windows.Forms.ToolStripDropDownButton();
            this.jsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl = new System.Windows.Forms.Panel();
            this.checkBoxBypass = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxDspUnit
            // 
            this.comboBoxDspUnit.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxDspUnit.FormattingEnabled = true;
            this.comboBoxDspUnit.Location = new System.Drawing.Point(0, 0);
            this.comboBoxDspUnit.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.comboBoxDspUnit.Name = "comboBoxDspUnit";
            this.comboBoxDspUnit.Size = new System.Drawing.Size(1630, 49);
            this.comboBoxDspUnit.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelDspUnitType,
            this.toolStripStatusLabel1,
            this.menuView});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1171);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 34, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1630, 54);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelDspUnitType
            // 
            this.labelDspUnitType.Name = "labelDspUnitType";
            this.labelDspUnitType.Size = new System.Drawing.Size(188, 41);
            this.labelDspUnitType.Text = "DspUnitType";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1220, 41);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // menuView
            // 
            this.menuView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jsonToolStripMenuItem,
            this.controlsToolStripMenuItem,
            this.propertyGridToolStripMenuItem});
            this.menuView.Image = ((System.Drawing.Image)(resources.GetObject("menuView.Image")));
            this.menuView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(108, 49);
            this.menuView.Text = "View";
            // 
            // jsonToolStripMenuItem
            // 
            this.jsonToolStripMenuItem.Name = "jsonToolStripMenuItem";
            this.jsonToolStripMenuItem.Size = new System.Drawing.Size(448, 54);
            this.jsonToolStripMenuItem.Text = "JSON";
            this.jsonToolStripMenuItem.Click += new System.EventHandler(this.jsonToolStripMenuItem_Click);
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(448, 54);
            this.controlsToolStripMenuItem.Text = "Controls";
            this.controlsToolStripMenuItem.Click += new System.EventHandler(this.controlsToolStripMenuItem_Click);
            // 
            // propertyGridToolStripMenuItem
            // 
            this.propertyGridToolStripMenuItem.Name = "propertyGridToolStripMenuItem";
            this.propertyGridToolStripMenuItem.Size = new System.Drawing.Size(448, 54);
            this.propertyGridToolStripMenuItem.Text = "Property Grid";
            this.propertyGridToolStripMenuItem.Click += new System.EventHandler(this.propertyGridToolStripMenuItem_Click);
            // 
            // panelControl
            // 
            this.panelControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 49);
            this.panelControl.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1630, 838);
            this.panelControl.TabIndex = 5;
            // 
            // checkBoxBypass
            // 
            this.checkBoxBypass.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxBypass.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxBypass.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBoxBypass.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxBypass.Location = new System.Drawing.Point(0, 887);
            this.checkBoxBypass.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.checkBoxBypass.Name = "checkBoxBypass";
            this.checkBoxBypass.Size = new System.Drawing.Size(1630, 284);
            this.checkBoxBypass.TabIndex = 6;
            this.checkBoxBypass.Text = "Bypass";
            this.checkBoxBypass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxBypass.UseVisualStyleBackColor = true;
            this.checkBoxBypass.Visible = false;
            this.checkBoxBypass.CheckedChanged += new System.EventHandler(this.checkBoxBypass_CheckedChanged);
            // 
            // DspUnitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.checkBoxBypass);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.comboBoxDspUnit);
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.Name = "DspUnitControl";
            this.Size = new System.Drawing.Size(1630, 1225);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComboBox comboBoxDspUnit;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel labelDspUnitType;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripDropDownButton menuView;
        private ToolStripMenuItem jsonToolStripMenuItem;
        private ToolStripMenuItem controlsToolStripMenuItem;
        private ToolStripMenuItem propertyGridToolStripMenuItem;
        private Panel panelControl;
        private CheckBox checkBoxBypass;
    }
}
