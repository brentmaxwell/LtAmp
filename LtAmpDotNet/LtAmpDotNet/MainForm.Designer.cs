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
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vUMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReset = new System.Windows.Forms.ToolStripButton();
            this.exportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewModelBindingSource)).BeginInit();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStripMain);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitterVuMeter);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelVuMeter);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1036, 504);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1036, 577);
            this.toolStripContainer1.TabIndex = 8;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStripMain);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripMain);
            // 
            // statusStripMain
            // 
            this.statusStripMain.AccessibleRole = System.Windows.Forms.AccessibleRole.Sound;
            this.statusStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPresetList,
            this.toolStripStatusLabel1,
            this.statusLabelFootSwitch,
            this.statusLabelConnectionStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 0);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1036, 24);
            this.statusStripMain.TabIndex = 7;
            this.statusStripMain.Text = "statusStrip1";
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
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(896, 19);
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
            this.splitterVuMeter.Location = new System.Drawing.Point(833, 0);
            this.splitterVuMeter.Name = "splitterVuMeter";
            this.splitterVuMeter.Size = new System.Drawing.Size(3, 504);
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
            this.splitContainer1.Size = new System.Drawing.Size(836, 504);
            this.splitContainer1.SplitterDistance = 300;
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
            this.listBoxPresets.Size = new System.Drawing.Size(300, 504);
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
            this.currentPresetPanel1.Size = new System.Drawing.Size(532, 504);
            this.currentPresetPanel1.TabIndex = 0;
            // 
            // panelVuMeter
            // 
            this.panelVuMeter.BackColor = System.Drawing.SystemColors.Control;
            this.panelVuMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVuMeter.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelVuMeter.Location = new System.Drawing.Point(836, 0);
            this.panelVuMeter.Name = "panelVuMeter";
            this.panelVuMeter.Size = new System.Drawing.Size(200, 504);
            this.panelVuMeter.TabIndex = 9;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1036, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemImport,
            this.menuItemExport,
            this.menuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuItemImport
            // 
            this.menuItemImport.Name = "menuItemImport";
            this.menuItemImport.Size = new System.Drawing.Size(110, 22);
            this.menuItemImport.Text = "&Import";
            this.menuItemImport.Click += new System.EventHandler(this.menuItemImport_Click);
            // 
            // menuItemExport
            // 
            this.menuItemExport.Name = "menuItemExport";
            this.menuItemExport.Size = new System.Drawing.Size(110, 22);
            this.menuItemExport.Text = "&Export";
            this.menuItemExport.Click += new System.EventHandler(this.menuItemExport_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(110, 22);
            this.menuItemExit.Text = "E&xit";
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
            this.vUMeterToolStripMenuItem.Click += new System.EventHandler(this.menuItemVuMeter_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripButtonSaveAs,
            this.toolStripButtonReset});
            this.toolStripMain.Location = new System.Drawing.Point(3, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(137, 25);
            this.toolStripMain.TabIndex = 2;
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonSave.Text = "Save";
            this.toolStripButtonSave.ToolTipText = "toolStripButtonSave";
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveAs.Image")));
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonSaveAs.Text = "Save As";
            // 
            // toolStripButtonReset
            // 
            this.toolStripButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonReset.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReset.Image")));
            this.toolStripButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReset.Name = "toolStripButtonReset";
            this.toolStripButtonReset.Size = new System.Drawing.Size(39, 22);
            this.toolStripButtonReset.Text = "Reset";
            // 
            // exportFileDialog
            // 
            this.exportFileDialog.DefaultExt = "json";
            this.exportFileDialog.Filter = "JSON Files|*.json|All Files|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 577);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.Text = "LtAmpDotNet";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewModelBindingSource)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStripContainer toolStripContainer1;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel statusLabelConnectionStatus;
        private SplitContainer splitContainer1;
        private ListBox listBoxPresets;
        private Splitter splitterVuMeter;
        private Panel panelVuMeter;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem menuItemImport;
        private ToolStripMenuItem menuItemExport;
        private ToolStripMenuItem menuItemExit;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem vUMeterToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panels.CurrentPresetPanel currentPresetPanel1;
        private BindingSource viewModelBindingSource;
        private ToolStripDropDownButton toolStripPresetList;
        private ToolStripStatusLabel statusLabelFootSwitch;
        private SaveFileDialog exportFileDialog;
        private ToolStrip toolStripMain;
        private ToolStripButton toolStripButtonSave;
        private ToolStripButton toolStripButtonSaveAs;
        private ToolStripButton toolStripButtonReset;
        private OpenFileDialog importFileDialog;
    }
}