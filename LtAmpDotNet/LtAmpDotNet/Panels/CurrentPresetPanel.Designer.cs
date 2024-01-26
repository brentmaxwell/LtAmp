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
            this.labelPresetName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DspUnitPanels
            // 
            this.DspUnitPanels.ColumnCount = 4;
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.DspUnitPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DspUnitPanels.Location = new System.Drawing.Point(0, 40);
            this.DspUnitPanels.Name = "DspUnitPanels";
            this.DspUnitPanels.RowCount = 2;
            this.DspUnitPanels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.DspUnitPanels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.DspUnitPanels.Size = new System.Drawing.Size(812, 376);
            this.DspUnitPanels.TabIndex = 0;
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
            // CurrentPresetPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DspUnitPanels);
            this.Controls.Add(this.labelPresetName);
            this.Name = "CurrentPresetPanel";
            this.Size = new System.Drawing.Size(812, 416);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel DspUnitPanels;
        private Label labelPresetName;
        //private DspUnitControl dspUnitControlAmp;
        //private DspUnitControl dspUnitControlStomp;
        //private DspUnitControl dspUnitControlMod;
        //private DspUnitControl dspUnitControlDelay;
        //private DspUnitControl dspUnitControlReverb;
    }
}
