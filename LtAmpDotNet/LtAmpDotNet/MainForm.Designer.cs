namespace LtAmpDotNet
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripPresetList = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelFootSwitch = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitterVuMeter = new System.Windows.Forms.Splitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxPresets = new System.Windows.Forms.ListBox();
            this.viewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.currentPresetPanel1 = new LtAmpDotNet.Panels.CurrentPresetPanel();
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewModelBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 340);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 388);
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
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPresetList,
            this.toolStripStatusLabel1,
            this.statusLabelFootSwitch,
            this.statusLabelConnectionStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripPresetList
            // 
            this.toolStripPresetList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripPresetList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPresetList.Image")));
            this.toolStripPresetList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPresetList.Name = "toolStripPresetList";
            this.toolStripPresetList.Size = new System.Drawing.Size(27, 22);
            this.toolStripPresetList.Text = "#";
            this.toolStripPresetList.ToolTipText = "Presets";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(660, 19);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // statusLabelFootSwitch
            // 
            this.statusLabelFootSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelFootSwitch.Name = "statusLabelFootSwitch";
            this.statusLabelFootSwitch.Size = new System.Drawing.Size(15, 19);
            this.statusLabelFootSwitch.Text = "[]";
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
            this.splitterVuMeter.Size = new System.Drawing.Size(3, 340);
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
            this.splitContainer1.Panel2.Controls.Add(this.currentPresetPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(600, 340);
            this.splitContainer1.SplitterDistance = 349;
            this.splitContainer1.TabIndex = 8;
            // 
            // listBoxPresets
            // 
            this.listBoxPresets.DataSource = this.viewModelBindingSource;
            this.listBoxPresets.DisplayMember = "FormattedDisplayName";
            this.listBoxPresets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPresets.FormattingEnabled = true;
            this.listBoxPresets.ItemHeight = 15;
            this.listBoxPresets.Location = new System.Drawing.Point(0, 0);
            this.listBoxPresets.Name = "listBoxPresets";
            this.listBoxPresets.Size = new System.Drawing.Size(349, 340);
            this.listBoxPresets.TabIndex = 0;
            // 
            // viewModelBindingSource
            // 
            this.viewModelBindingSource.DataMember = "Presets";
            this.viewModelBindingSource.DataSource = typeof(LtAmpDotNet.ViewModels.MainFormViewModel);
            // 
            // currentPresetPanel1
            // 
            this.currentPresetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentPresetPanel1.Location = new System.Drawing.Point(0, 0);
            this.currentPresetPanel1.Name = "currentPresetPanel1";
            this.currentPresetPanel1.Size = new System.Drawing.Size(247, 340);
            this.currentPresetPanel1.TabIndex = 0;
            // 
            // panelVuMeter
            // 
            this.panelVuMeter.BackColor = System.Drawing.SystemColors.Control;
            this.panelVuMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVuMeter.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelVuMeter.Location = new System.Drawing.Point(600, 0);
            this.panelVuMeter.Name = "panelVuMeter";
            this.panelVuMeter.Size = new System.Drawing.Size(200, 340);
            this.panelVuMeter.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "json";
            this.saveFileDialog1.Filter = "JSON Files|*.json|All Files|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 388);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "LtAmpDotNet";
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewModelBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStripContainer toolStripContainer1;
        private StatusStrip statusStrip1;
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
        private Panels.CurrentPresetPanel currentPresetPanel1;
        private BindingSource viewModelBindingSource;
        private ToolStripDropDownButton toolStripPresetList;
        private ToolStripStatusLabel statusLabelFootSwitch;
        private SaveFileDialog saveFileDialog1;
    }
}