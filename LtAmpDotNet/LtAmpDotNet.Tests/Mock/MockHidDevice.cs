using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Tests.Mock
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

        public MockHidDevice() : this(MockDeviceState.Load()) { }
        public MockHidDevice(MockDeviceState deviceState)
        {
            DeviceState = deviceState;
            MessageSent += MockHidDevice_MessageSent;
            InputReceived += MockHidDevice_InputReceived;
        }

        public void Close()
        {
            IsOpen = false;
        }

        public void Dispose()
        {
            IsOpen = false;
        }

        public void Open()
        {
            DeviceOpened?.Invoke(this, new EventArgs());
        }

        public void Open(bool continueTry)
        {
            OnDeviceOpened(new EventArgs());
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

        public void OnMessageSent(FenderMessageEventArgs e)
        {
            MessageSent?.Invoke(this, e);
        }

        public void Write(FenderMessageLT message)
        {
            MockHidDevice_MessageSent(this, new FenderMessageEventArgs(message));
        }

        private void MockHidDevice_MessageSent(object? sender, FenderMessageEventArgs eventArgs)
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
        }

        private void MockHidDevice_InputReceived(object? sender, FenderMessageEventArgs eventArgs)
        {
            FenderMessageLT outMessage = new();
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
                    outMessage.QASlotsStatus.Slots.AddRange(eventArgs.Message.QASlotsSet.Slots.ToArray());
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
                    int index = eventArgs.Message.RetrievePreset.Slot;
                    outMessage = MessageFactory.Create(new PresetJSONMessage() { Data = DeviceState?.Presets![index - 1], SlotIndex = index });
                    break;
            }
            OnMessageReceived(new FenderMessageEventArgs(outMessage));
        }
    }
}
