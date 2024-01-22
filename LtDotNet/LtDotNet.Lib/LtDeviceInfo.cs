using LtDotNet.Lib.Model;
using LtDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtDotNet.Lib
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
        public int ActivePresetIndex { get; set; }
        public Preset CurrentPreset { get; set; }
        public bool IsPresetEdited { get; set; }
        public float UsbGain { get; set; }
        public uint[] FootswitchPresets { get; set; }
        public bool IsAuditioning { get; set; }
        public Preset AuditioningPreset { get; set; }
        public List<Preset> Presets { get; set; }
    }
}
