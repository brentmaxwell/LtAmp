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


        private Dictionary<FenderMessageLT.TypeOneofCase, Action<FenderMessageEventArgs>> MessageEventHandlers = 
            new Dictionary<FenderMessageLT.TypeOneofCase, Action<FenderMessageEventArgs>>();
        private void SetupEventHandlers()
        {
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.AuditionPresetStatus,
                (eventArgs) => AuditionPresetStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.AuditionStateStatus,
                (eventArgs) => AuditionStateStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ClearPresetStatus,
                (eventArgs) => ClearPresetStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ConnectionStatus,
                (eventArgs) => ConnectionStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.CurrentDisplayedPresetIndexStatus,
                (eventArgs) => CurrentDisplayedPresetIndexStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexStatus,
                (eventArgs) => CurrentLoadedPresetIndexStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexBypassStatus,
                (eventArgs) => CurrentLoadedPresetIndexBypassStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.CurrentPresetStatus,
                (eventArgs) => CurrentPresetStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.DspUnitParameterStatus,
                (eventArgs) => DspUnitParameterStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ExitAuditionPresetStatus,
                (eventArgs) => ExitAuditionPresetStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.FirmwareVersionStatus,
                (eventArgs) => FirmwareVersionStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.FrameBufferMessage,
                (eventArgs) => FrameBufferMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.IndexButton,
                (eventArgs) => IndexButtonMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.IndexEncoder,
                (eventArgs) => IndexEncoderMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.IndexPot,
                (eventArgs) => IndexPotMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.LineOutGainStatus,
                (eventArgs) => LineOutGainStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.Lt4FootswitchModeStatus,
                (eventArgs) => LT4FootswitchModeStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.MemoryUsageStatus,
                (eventArgs) => MemoryUsageStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ModalStatusMessage,
                (eventArgs) => ModalStatusMessageMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.NewPresetSavedStatus,
                (eventArgs) => NewPresetSavedStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.PresetEditedStatus,
                (eventArgs) => PresetEditedStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.PresetJSONMessage,
                (eventArgs) => PresetJSONMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.PresetSavedStatus,
                (eventArgs) => PresetSavedStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ProcessorUtilization,
                (eventArgs) => ProcessorUtilizationMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ProductIdentificationStatus,
                (eventArgs) => ProductIdentificationStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.QASlotsStatus,
                (eventArgs) => QASlotsStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ReplaceNodeStatus,
                (eventArgs) => ReplaceNodeStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.SetDspUnitParameterStatus,
                (eventArgs) => SetDspUnitParameterStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ShiftPresetStatus,
                (eventArgs) => ShiftPresetStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.SwapPresetStatus,
                (eventArgs) => SwapPresetStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.UsbGainStatus,
                (eventArgs) => UsbGainStatusMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.ActiveDisplay,
                (eventArgs) => ActiveDisplayMessageReceived?.Invoke(this, eventArgs));
            MessageEventHandlers.Add(
                FenderMessageLT.TypeOneofCase.UnsupportedMessageStatus,
                (eventArgs) =>
                {
                    ErrorType = eventArgs.Message.UnsupportedMessageStatus.Status;
                    UnsupportedMessageStatusReceived?.Invoke(this, eventArgs);
                });
        }
    }
}
