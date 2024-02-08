using LtAmpDotNet.Base;
using LtAmpDotNet.Extensions;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;
using System.ComponentModel;

namespace LtAmpDotNet
{
    public partial class MainForm : Form
    {
        private readonly LtAmplifier amp = new(new MockHidDevice(MockDeviceState.Load()));
        private readonly MainFormViewModel viewModel = new();

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
            viewModelBindingSource.ListChanged += ViewModelBindingSource_ListChanged;
        }

        private void SubscribeToEvents()
        {
            amp.AmplifierConnected += Amp_DeviceConnected;
            amp.AmplifierDisconnected += Amp_DeviceDisconnected;
            amp.PresetJSONMessageReceived += Amp_PresetJSONMessageReceived;
            amp.QASlotsStatusMessageReceived += Amp_QASlotsStatusMessageReceived;
        }

        #region control creation

        private void createToolStripPresetList()
        {
            toolStripPresetList.DropDownItems.Clear();
            for (int i = 0; i < viewModel.Presets.Count; i++)
            {
                ToolStripMenuItem menuItem = new($"{i}: {viewModel.Presets[i].FormattedDisplayName}")
                {
                    Tag = i
                };
                menuItem.Click += presetItemChanged;
                toolStripPresetList.DropDownItems.Add(menuItem);
            }
        }

        #endregion

        #region Events

        #region amplifier events

        private void Amp_DeviceConnected(object? sender, EventArgs e)
        {
            viewModel.ValueChanged += viewModel_ValueChanged;
            amp.GetAllPresets();
            amp.GetQASlots();
            BindData();
            viewModel.ConnectionStatus = true;
        }

        private void Amp_DeviceDisconnected(object? sender, EventArgs e)
        {
            viewModel.ConnectionStatus = false;
        }

        private void Amp_QASlotsStatusMessageReceived(object? sender, FenderMessageEventArgs eventArgs)
        {
            viewModel.FootswitchPresets = eventArgs.Message.QASlotsStatus.Slots.ToArray();
        }

        private void Amp_PresetJSONMessageReceived(object? sender, FenderMessageEventArgs eventArgs)
        {
            viewModel.Presets[eventArgs.Message.PresetJSONMessage.SlotIndex] = Preset.FromString(eventArgs.Message.PresetJSONMessage.Data);
        }

        #endregion

        #region data source events

        private void ViewModelBindingSource_ListChanged(object? sender, ListChangedEventArgs e)
        {
            createToolStripPresetList();
        }

        private void viewModelBindingSource_DataSourceChanged(object? sender, EventArgs e)
        {
            createToolStripPresetList();
        }


        private void viewModel_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "MainFormViewModel.ConnectionStatus":
                    statusLabelConnectionStatus.Text = viewModel.ConnectionStatus ? "Connected" : "Disconnected";
                    statusLabelConnectionStatus.BackColor = viewModel.ConnectionStatus ? Color.Green : Color.FromKnownColor(KnownColor.Control);
                    break;
                case "MainFormViewModel.CurrentPresetIndex":
                    ((ToolStripMenuItem)toolStripPresetList.DropDownItems[(int)e.PreviousValue]).Checked = false;
                    setControlsToCurrentPresetIndex(viewModel.CurrentPresetIndex);
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

        #region menu events

        private void menuItemExport_Click(object sender, EventArgs e)
        {
            exportFileDialog.FileName = $"{viewModel.CurrentPreset.FormattedDisplayName}.json";
            if (exportFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(exportFileDialog.FileName, viewModel.CurrentPreset.ToString());
            }
        }

        private void menuItemImport_Click(object sender, EventArgs e)
        {
            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                string presetData = File.ReadAllText(importFileDialog.FileName);
                viewModel.CurrentPreset = Preset.FromString(presetData);
                amp.SetCurrentPreset(viewModel.CurrentPreset);
                amp.SaveCurrentPreset();
            }
        }

        private void menuItemVuMeter_Click(object sender, EventArgs e)
        {
            vUMeterToolStripMenuItem.Checked = !vUMeterToolStripMenuItem.Checked;
            panelVuMeter.Visible = vUMeterToolStripMenuItem.Checked;
        }

        #endregion

        private void presetItemChanged(object? sender, EventArgs e)
        {
            switch (sender)
            {
                case ToolStripMenuItem:
                    viewModel.CurrentPresetIndex = (int)((ToolStripMenuItem)sender).Tag!;
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

        private void setControlsToCurrentPresetIndex(int index)
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
                currentPresetPanel1.ViewModel = viewModel.CurrentPresetViewModel;
            }
        }

        #endregion


    }
}