using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services.Messages;
using System;

namespace LtAmpDotNet.Services
{
    [MessageReceiverChannel<MessageChannelEnum>(MessageChannelEnum.ToAmplifier)]
    public class AmplifierService : ObservableModel,
        IRecipient<ConnectionStatusMessage>,
        IRecipient<ParameterChangedMessage>,
        IRecipient<CurrentPresetChangedMessage>,
        IRecipient<DspUnitChangedMessage>,
        IRecipient<QaSlotsChangedMessage>
    {
        private readonly ILtAmplifier _amplifier;

        public AmplifierService(ILtAmplifier amplifier) : base()
        {
            _amplifier = amplifier;
            IsActive = true;
        }

        protected override void OnActivated()
        {
            _amplifier.AmplifierConnected += Amplifier_ConnectionStatus;
            _amplifier.AmplifierDisconnected += Amplifier_ConnectionStatus;
            _amplifier.MessageSent += Amplifier_MessageSent;
            _amplifier.MessageReceived += Amplifier_MessageReceived;
            _amplifier.CurrentDisplayedPresetIndexStatusMessageReceived += Amplifier_CurrentDisplayedPresetIndexStatusMessageReceived;
            _amplifier.CurrentLoadedPresetIndexStatusMessageReceived += Amplifier_CurrentLoadedPresetIndexStatusMessageReceived;
            _amplifier.CurrentPresetStatusMessageReceived += Amplifier_CurrentPresetStatusMessageReceived;
            _amplifier.DspUnitParameterStatusMessageReceived += Amplifier_DspUnitParameterStatusMessageReceived;
            _amplifier.ReplaceNodeStatusMessageReceived += Amplifier_ReplaceNodeStatusMessageReceived;
            _amplifier.PresetJSONMessageReceived += Amplifier_PresetJSONMessageReceived;
            _amplifier.QASlotsStatusMessageReceived += Amplifier_QASlotsStatusMessageReceived;
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            _amplifier.AmplifierConnected -= Amplifier_ConnectionStatus;
            _amplifier.AmplifierDisconnected -= Amplifier_ConnectionStatus;
            _amplifier.MessageSent -= Amplifier_MessageSent;
            _amplifier.MessageReceived -= Amplifier_MessageReceived;
            _amplifier.CurrentDisplayedPresetIndexStatusMessageReceived -= Amplifier_CurrentDisplayedPresetIndexStatusMessageReceived;
            _amplifier.CurrentLoadedPresetIndexStatusMessageReceived -= Amplifier_CurrentLoadedPresetIndexStatusMessageReceived;
            _amplifier.CurrentPresetStatusMessageReceived -= Amplifier_CurrentPresetStatusMessageReceived;
            _amplifier.DspUnitParameterStatusMessageReceived -= Amplifier_DspUnitParameterStatusMessageReceived;
            _amplifier.PresetJSONMessageReceived -= Amplifier_PresetJSONMessageReceived;
            _amplifier.QASlotsStatusMessageReceived -= Amplifier_QASlotsStatusMessageReceived;
            base.OnDeactivated();
        }

        #region Amplifier Events

        private async void Amplifier_ConnectionStatus(object? sender, EventArgs e)
        {
            if (_amplifier.IsOpen)
            {
                Send(new ConnectionStatusMessage(Messages.ConnectionStatus.Connecting), MessageChannelEnum.FromAmplifier);
                await _amplifier.GetAllPresetsAsync();
                await _amplifier.GetCurrentPresetAsync();
                await _amplifier.GetUsbGainAsync();
                await _amplifier.GetQASlotsAsync();
            }
            Send(new ConnectionStatusMessage(
                _amplifier.IsOpen ? Messages.ConnectionStatus.Connected : Messages.ConnectionStatus.Disconnected),
                MessageChannelEnum.FromAmplifier
            );
        }

        private void Amplifier_MessageSent(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Send(new FenderLtMessage(MessageDirection.Output, e.Message));
        }

        private void Amplifier_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Send(new FenderLtMessage(MessageDirection.Input, e.Message));
        }

        private void Amplifier_CurrentDisplayedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            SendOnlyUnsolicited(e.Message.ResponseType, new CurrentPresetChangedMessage(e.Message.CurrentDisplayedPresetIndexStatus.CurrentDisplayedPresetIndex));
        }

        private void Amplifier_CurrentLoadedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            SendOnlyUnsolicited(e.Message.ResponseType,
                        new CurrentPresetChangedMessage(e.Message.CurrentLoadedPresetIndexStatus.CurrentLoadedPresetIndex));
        }

        private void Amplifier_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Send(new CurrentPresetChangedMessage(e.Message.CurrentPresetStatus.CurrentSlotIndex), MessageChannelEnum.FromAmplifier);
        }

        private void Amplifier_DspUnitParameterStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            SendOnlyUnsolicited(e.Message.ResponseType,
                new ParameterChangedMessage(Enum.Parse<NodeIdType>(e.Message.DspUnitParameterStatus.NodeId),
                    e.Message.DspUnitParameterStatus.ParameterId,
                    e.Message.DspUnitParameterStatus.HasBoolParameter ? e.Message.DspUnitParameterStatus.BoolParameter :
                    e.Message.DspUnitParameterStatus.HasSint32Parameter ? e.Message.DspUnitParameterStatus.Sint32Parameter :
                    e.Message.DspUnitParameterStatus.HasFloatParameter ? e.Message.DspUnitParameterStatus.FloatParameter :
                    e.Message.DspUnitParameterStatus.HasStringParameter ? e.Message.DspUnitParameterStatus.StringParameter : null));
        }

        private void Amplifier_PresetJSONMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Send(new PresetMessage(new PresetModel(Preset.FromString(e.Message!.PresetJSONMessage.Data)!)
            {
                PresetIndex = e.Message.PresetJSONMessage.SlotIndex
            }), MessageChannelEnum.FromAmplifier);
        }

        private void Amplifier_QASlotsStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            SendOnlyUnsolicited(e.Message.ResponseType,
                new QaSlotsChangedMessage(
                    (int)e.Message.QASlotsStatus.Slots[0],
                    (int)e.Message.QASlotsStatus.Slots[1]
                )
            );
        }

        private void Amplifier_ReplaceNodeStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            SendOnlyUnsolicited(e.Message.ResponseType,
                new DspUnitChangedMessage(
                    Enum.Parse<NodeIdType>(e.Message.ReplaceNodeStatus.NodeIdReplaced),
                    e.Message.ReplaceNodeStatus.FenderIdReplaced
                )
            );
        }

        #endregion Amplifier Events

        #region Application events

        //Connect/Disconnect
        public async void Receive(ConnectionStatusMessage message)
        {
            if (_amplifier.IsOpen)
            {
                _amplifier.Close();
            }
            else
            {
                await _amplifier.OpenAsync();
            }
        }

        public void Receive(ParameterChangedMessage message)
        {
            _amplifier.SetDspUnitParameter(message.DspUnitType,
                new DspUnitParameter() { Name = message.ControlId, Value = message.ParameterValue });
        }

        public void Receive(CurrentPresetChangedMessage message)
        {
            _amplifier.LoadPreset(message.PresetIndex);
        }

        public void Receive(DspUnitChangedMessage message)
        {
            _amplifier.ReplaceNode(message.DspUnitType, message.FenderId);
        }

        #endregion Application events

        public T? SendOnlyUnsolicited<T>(ResponseType responseType, T message) where T : class
        {
            return responseType == ResponseType.Unsolicited ? Send(message, MessageChannelEnum.FromAmplifier) : null;
        }

        public void Receive(QaSlotsChangedMessage message)
        {
            _amplifier.SetQASlots([(uint)message.SlotA, (uint)message.SlotB]);
        }
    }
}