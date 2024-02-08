using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib.Device
{
    public class MockHidDevice : IAmpDevice
    {
        public MockDeviceState DeviceState;

        public bool IsOpen { get; private set; }
        public static int? ReportLength => 65;

        public event EventHandler? DeviceOpened;

        public event EventHandler? DeviceClosed;

        public event EventHandler<FenderMessageEventArgs>? MessageReceived;

        public event EventHandler<FenderMessageEventArgs>? MessageSent;

        private event EventHandler<FenderMessageEventArgs> InputReceived;

        private byte[] _dataBuffer = [];

        public MockHidDevice() : this(MockDeviceState.Load())
        {
        }

        public MockHidDevice(MockDeviceState deviceState)
        {
            DeviceState = deviceState;
            InputReceived += MockHidDevice_InputReceived;
        }

        public void Close()
        {
            IsOpen = false;
        }

        public void Dispose()
        {
            IsOpen = false;
            GC.SuppressFinalize(this);
        }

        public void Open()
        {
            IsOpen = true;
            OnDeviceOpened(new EventArgs());
        }

        public void Open(bool continueTry)
        {
            Open();
        }

        public void OnDeviceOpened(EventArgs e)
        {
            DeviceOpened?.Invoke(this, e);
        }

        public void OnDeviceClosed(EventArgs e)
        {
            DeviceClosed?.Invoke(this, e);
        }

        public void OnMessageReceived(FenderMessageEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        public void Write(FenderMessageLT message)
        {
            OnMessageSent(this, new FenderMessageEventArgs(message));
        }

        private void OnMessageSent(object? sender, FenderMessageEventArgs eventArgs)
        {
            byte[][]? inBuffer = eventArgs.Message?.ToUsbMessage();
            foreach (byte[] line in inBuffer!)
            {
                byte[]? inputBuffer = line;
                byte? tag = inputBuffer?[1];
                byte? length = inputBuffer?[2];
                int bufferStart = _dataBuffer.Length;
                Array.Resize(ref _dataBuffer, _dataBuffer.Length + length.GetValueOrDefault());
                Buffer.BlockCopy(inputBuffer!, 3, _dataBuffer, bufferStart, length.GetValueOrDefault());
                if (tag == (byte)UsbHidMessageTag.End)
                {
                    FenderMessageLT message = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
                    InputReceived?.Invoke(this, new FenderMessageEventArgs(message));
                    _dataBuffer = [];
                }
            }
            MessageSent?.Invoke(this, eventArgs);
        }

        private void MockHidDevice_InputReceived(object? sender, FenderMessageEventArgs eventArgs)
        {
            FenderMessageLT outMessage = new();
            int index;
            switch (eventArgs.Message?.TypeCase)
            {
                case FenderMessageLT.TypeOneofCase.FirmwareVersionRequest:
                    outMessage = MessageFactory.Create(new FirmwareVersionStatus() { Version = DeviceState.firmwareVersion }, ResponseType.IsLastAck);
                    break;

                case FenderMessageLT.TypeOneofCase.ProductIdentificationRequest:
                    outMessage = MessageFactory.Create(new ProductIdentificationStatus() { Id = DeviceState.productId }, ResponseType.IsLastAck);
                    break;

                case FenderMessageLT.TypeOneofCase.QASlotsRequest:
                    outMessage = MessageFactory.Create(new QASlotsStatus(), ResponseType.IsLastAck);
                    outMessage.QASlotsStatus.Slots.AddRange(DeviceState.qaSlots);
                    break;

                case FenderMessageLT.TypeOneofCase.QASlotsSet:
                    outMessage = MessageFactory.Create(new QASlotsStatus(), ResponseType.IsLastAck);
                    outMessage.QASlotsStatus.Slots.AddRange([.. eventArgs.Message.QASlotsSet.Slots]);
                    break;

                case FenderMessageLT.TypeOneofCase.UsbGainRequest:
                    outMessage = MessageFactory.Create(new UsbGainStatus() { ValueDB = DeviceState.usbGain.GetValueOrDefault() }, ResponseType.IsLastAck);
                    break;

                case FenderMessageLT.TypeOneofCase.UsbGainSet:
                    outMessage = MessageFactory.Create(new UsbGainStatus() { ValueDB = eventArgs.Message.UsbGainSet.ValueDB }, ResponseType.IsLastAck);
                    break;

                case FenderMessageLT.TypeOneofCase.ModalStatusMessage:
                    outMessage = MessageFactory.Create(new ModalStatusMessage() { Context = eventArgs.Message.ModalStatusMessage.Context, State = ModalState.Ok }, ResponseType.IsLastAck);
                    break;

                case FenderMessageLT.TypeOneofCase.RetrievePreset:
                    index = eventArgs.Message.RetrievePreset.Slot;
                    outMessage = MessageFactory.Create(new PresetJSONMessage() { Data = DeviceState?.Presets![index - 1], SlotIndex = index });
                    break;

                case FenderMessageLT.TypeOneofCase.LoadPreset:
                    index = eventArgs.Message.LoadPreset.PresetIndex;
                    DeviceState.CurrentPresetIndex = index - 1;
                    outMessage = MessageFactory.Create(new CurrentLoadedPresetIndexStatus() { CurrentLoadedPresetIndex = index });
                    break;

                case FenderMessageLT.TypeOneofCase.CurrentPresetRequest:
                    outMessage = MessageFactory.Create(new CurrentPresetStatus() { CurrentPresetData = DeviceState?.Presets![DeviceState.CurrentPresetIndex], CurrentSlotIndex = DeviceState.CurrentPresetIndex + 1, CurrentPresetDirtyStatus = false });
                    break;
            }
            OnMessageReceived(new FenderMessageEventArgs(outMessage));
        }
    }
}