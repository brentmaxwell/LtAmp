using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Models.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LtAmpDotNet.Lib.Models.Protobuf.FenderMessageLT;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmplifier
    {
        public event EventHandler? AmplifierConnected;
        public event EventHandler? AmplifierDisconnected;
        public event EventHandler<FenderMessageEventArgs>? MessageReceived;
        public event EventHandler<FenderMessageEventArgs>? MessageSent;
        public event EventHandler<FenderMessageEventArgs>? UnknownMessageReceived;
        
        public event EventHandler<FenderMessageEventArgs>? ActiveDisplayMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? AuditionPresetStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? AuditionStateStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ClearPresetStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ConnectionStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? CurrentDisplayedPresetIndexStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? CurrentLoadedPresetIndexBypassStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? CurrentLoadedPresetIndexStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? CurrentPresetStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? DspUnitParameterStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ExitAuditionPresetStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? FirmwareVersionStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? FrameBufferMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? HeartbeatMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? IndexButtonMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? IndexEncoderMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? IndexPotMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? LT4FootswitchModeStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? LineOutGainStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? LoadPreset_TestSuiteMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? LoopbackTestMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? MemoryUsageStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ModalStatusMessageMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? NewPresetSavedStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? PresetEditedStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? PresetJSONMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? PresetSavedStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ProcessorUtilizationMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ProductIdentificationStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? QASlotsStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ReplaceNodeStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? SetDspUnitParameterStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? ShiftPresetStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? SwapPresetStatusMessageReceived;
        public event EventHandler<FenderMessageEventArgs>? UnsupportedMessageStatusReceived;
        public event EventHandler<FenderMessageEventArgs>? UsbGainStatusMessageReceived;
    }
}
