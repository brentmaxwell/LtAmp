using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;

namespace LtAmpDotNet
{
    public partial class FormMain : Form
    {
        private LtAmpDevice amp = new LtAmpDevice();
        public FormMain()
        {
            InitializeComponent();
            amp.DeviceConnected += Amp_DeviceConnected;
            amp.DeviceDisconnected += Amp_DeviceDisconnected;
            amp.Open();
        }

        private void Amp_DeviceDisconnected(object sender, EventArgs e)
        {
            statusLabelConnectionStatus.Text = "Disconnected";
            statusLabelConnectionStatus.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void BindPresets()
        {
            LtAmpDevice.DeviceInfo.PropertyChanged += DeviceInfo_PropertyChanged;
            presetBindingSource.DataSource = LtAmpDevice.DeviceInfo.Presets;
            presetBindingSource.DataSourceChanged += PresetBindingSource_DataSourceChanged;
        }

        private void PresetBindingSource_DataSourceChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeviceInfo_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "LtDeviceInfo.ActivePresetIndex":
                    this.Invoke(new MethodInvoker(delegate
                    {
                        listBoxPresets.SelectedIndex = LtAmpDevice.DeviceInfo.ActivePresetIndex;
                        statusLabelCurrentPreset.Text = $"{LtAmpDevice.DeviceInfo.ActivePresetIndex.ToString()}: {((Preset)presetBindingSource.Current).FormattedDisplayName}";
                    }));
                    break;
                case "LtDeviceInfo.IsPresetEdited":
                    this.Invoke(new MethodInvoker(delegate
                    {
                        statusLabelCurrentPreset.ForeColor = LtAmpDevice.DeviceInfo.IsPresetEdited ? Color.Red : Color.FromKnownColor(KnownColor.ControlText);
                    }));
                    break;

            }
        }

        private void Amp_DeviceConnected(object sender, EventArgs e)
        {
            amp.GetAllPresets();
            BindPresets();
            statusLabelConnectionStatus.Text = "Connected";
            statusLabelConnectionStatus.BackColor = Color.Green;
        }

        private void vUMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vUMeterToolStripMenuItem.Checked = !vUMeterToolStripMenuItem.Checked;
            panelVuMeter.Visible = vUMeterToolStripMenuItem.Checked;
        }
    }
}