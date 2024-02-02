using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmplifier
    {
        /// <summary>Triggered when the amplifer connection has been initialized</summary>
        public event EventHandler? AmplifierConnected;

        /// <summary>Triggered when the amplifier disconnects</summary>
        public event EventHandler? AmplifierDisconnected;

        /// <summary>Triggered when a message is sent to the amp</summary>
        public event EventHandler<FenderMessageEventArgs>? MessageSent;

        /// <summary>Triggered when any valid message is received from the amplifier</summary>
        public event EventHandler<FenderMessageEventArgs>? MessageReceived;

        /// <summary>Triggered when the amp responds with an error message</summary>
        public event EventHandler<FenderMessageEventArgs>? UnknownMessageReceived;

        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ActiveDisplayMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? AuditionPresetStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? AuditionStateStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ClearPresetStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ConnectionStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? CurrentDisplayedPresetIndexStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? CurrentLoadedPresetIndexBypassStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? CurrentLoadedPresetIndexStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? CurrentPresetStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? DspUnitParameterStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ExitAuditionPresetStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? FirmwareVersionStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? FrameBufferMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? HeartbeatMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? IndexButtonMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? IndexEncoderMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? IndexPotMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? LT4FootswitchModeStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? LineOutGainStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? LoadPreset_TestSuiteMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? LoopbackTestMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? MemoryUsageStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ModalStatusMessageMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? NewPresetSavedStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? PresetEditedStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? PresetJSONMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? PresetSavedStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ProcessorUtilizationMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ProductIdentificationStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? QASlotsStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ReplaceNodeStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? SetDspUnitParameterStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? ShiftPresetStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? SwapPresetStatusMessageReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? UnsupportedMessageStatusReceived;
        /// <summary></summary>
        public event EventHandler<FenderMessageEventArgs>? UsbGainStatusMessageReceived;

        /// <summary>Contains event handlers to trigger message events</summary>
        private Dictionary<FenderMessageLT.TypeOneofCase, Action<FenderMessageEventArgs>> MessageEventHandlers = [];

        /// <summary>Initializes the collection of MessageEventHandlers</summary>
        private void SetupMessageEventHandlers()
        {
            MessageEventHandlers = new Dictionary<FenderMessageLT.TypeOneofCase, Action<FenderMessageEventArgs>>(){
                { FenderMessageLT.TypeOneofCase.AuditionPresetStatus,
                    (eventArgs) => AuditionPresetStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.AuditionStateStatus,
                    (eventArgs) => AuditionStateStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ClearPresetStatus,
                    (eventArgs) => ClearPresetStatusMessageReceived?.Invoke(this, eventArgs)
                },
                {FenderMessageLT.TypeOneofCase.ConnectionStatus,
                    (eventArgs) => ConnectionStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.CurrentDisplayedPresetIndexStatus,
                    (eventArgs) => CurrentDisplayedPresetIndexStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexStatus,
                    (eventArgs) => CurrentLoadedPresetIndexStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexBypassStatus,
                    (eventArgs) => CurrentLoadedPresetIndexBypassStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.CurrentPresetStatus,
                    (eventArgs) => CurrentPresetStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.DspUnitParameterStatus,
                    (eventArgs) => DspUnitParameterStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ExitAuditionPresetStatus,
                    (eventArgs) => ExitAuditionPresetStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.FirmwareVersionStatus,
                    (eventArgs) => FirmwareVersionStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.FrameBufferMessage,
                    (eventArgs) => FrameBufferMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.IndexButton,
                    (eventArgs) => IndexButtonMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.IndexEncoder,
                    (eventArgs) => IndexEncoderMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.IndexPot,
                    (eventArgs) => IndexPotMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.LineOutGainStatus,
                    (eventArgs) => LineOutGainStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.Lt4FootswitchModeStatus,
                    (eventArgs) => LT4FootswitchModeStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.MemoryUsageStatus,
                    (eventArgs) => MemoryUsageStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ModalStatusMessage,
                    (eventArgs) => ModalStatusMessageMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.NewPresetSavedStatus,
                    (eventArgs) => NewPresetSavedStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.PresetEditedStatus,
                    (eventArgs) => PresetEditedStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.PresetJSONMessage,
                    (eventArgs) => PresetJSONMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.PresetSavedStatus,
                    (eventArgs) => PresetSavedStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ProcessorUtilization,
                    (eventArgs) => ProcessorUtilizationMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ProductIdentificationStatus,
                    (eventArgs) => ProductIdentificationStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.QASlotsStatus,
                    (eventArgs) => QASlotsStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ReplaceNodeStatus,
                    (eventArgs) => ReplaceNodeStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.SetDspUnitParameterStatus,
                    (eventArgs) => SetDspUnitParameterStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ShiftPresetStatus,
                    (eventArgs) => ShiftPresetStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.SwapPresetStatus,
                    (eventArgs) => SwapPresetStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.UsbGainStatus,
                    (eventArgs) => UsbGainStatusMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.ActiveDisplay,
                    (eventArgs) => ActiveDisplayMessageReceived?.Invoke(this, eventArgs)
                },
                { FenderMessageLT.TypeOneofCase.UnsupportedMessageStatus,
                    (eventArgs) =>
                    {
                        ErrorType = eventArgs.Message?.UnsupportedMessageStatus.Status;
                        UnsupportedMessageStatusReceived?.Invoke(this, eventArgs);
                    }
                }
            };
        }

        /// <summary>Executes the command, and waits for the event to respond before continuing.</summary>
        /// <param name="action">the action to run</param>
        /// <param name="eventHandler">the event to wait for</param>
        /// <param name="waitTime">timeout (in seconds)</param>
        public static EventArgs? WaitForEvent(Action action, Action<EventHandler> eventHandler, int waitTime = 5)
        {
            EventArgs? returnVal = null;
            AutoResetEvent wait = new AutoResetEvent(false);
            eventHandler((sender, eventArgs) =>
            {
                returnVal = eventArgs;
                wait.Set();
            });
            action.Invoke();
            wait.WaitOne(TimeSpan.FromSeconds(waitTime));
            return returnVal;
        }

        /// <summary>Executes the command, and waits for the event to respond before continuing.</summary>
        /// <param name="action">the action to run</param>
        /// <param name="eventHandler">the event to wait for</param>
        /// <param name="waitTime">timeout (in seconds)</param>
        public static T? WaitForEvent<T>(Action action, Action<EventHandler<T>> eventHandler, int waitTime = 5)
        {
            T? returnVal = default;
            AutoResetEvent wait = new AutoResetEvent(false);
            eventHandler((sender, eventArgs) =>
            {
                returnVal = eventArgs;
                wait.Set();
            });
            action.Invoke();
            wait.WaitOne(TimeSpan.FromSeconds(waitTime));
            return returnVal;
        }


    }
}
