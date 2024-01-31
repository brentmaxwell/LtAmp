namespace LtAmpDotNet.Panels
{
    partial class CurrentPresetPanel
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
            this.DspUnitPanels = new System.Windows.Forms.TableLayoutPanel();
            this.dspUnitControlStomp = new LtAmpDotNet.Panels.DspUnitControl();
            this.dspUnitControlMod = new LtAmpDotNet.Panels.DspUnitControl();
            this.dspUnitControlDelay = new LtAmpDotNet.Panels.DspUnitControl();
            this.dspUnitControlReverb = new LtAmpDotNet.Panels.DspUnitControl();
            this.labelPresetName = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dspUnitControlAmp = new LtAmpDotNet.Panels.DspUnitControl();
            this.DspUnitPanels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DspUnitPanels
            // 
            this.DspUnitPanels.ColumnCount = 4;
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.Controls.Add(this.dspUnitControlStomp, 0, 0);
            this.DspUnitPanels.Controls.Add(this.dspUnitControlMod, 1, 0);
            this.DspUnitPanels.Controls.Add(this.dspUnitControlDelay, 2, 0);
            this.DspUnitPanels.Controls.Add(this.dspUnitControlReverb, 3, 0);
            this.DspUnitPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DspUnitPanels.Location = new System.Drawing.Point(0, 0);
            this.DspUnitPanels.Name = "DspUnitPanels";
            this.DspUnitPanels.RowCount = 1;
            this.DspUnitPanels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.DspUnitPanels.Size = new System.Drawing.Size(812, 182);
            this.DspUnitPanels.TabIndex = 0;
            // 
            // dspUnitControlStomp
            // 
            this.dspUnitControlStomp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dspUnitControlStomp.Location = new System.Drawing.Point(3, 3);
            this.dspUnitControlStomp.Name = "dspUnitControlStomp";
            this.dspUnitControlStomp.Size = new System.Drawing.Size(197, 176);
            this.dspUnitControlStomp.TabIndex = 1;
            this.dspUnitControlStomp.ViewModel = null;
            // 
            // dspUnitControlMod
            // 
            this.dspUnitControlMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dspUnitControlMod.Location = new System.Drawing.Point(206, 3);
            this.dspUnitControlMod.Name = "dspUnitControlMod";
            this.dspUnitControlMod.Size = new System.Drawing.Size(197, 176);
            this.dspUnitControlMod.TabIndex = 2;
            this.dspUnitControlMod.ViewModel = null;
            // 
            // dspUnitControlDelay
            // 
            this.dspUnitControlDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dspUnitControlDelay.Location = new System.Drawing.Point(409, 3);
            this.dspUnitControlDelay.Name = "dspUnitControlDelay";
            this.dspUnitControlDelay.Size = new System.Drawing.Size(197, 176);
            this.dspUnitControlDelay.TabIndex = 3;
            this.dspUnitControlDelay.ViewModel = null;
            // 
            // dspUnitControlReverb
            // 
            this.dspUnitControlReverb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dspUnitControlReverb.Location = new System.Drawing.Point(612, 3);
            this.dspUnitControlReverb.Name = "dspUnitControlReverb";
            this.dspUnitControlReverb.Size = new System.Drawing.Size(197, 176);
            this.dspUnitControlReverb.TabIndex = 4;
            this.dspUnitControlReverb.ViewModel = null;
            // 
            // labelPresetName
            // 
            this.labelPresetName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPresetName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelPresetName.Location = new System.Drawing.Point(0, 0);
            this.labelPresetName.Name = "labelPresetName";
            this.labelPresetName.Size = new System.Drawing.Size(812, 40);
            this.labelPresetName.TabIndex = 0;
            this.labelPresetName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dspUnitControlAmp);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DspUnitPanels);
            this.splitContainer1.Size = new System.Drawing.Size(812, 376);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // dspUnitControlAmp
            // 
            this.dspUnitControlAmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dspUnitControlAmp.Location = new System.Drawing.Point(0, 0);
            this.dspUnitControlAmp.Name = "dspUnitControlAmp";
            this.dspUnitControlAmp.Size = new System.Drawing.Size(812, 186);
            this.dspUnitControlAmp.TabIndex = 0;
            this.dspUnitControlAmp.ViewModel = null;
            // 
            // CurrentPresetPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.labelPresetName);
            this.Name = "CurrentPresetPanel";
            this.Size = new System.Drawing.Size(812, 416);
            this.DspUnitPanels.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel DspUnitPanels;
        private Label labelPresetName;
        private DspUnitControl dspUnitControlStomp;
        private DspUnitControl dspUnitControlMod;
        private DspUnitControl dspUnitControlDelay;
        private DspUnitControl dspUnitControlReverb;
        private SplitContainer splitContainer1;
        private DspUnitControl dspUnitControlAmp;
        //private DspUnitControl dspUnitControlAmp;
        //private DspUnitControl dspUnitControlStomp;
        //private DspUnitControl dspUnitControlMod;
        //private DspUnitControl dspUnitControlDelay;
        //private DspUnitControl dspUnitControlReverb;
    }
}
