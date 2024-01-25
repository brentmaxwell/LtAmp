using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;
using LtAmpDotNet.Tests.Mock;
using LtAmpDotNet.Tests;
using LtAmpDotNet.Extensions;
using System;

namespace LtAmpDotNet
{
    public partial class MainForm : Form
    {
        private LtAmpDevice amp = new LtAmpDevice(new MockHidDevice(MockDeviceState.Load()));
        private MainFormViewModel viewModel = new MainFormViewModel();

        

        public MainForm()
        {
            InitializeComponent();
            SubscribeToEvents();
            amp.Open();
        }
        
        private void BindData()
        {
            createToolStripPresetList();
            viewModelBindingSource.DataSource = viewModel;
            viewModelBindingSource.CurrentChanged += presetItemChanged;
            viewModelBindingSource.DataSourceChanged += viewModelBindingSource_DataSourceChanged;
        }

        private void SubscribeToEvents()
        {
            amp.DeviceConnected += Amp_DeviceConnected;
            amp.DeviceDisconnected += Amp_DeviceDisconnected;
            amp.PresetJSONMessageReceived += Amp_PresetJSONMessageReceived;
            amp.QASlotsStatusMessageReceived += Amp_QASlotsStatusMessageReceived;
        }

        #region control creation

        private void createToolStripPresetList()
        {
            toolStripPresetList.DropDownItems.Clear();
            for (var i = 0; i < viewModel.Presets.Count; i++)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem($"{i}: {viewModel.Presets[i].FormattedDisplayName}");
                menuItem.Tag = i;
                menuItem.Click += presetItemChanged;
                toolStripPresetList.DropDownItems.Add(menuItem);
            }
        }

        #endregion

        #region Events

        #region amplifier events

        private void Amp_DeviceConnected(object sender, EventArgs e)
        {
            viewModel.BeforePropertyChanged += viewModel_BeforePropertyChanged;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
            amp.GetAllPresets();
            amp.GetQASlots();
            BindData();
            viewModel.ConnectionStatus = true;
        }

        private void Amp_DeviceDisconnected(object sender, EventArgs e)
        {
            viewModel.ConnectionStatus = false;
        }

        private void Amp_QASlotsStatusMessageReceived(QASlotsStatus message)
        {
            viewModel.FootswitchPresets = message.Slots.ToArray();
        }

        private void Amp_PresetJSONMessageReceived(PresetJSONMessage message)
        {
            viewModel.Presets[message.SlotIndex] = Preset.FromString(message.Data);
        }

        #endregion

        #region data source events

        private void viewModelBindingSource_DataSourceChanged(object? sender, EventArgs e)
        {
            createToolStripPresetList();
        }

        private void viewModel_BeforePropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "MainFormViewModel.CurrentPresetIndex":
                    ((ToolStripMenuItem)toolStripPresetList.DropDownItems[viewModel.CurrentPresetIndex]).Checked = false;
                    break;
            }
        }

        private void viewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "MainFormViewModel.ConnectionStatus":
                    statusLabelConnectionStatus.Text = viewModel.ConnectionStatus ? "Connected" : "Disconnected";
                    statusLabelConnectionStatus.BackColor = viewModel.ConnectionStatus ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                    break;
                case "MainFormViewModel.CurrentPresetIndex":
                    setCurrentPreset(viewModel.CurrentPresetIndex);
                    currentPresetPanel1.ViewModel = viewModel.CurrentPresetViewModel;
                    break;
                case "MainFormViewModel.FootswitchPresets":
                    statusLabelFootSwitch.Text = $"[ {viewModel.FootswitchPresets[0]}: {viewModel.Presets[(int)viewModel.FootswitchPresets[0]].FormattedDisplayName} , {viewModel.FootswitchPresets[1]}: {viewModel.Presets[(int)viewModel.FootswitchPresets[1]].FormattedDisplayName} ]";
                    break;
                case "LtDeviceInfo.IsPresetEdited":
                    toolStripPresetList.ForeColor = viewModel.IsPresetEdited ? Color.Red : Color.FromKnownColor(KnownColor.ControlText);
                    break;
            }
        }

        #endregion

        #region control events

        private void vUMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vUMeterToolStripMenuItem.Checked = !vUMeterToolStripMenuItem.Checked;
            panelVuMeter.Visible = vUMeterToolStripMenuItem.Checked;
        }

        private void presetItemChanged(object? sender, EventArgs e)
        {
            switch(sender)
            {
                case ToolStripMenuItem:
                    viewModel.CurrentPresetIndex = (int)((ToolStripMenuItem)sender).Tag;
                    break;
                case ListBox:
                    viewModel.CurrentPresetIndex = listBoxPresets.SelectedIndex;
                    break;
                case BindingSource:
                    viewModel.CurrentPresetIndex = viewModelBindingSource.Position;
                    break;
            }
        }

        #endregion

        private void setCurrentPreset(int index)
        {
            
            listBoxPresets.TryInvoke(new MethodInvoker(delegate
            {
                listBoxPresets.SelectedIndex = index;
            }));
            ((ToolStripMenuItem)toolStripPresetList.DropDownItems[index]).Checked = true;
            toolStripPresetList.Text = $"{index}: {viewModel.Presets[index].FormattedDisplayName}";
            if (index > 0)
            {
                viewModel.CurrentPresetViewModel = new CurrentPresetPanelViewModel(viewModel.Presets[viewModel.CurrentPresetIndex]);
            }
        }

        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = $"{viewModel.CurrentPreset.FormattedDisplayName}.json";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, viewModel.CurrentPreset.ToString());
            }
        }
    }
}