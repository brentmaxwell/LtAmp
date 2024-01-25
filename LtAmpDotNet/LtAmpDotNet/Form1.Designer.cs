namespace LtAmpDotNet
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ltDeviceInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.presetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelCurrentPreset = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitterVuMeter = new System.Windows.Forms.Splitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxPresets = new System.Windows.Forms.ListBox();
            this.panelVuMeter = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vUMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ltDeviceInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.presetBindingSource)).BeginInit();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltDeviceInfoBindingSource
            // 
            this.ltDeviceInfoBindingSource.DataSource = typeof(LtAmpDotNet.Lib.LtDeviceInfo);
            // 
            // presetBindingSource
            // 
            this.presetBindingSource.DataSource = typeof(LtAmpDotNet.Lib.Model.Preset.Preset);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitterVuMeter);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelVuMeter);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 402);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 450);
            this.toolStripContainer1.TabIndex = 8;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.Sound;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCurrentPreset,
            this.toolStripStatusLabel1,
            this.statusLabelConnectionStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelCurrentPreset
            // 
            this.statusLabelCurrentPreset.Name = "statusLabelCurrentPreset";
            this.statusLabelCurrentPreset.Size = new System.Drawing.Size(14, 19);
            this.statusLabelCurrentPreset.Text = "#";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(688, 19);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // statusLabelConnectionStatus
            // 
            this.statusLabelConnectionStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelConnectionStatus.Name = "statusLabelConnectionStatus";
            this.statusLabelConnectionStatus.Size = new System.Drawing.Size(83, 19);
            this.statusLabelConnectionStatus.Text = "Disconnected";
            // 
            // splitterVuMeter
            // 
            this.splitterVuMeter.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitterVuMeter.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterVuMeter.Location = new System.Drawing.Point(597, 0);
            this.splitterVuMeter.Name = "splitterVuMeter";
            this.splitterVuMeter.Size = new System.Drawing.Size(3, 402);
            this.splitterVuMeter.TabIndex = 10;
            this.splitterVuMeter.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.listBoxPresets);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Size = new System.Drawing.Size(600, 402);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 8;
            // 
            // listBoxPresets
            // 
            this.listBoxPresets.DataBindings.Add(new System.Windows.Forms.Binding("SelectedIndex", this.ltDeviceInfoBindingSource, "ActivePresetIndex", true));
            this.listBoxPresets.DataSource = this.presetBindingSource;
            this.listBoxPresets.DisplayMember = "FormattedDisplayName";
            this.listBoxPresets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPresets.FormattingEnabled = true;
            this.listBoxPresets.ItemHeight = 15;
            this.listBoxPresets.Location = new System.Drawing.Point(0, 0);
            this.listBoxPresets.Name = "listBoxPresets";
            this.listBoxPresets.Size = new System.Drawing.Size(130, 402);
            this.listBoxPresets.TabIndex = 0;
            // 
            // panelVuMeter
            // 
            this.panelVuMeter.BackColor = System.Drawing.SystemColors.Control;
            this.panelVuMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVuMeter.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelVuMeter.Location = new System.Drawing.Point(600, 0);
            this.panelVuMeter.Name = "panelVuMeter";
            this.panelVuMeter.Size = new System.Drawing.Size(200, 402);
            this.panelVuMeter.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vUMeterToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // vUMeterToolStripMenuItem
            // 
            this.vUMeterToolStripMenuItem.Checked = true;
            this.vUMeterToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vUMeterToolStripMenuItem.Name = "vUMeterToolStripMenuItem";
            this.vUMeterToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.vUMeterToolStripMenuItem.Text = "&VU Meter";
            this.vUMeterToolStripMenuItem.Click += new System.EventHandler(this.vUMeterToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 26);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "LtAmpDotNet";
            ((System.ComponentModel.ISupportInitialize)(this.ltDeviceInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.presetBindingSource)).EndInit();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private BindingSource presetBindingSource;
        private BindingSource ltDeviceInfoBindingSource;
        private ToolStripContainer toolStripContainer1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabelCurrentPreset;
        private ToolStripStatusLabel statusLabelConnectionStatus;
        private SplitContainer splitContainer1;
        private ListBox listBoxPresets;
        private Splitter splitterVuMeter;
        private Panel panelVuMeter;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem vUMeterToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ContextMenuStrip contextMenuStrip1;
    }
}