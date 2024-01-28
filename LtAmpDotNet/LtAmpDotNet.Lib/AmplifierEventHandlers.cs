using LtAmpDotNet.Lib.Models.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmpDevice
    {
        public event DeviceConnectedEventHandler? DeviceConnected;
        public event DeviceDisconnectedEventHandler? DeviceDisconnected;
        public event MessageReceivedEventHandler? MessageReceived;
        public event MessageSentEventHandler? MessageSent;
        public event UnknownMessageReceivedEventHandler? UnknownMessageReceived;
        
        public event ActiveDisplayHandler? ActiveDisplayMessageReceived;
        public event AuditionPresetStatusHandler? AuditionPresetStatusMessageReceived;
        public event AuditionStateStatusHandler? AuditionStateStatusMessageReceived;
        public event ClearPresetStatusHandler? ClearPresetStatusMessageReceived;
        public event ConnectionStatusHandler? ConnectionStatusMessageReceived;
        public event CurrentDisplayedPresetIndexStatusHandler? CurrentDisplayedPresetIndexStatusMessageReceived;
        public event CurrentLoadedPresetIndexBypassStatusHandler? CurrentLoadedPresetIndexBypassStatusMessageReceived;
        public event CurrentLoadedPresetIndexStatusHandler? CurrentLoadedPresetIndexStatusMessageReceived;
        public event CurrentPresetStatusHandler? CurrentPresetStatusMessageReceived;
        public event DspUnitParameterStatusHandler? DspUnitParameterStatusMessageReceived;
        public event ExitAuditionPresetStatusHandler? ExitAuditionPresetStatusMessageReceived;
        public event FirmwareVersionStatusHandler? FirmwareVersionStatusMessageReceived;
        public event FrameBufferMessageHandler? FrameBufferMessageReceived;
        public event HeartbeatHandler? HeartbeatMessageReceived;
        public event IndexButtonHandler? IndexButtonMessageReceived;
        public event IndexEncoderHandler? IndexEncoderMessageReceived;
        public event IndexPotHandler? IndexPotMessageReceived;
        public event LT4FootswitchModeStatusHandler? LT4FootswitchModeStatusMessageReceived;
        public event LineOutGainStatusHandler? LineOutGainStatusMessageReceived;
        public event LoadPreset_TestSuiteHandler? LoadPreset_TestSuiteMessageReceived;
        public event LoopbackTestHandler? LoopbackTestMessageReceived;
        public event MemoryUsageStatusHandler? MemoryUsageStatusMessageReceived;
        public event ModalStatusMessageHandler? ModalStatusMessageMessageReceived;
        public event NewPresetSavedStatusHandler? NewPresetSavedStatusMessageReceived;
        public event PresetEditedStatusHandler? PresetEditedStatusMessageReceived;
        public event PresetJSONMessageHandler? PresetJSONMessageReceived;
        public event PresetSavedStatusHandler? PresetSavedStatusMessageReceived;
        public event ProcessorUtilizationHandler? ProcessorUtilizationMessageReceived;
        public event ProductIdentificationStatusHandler? ProductIdentificationStatusMessageReceived;
        public event QASlotsStatusHandler? QASlotsStatusMessageReceived;
        public event ReplaceNodeStatusHandler? ReplaceNodeStatusMessageReceived;
        public event SetDspUnitParameterStatusHandler? SetDspUnitParameterStatusMessageReceived;
        public event ShiftPresetStatusHandler? ShiftPresetStatusMessageReceived;
        public event SwapPresetStatusHandler? SwapPresetStatusMessageReceived;
        public event UnsupportedMessageStatusHandler? UnsupportedMessageStatusReceived;
        public event UsbGainStatusHandler? UsbGainStatusMessageReceived;
    }
}
