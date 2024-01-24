using Google.Protobuf.Compiler;
using Google.Protobuf.WellKnownTypes;
using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib
{
    public class LtDeviceInfo : ObservableAmpData
    {
        public const int VENDOR_ID = 0x1ed8;
        public const int PRODUCT_ID = 0x0037;
        public const int NUM_OF_PRESETS = 60;
        public bool IsConnected { get; set; }
        public string ProductId { get; set; }
        public string FirmwareVersion { get; set; }
        public ProcessorUtilization ProcessorUtilization { get; set; }
        public MemoryUsageStatus MemoryUsageStatus { get; set; }
        public ModalContext ModalContext { get; set; }
        public ModalState ModalState { get; set; }
        public int DisplayedPresetIndex { get; set; }

        private int _activePresetIndex;
        public int ActivePresetIndex
        {
            get => _activePresetIndex;
            set => SetProperty(ref _activePresetIndex, value);
        }

        public Preset CurrentPreset
        {
            get => Presets[ActivePresetIndex];
            set => Presets[ActivePresetIndex] = value;
        }

        private bool _isPresetEdited;
        public bool IsPresetEdited
        {
            get => _isPresetEdited;
            set => SetProperty(ref _isPresetEdited, value);
        }
        public float UsbGain { get; set; }
        public uint[] FootswitchPresets { get; set; }
        public bool IsAuditioning { get; set; }
        public Preset AuditioningPreset { get; set; }
        public List<Preset> Presets { get; set; }
    }
}
