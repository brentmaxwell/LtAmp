using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib.Events
{
    public delegate void MessageReceivedEventHandler(object sender, FenderMessageEventArgs e);
    public delegate void MessageSentEventHandler(object sender, FenderMessageEventArgs e);
    public delegate void UnknownMessageReceivedEventHandler(object sender, FenderMessageEventArgs e);

    public delegate void ActiveDisplayHandler(object sender, FenderMessageEventArgs e);
    public delegate void AuditionPresetStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void AuditionStateStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void ClearPresetStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void ConnectionStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void CurrentDisplayedPresetIndexStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void CurrentLoadedPresetIndexBypassStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void CurrentLoadedPresetIndexStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void CurrentPresetRequestHandler(object sender, FenderMessageEventArgs e);
    public delegate void CurrentPresetSetHandler(object sender, FenderMessageEventArgs e);
    public delegate void CurrentPresetStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void DspUnitParameterStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void ExitAuditionPresetStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void FirmwareVersionStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void FrameBufferMessageHandler(object sender, FenderMessageEventArgs e);
    public delegate void HeartbeatHandler(object sender, FenderMessageEventArgs e);
    public delegate void IndexButtonHandler(object sender, FenderMessageEventArgs e);
    public delegate void IndexEncoderHandler(object sender, FenderMessageEventArgs e);
    public delegate void IndexPotHandler(object sender, FenderMessageEventArgs e);
    public delegate void LT4FootswitchModeStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void LineOutGainStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void LoadPreset_TestSuiteHandler(object sender, FenderMessageEventArgs e);
    public delegate void LoopbackTestHandler(object sender, FenderMessageEventArgs e);
    public delegate void MemoryUsageStatusHandler(object sender, FenderMessageEventArgs e);
    
    public delegate void ModalStatusMessageHandler(object sender, FenderMessageEventArgs e);
    public delegate void NewPresetSavedStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void PresetEditedStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void PresetJSONMessageHandler(object sender, FenderMessageEventArgs e);
    public delegate void PresetSavedStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void ProcessorUtilizationHandler(object sender, FenderMessageEventArgs e);
    public delegate void ProcessorUtilizationRequestHandler(object sender, FenderMessageEventArgs e);
    public delegate void ProductIdentificationRequestHandler(object sender, FenderMessageEventArgs e);
    public delegate void ProductIdentificationStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void QASlotsStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void RenamePresetAtHandler(object sender, FenderMessageEventArgs e);
    public delegate void ReplaceNodeHandler(object sender, FenderMessageEventArgs e);
    public delegate void ReplaceNodeStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void SetDspUnitParameterStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void ShiftPresetStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void SwapPresetStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void UnsupportedMessageStatusHandler(object sender, FenderMessageEventArgs e);
    public delegate void UsbGainStatusHandler(object sender, FenderMessageEventArgs e);
}
