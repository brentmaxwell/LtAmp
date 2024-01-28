using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib
{
    public delegate void DeviceConnectedEventHandler(object sender, EventArgs e);
    public delegate void DeviceDisconnectedEventHandler(object sender, EventArgs e);
    public delegate void MessageReceivedEventHandler(FenderMessageLT message);
    public delegate void MessageSentEventHandler(FenderMessageLT message);
    public delegate void UnknownMessageReceivedEventHandler(FenderMessageLT message);

    public delegate void ActiveDisplayHandler(ActiveDisplay message);
    public delegate void AuditionPresetStatusHandler(AuditionPresetStatus message);
    public delegate void AuditionStateStatusHandler(AuditionStateStatus message);
    public delegate void ClearPresetStatusHandler(ClearPresetStatus message);
    public delegate void ConnectionStatusHandler(ConnectionStatus message);
    public delegate void CurrentDisplayedPresetIndexStatusHandler(CurrentDisplayedPresetIndexStatus message);
    public delegate void CurrentLoadedPresetIndexBypassStatusHandler(CurrentLoadedPresetIndexBypassStatus message);
    public delegate void CurrentLoadedPresetIndexStatusHandler(CurrentLoadedPresetIndexStatus message);
    public delegate void CurrentPresetRequestHandler(CurrentPresetRequest message);
    public delegate void CurrentPresetSetHandler(CurrentPresetSet message);
    public delegate void CurrentPresetStatusHandler(CurrentPresetStatus message);
    public delegate void DspUnitParameterStatusHandler(DspUnitParameterStatus message);
    public delegate void ExitAuditionPresetStatusHandler(ExitAuditionPresetStatus message);
    public delegate void FirmwareVersionStatusHandler(FirmwareVersionStatus message);
    public delegate void FrameBufferMessageHandler(FrameBufferMessage message);
    public delegate void HeartbeatHandler(Heartbeat message);
    public delegate void IndexButtonHandler(IndexButton message);
    public delegate void IndexEncoderHandler(IndexEncoder message);
    public delegate void IndexPotHandler(IndexPot message);
    public delegate void LT4FootswitchModeStatusHandler(LT4FootswitchModeStatus message);
    public delegate void LineOutGainStatusHandler(LineOutGainStatus message);
    public delegate void LoadPreset_TestSuiteHandler(LoadPreset_TestSuite message);
    public delegate void LoopbackTestHandler(LoopbackTest message);
    public delegate void MemoryUsageStatusHandler(MemoryUsageStatus message);
    
    public delegate void ModalStatusMessageHandler(ModalStatusMessage message);
    public delegate void NewPresetSavedStatusHandler(NewPresetSavedStatus message);
    public delegate void PresetEditedStatusHandler(PresetEditedStatus message);
    public delegate void PresetJSONMessageHandler(PresetJSONMessage message);
    public delegate void PresetSavedStatusHandler(PresetSavedStatus message);
    public delegate void ProcessorUtilizationHandler(ProcessorUtilization message);
    public delegate void ProcessorUtilizationRequestHandler(ProcessorUtilizationRequest message);
    public delegate void ProductIdentificationRequestHandler(ProductIdentificationRequest message);
    public delegate void ProductIdentificationStatusHandler(ProductIdentificationStatus message);
    public delegate void QASlotsStatusHandler(QASlotsStatus message);
    public delegate void RenamePresetAtHandler(RenamePresetAt message);
    public delegate void ReplaceNodeHandler(ReplaceNode message);
    public delegate void ReplaceNodeStatusHandler(ReplaceNodeStatus message);
    public delegate void SetDspUnitParameterStatusHandler(SetDspUnitParameterStatus message);
    public delegate void ShiftPresetStatusHandler(ShiftPresetStatus message);
    public delegate void SwapPresetStatusHandler(SwapPresetStatus message);
    public delegate void UnsupportedMessageStatusHandler(UnsupportedMessageStatus message);
    public delegate void UsbGainStatusHandler(UsbGainStatus message);
}
