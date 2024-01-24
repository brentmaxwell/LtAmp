using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmpDevice
    {
        #region Library events

        public delegate void DeviceConnectedEventHandler(object sender, EventArgs e);
        public event DeviceConnectedEventHandler? DeviceConnected;

        public delegate void DeviceDisconnectedEventHandler(object sender, EventArgs e);
        public event DeviceDisconnectedEventHandler? DeviceDisconnected;

        public delegate void MessageSentEventHandler(FenderMessageLT message);
        public event MessageSentEventHandler? MessageSent;
        
        public delegate void MessageReceivedEventHandler(FenderMessageLT message);
        public event MessageReceivedEventHandler? MessageReceived;

        public delegate void UnknownMessageReceivedEventHandler(FenderMessageLT message);
        public event UnknownMessageReceivedEventHandler? UnknownMessageReceived;

        #endregion

        #region Known amplifier messages

        public delegate void AuditionPresetStatusHandler(AuditionPresetStatus message);
        public event AuditionPresetStatusHandler? AuditionPresetStatusMessageReceived;

        public delegate void AuditionStateStatusHandler(AuditionStateStatus message);
        public event AuditionStateStatusHandler? AuditionStateStatusMessageReceived;

        public delegate void ClearPresetStatusHandler(ClearPresetStatus message);
        public event ClearPresetStatusHandler? ClearPresetStatusMessageReceived;

        public delegate void ConnectionStatusHandler(ConnectionStatus message);
        public event ConnectionStatusHandler? ConnectionStatusMessageReceived;

        public delegate void CurrentDisplayedPresetIndexStatusHandler(CurrentDisplayedPresetIndexStatus message);
        public event CurrentDisplayedPresetIndexStatusHandler? CurrentDisplayedPresetIndexStatusMessageReceived;

        public delegate void CurrentLoadedPresetIndexBypassStatusHandler(CurrentLoadedPresetIndexBypassStatus message);
        public event CurrentLoadedPresetIndexBypassStatusHandler? CurrentLoadedPresetIndexBypassStatusMessageReceived;

        public delegate void CurrentLoadedPresetIndexStatusHandler(CurrentLoadedPresetIndexStatus message);
        public event CurrentLoadedPresetIndexStatusHandler? CurrentLoadedPresetIndexStatusMessageReceived;

        public delegate void CurrentPresetRequestHandler(CurrentPresetRequest message);
        public delegate void CurrentPresetSetHandler(CurrentPresetSet message);

        public delegate void CurrentPresetStatusHandler(CurrentPresetStatus message);
        public event CurrentPresetStatusHandler? CurrentPresetStatusMessageReceived;

        public delegate void DspUnitParameterStatusHandler(DspUnitParameterStatus message);
        public event DspUnitParameterStatusHandler? DspUnitParameterStatusMessageReceived;

        public delegate void ExitAuditionPresetStatusHandler(ExitAuditionPresetStatus message);
        public event ExitAuditionPresetStatusHandler? ExitAuditionPresetStatusMessageReceived;

        public delegate void FirmwareVersionStatusHandler(FirmwareVersionStatus message);
        public event FirmwareVersionStatusHandler? FirmwareVersionStatusMessageReceived;

        public delegate void HeartbeatHandler(Heartbeat message);
        public event HeartbeatHandler? HeartbeatMessageReceived;

        public delegate void MemoryUsageStatusHandler(MemoryUsageStatus message);
        public event MemoryUsageStatusHandler? MemoryUsageStatusMessageReceived;

        public delegate void ModalStatusMessageHandler(ModalStatusMessage message);
        public event ModalStatusMessageHandler? ModalStatusMessageMessageReceived;

        public delegate void NewPresetSavedStatusHandler(NewPresetSavedStatus message);
        public event NewPresetSavedStatusHandler? NewPresetSavedStatusMessageReceived;

        public delegate void PresetEditedStatusHandler(PresetEditedStatus message);
        public event PresetEditedStatusHandler? PresetEditedStatusMessageReceived;

        public delegate void PresetJSONMessageHandler(PresetJSONMessage message);
        public event PresetJSONMessageHandler? PresetJSONMessageReceived;

        public delegate void PresetSavedStatusHandler(PresetSavedStatus message);
        public event PresetSavedStatusHandler? PresetSavedStatusMessageReceived;

        public delegate void ProcessorUtilizationHandler(ProcessorUtilization message);
        public event ProcessorUtilizationHandler? ProcessorUtilizationMessageReceived;

        public delegate void ProcessorUtilizationRequestHandler(ProcessorUtilizationRequest message);
        public delegate void ProductIdentificationRequestHandler(ProductIdentificationRequest message);

        public delegate void ProductIdentificationStatusHandler(ProductIdentificationStatus message);
        public event ProductIdentificationStatusHandler? ProductIdentificationStatusMessageReceived;

        public delegate void QASlotsStatusHandler(QASlotsStatus message);
        public event QASlotsStatusHandler? QASlotsStatusMessageReceived;

        public delegate void RenamePresetAtHandler(RenamePresetAt message);
        public delegate void ReplaceNodeHandler(ReplaceNode message);

        public delegate void ReplaceNodeStatusHandler(ReplaceNodeStatus message);
        public event ReplaceNodeStatusHandler? ReplaceNodeStatusMessageReceived;

        public delegate void SetDspUnitParameterStatusHandler(SetDspUnitParameterStatus message);
        public event SetDspUnitParameterStatusHandler? SetDspUnitParameterStatusMessageReceived;

        public delegate void ShiftPresetStatusHandler(ShiftPresetStatus message);
        public event ShiftPresetStatusHandler? ShiftPresetStatusMessageReceived;

        public delegate void SwapPresetStatusHandler(SwapPresetStatus message);
        public event SwapPresetStatusHandler? SwapPresetStatusMessageReceived;

        public delegate void UnsupportedMessageStatusHandler(UnsupportedMessageStatus message);
        public event UnsupportedMessageStatusHandler? UnsupportedMessageStatusReceived;

        public delegate void UsbGainStatusHandler(UsbGainStatus message);
        public event UsbGainStatusHandler? UsbGainStatusMessageReceived;

        #endregion

        #region Unknown messages

        public delegate void ActiveDisplayHandler(ActiveDisplay message);
        public event ActiveDisplayHandler? ActiveDisplayMessageReceived;
        
        public delegate void IndexButtonHandler(IndexButton message);
        public event IndexButtonHandler? IndexButtonMessageReceived;
        
        public delegate void IndexEncoderHandler(IndexEncoder message);
        public event IndexEncoderHandler? IndexEncoderMessageReceived;
        
        public delegate void IndexPotHandler(IndexPot message);
        public event IndexPotHandler? IndexPotMessageReceived;

        public delegate void FrameBufferMessageHandler(FrameBufferMessage message);
        public event FrameBufferMessageHandler? FrameBufferMessageReceived;
        
        public delegate void LT4FootswitchModeStatusHandler(LT4FootswitchModeStatus message);
        public event LT4FootswitchModeStatusHandler? LT4FootswitchModeStatusMessageReceived;

        public delegate void LineOutGainStatusHandler(LineOutGainStatus message);
        public event LineOutGainStatusHandler? LineOutGainStatusMessageReceived;

        public delegate void LoadPreset_TestSuiteHandler(LoadPreset_TestSuite message);
        public event LoadPreset_TestSuiteHandler? LoadPreset_TestSuiteMessageReceived;
        
        public delegate void LoopbackTestHandler(LoopbackTest message);
        public event LoopbackTestHandler? LoopbackTestMessageReceived;

        #endregion
    }
}
