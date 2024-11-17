using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib
{
    public interface ILtAmplifier
    {
        ErrorType? ErrorType { get; set; }
        bool IsOpen { get; }

        void Open(bool continueTry = true);

        Task OpenAsync(bool continueTry = true);

        void Close();

        void Dispose();

        event EventHandler<FenderMessageEventArgs>? ActiveDisplayMessageReceived;

        event EventHandler? AmplifierConnected;

        event EventHandler? AmplifierDisconnected;

        event EventHandler<FenderMessageEventArgs>? AuditionPresetStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? AuditionStateStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ClearPresetStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ConnectionStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? CurrentDisplayedPresetIndexStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? CurrentLoadedPresetIndexBypassStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? CurrentLoadedPresetIndexStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? CurrentPresetStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? DspUnitParameterStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ExitAuditionPresetStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? FirmwareVersionStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? FrameBufferMessageReceived;

        event EventHandler<FenderMessageEventArgs>? HeartbeatMessageReceived;

        event EventHandler<FenderMessageEventArgs>? IndexButtonMessageReceived;

        event EventHandler<FenderMessageEventArgs>? IndexEncoderMessageReceived;

        event EventHandler<FenderMessageEventArgs>? IndexPotMessageReceived;

        event EventHandler<FenderMessageEventArgs>? LineOutGainStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? LoadPreset_TestSuiteMessageReceived;

        event EventHandler<FenderMessageEventArgs>? LoopbackTestMessageReceived;

        event EventHandler<FenderMessageEventArgs>? LT4FootswitchModeStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? MemoryUsageStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? MessageReceived;

        event EventHandler<FenderMessageEventArgs>? MessageSent;

        event EventHandler<FenderMessageEventArgs>? ModalStatusMessageMessageReceived;

        event EventHandler<FenderMessageEventArgs>? NewPresetSavedStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? PresetEditedStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? PresetJSONMessageReceived;

        event EventHandler<FenderMessageEventArgs>? PresetSavedStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ProcessorUtilizationMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ProductIdentificationStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? QASlotsStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ReplaceNodeStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? SetDspUnitParameterStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? ShiftPresetStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? SwapPresetStatusMessageReceived;

        event EventHandler<FenderMessageEventArgs>? UnknownMessageReceived;

        event EventHandler<FenderMessageEventArgs>? UnsupportedMessageStatusReceived;

        event EventHandler<FenderMessageEventArgs>? UsbGainStatusMessageReceived;

        void ActiveDisplay(string pageName);

        void ClearPreset(int slotIndex, bool isLoadPreset = true);

        Task<ClearPresetStatus> ClearPresetAsync(int slotIndex, bool isLoadPreset = true);

        void ExitAuditionPreset(bool exitStatus = true);

        void GetAuditionState();

        void GetConnectionStatus();

        Task<ConnectionStatus> GetConnectionStatusAsync();

        void GetCurrentPreset();

        Task<CurrentPresetStatus> GetCurrentPresetAsync();

        void GetFirmwareVersion();

        Task<FirmwareVersionStatus> GetFirmwareVersionAsync();

        void GetFramebuffer();

        void GetLineOutGain();

        void GetLt4FootswitchMode();

        void GetMemoryUsage();

        Task<MemoryUsageStatus> GetMemoryUsageAsync();

        void GetPreset(int slotIndex);

        Task<PresetJSONMessage> GetPresetAsync(int slotIndex);

        void GetPresetLT(int request);

        void GetProcessorUtilization();

        Task<ProcessorUtilization> GetProcessorUtilizationAsync();

        void GetProductIdentification();

        Task<ProductIdentificationStatus> GetProductIdentificationAsync();

        void GetQASlots();

        Task<QASlotsStatus> GetQASlotsAsync();

        void GetUsbGain();

        Task<UsbGainStatus> GetUsbGainAsync();

        void Heartbeat();

        void LoadPreset(int slotIndex);

        Task<CurrentLoadedPresetIndexStatus> LoadPresetAsync(int slotIndex);

        void LoopbackTest(string data);

        void RenamePresetAt(int slotIndex, string name);

        Task<PresetSavedStatus> RenamePresetAtAsync(int slotIndex, string name);

        void ReplaceNode(NodeIdType nodeId, string fenderId);

        Task<ReplaceNodeStatus> ReplaceNodeAsync(NodeIdType nodeId, string fenderId);

        void SaveCurrentPreset();

        Task<PresetSavedStatus> SaveCurrentPresetAsync();

        void SaveCurrentPresetTo(int slotIndex, string name);

        Task<PresetSavedStatus> SaveCurrentPresetToAsync(int slotIndex, string name);

        void SavePresetAs(int slotIndex, Preset preset, bool loadPreset = true);

        Task<PresetSavedStatus> SavePresetAsAsync(int slotIndex, Preset preset, bool loadPreset = true);

        void SendMessage(FenderMessageLT message);

        Task<FenderMessageLT> SendMessageAsync(FenderMessageLT message, FenderMessageLT.TypeOneofCase responseMessage = FenderMessageLT.TypeOneofCase.None);

        void SetAuditionPreset(Preset preset);

        void SetCurrentPreset(Preset preset);

        Task<CurrentPresetStatus> SetCurrentPresetAsync(Preset preset);

        void SetDspUnitParameter(NodeIdType nodeId, DspUnitParameter parameter);

        Task<SetDspUnitParameterStatus> SetDspUnitParameterAsync(NodeIdType nodeId, DspUnitParameter parameter);

        void SetLineOutGain(float value);

        void SetModalState(ModalContext modalContext);

        Task<ModalStatusMessage> SetModalStateAsync(ModalContext modalContext);

        void SetQASlots(uint[] slotIndexes);

        Task<QASlotsStatus> SetQASlotsAsync(uint[] slotIndexes);

        void SetUsbGain(float value);

        Task<UsbGainStatus> SetUsbGainAsync(float value);

        void ShiftPreset(int from, int to);

        Task<ShiftPresetStatus> ShiftPresetAsync(int from, int to);

        void SwapPreset(int indexA, int indexB);

        Task<SwapPresetStatus> SwapPresetAsync(int indexA, int indexB);

        void SetTuner(bool on_off);

        Task<ModalStatusMessage> SetTunerAsync(bool on_off);

        void GetAllPresets();

        Task<List<PresetJSONMessage>> GetAllPresetsAsync();
    }
}