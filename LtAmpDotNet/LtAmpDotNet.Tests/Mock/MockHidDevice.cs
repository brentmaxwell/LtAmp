using HidSharp;
using HidSharp.Reports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Model;
using Newtonsoft.Json;
using static LtAmpDotNet.Lib.LtAmpDevice;
using LtAmpDotNet.Lib.Models.Protobuf;
using LtAmpDotNet.Lib.Extensions;

namespace LtAmpDotNet.Tests.Mock
{
    public class MockHidDevice : IAmpDevice
    {
        public MockDeviceState DeviceState;
        private bool _isOpen;
        public bool IsOpen => _isOpen;
        public int? ReportLength => 65;


        public event EventHandler? Opened;
        public event EventHandler? Closed;
        public event MessageReceivedEventHandler? MessageReceived;
        public event MessageSentEventHandler? MessageSent;

        private event MessageReceivedEventHandler InputReceived;

        private byte[] _dataBuffer = Array.Empty<byte>();

        public MockHidDevice(MockDeviceState deviceState)
        {
            DeviceState = deviceState;
            MessageSent += MockHidDevice_MessageSent;
            InputReceived += MockHidDevice_InputReceived;
        }

        public void Close()
        {
            _isOpen = false;
        }

        public void Dispose()
        {
            _isOpen = false;
        }

        public void Open()
        {
        }

        public void Write(FenderMessageLT message)
        {
            var inBinaryMessage = message.ToUsbMessage();
            var inStringMessage = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
            FenderMessageLT outMessage = new FenderMessageLT();
            MessageSent?.Invoke(message);
        }

        private void MockHidDevice_MessageSent(FenderMessageLT message)
        {
            var inBuffer = message.ToUsbMessage();
            foreach (var line in inBuffer)
            {
                var inputBuffer = line;
                var tag = inputBuffer?[1];
                var length = inputBuffer?[2];
                var bufferStart = _dataBuffer.Length;
                Array.Resize(ref _dataBuffer, _dataBuffer.Length + length.GetValueOrDefault());
                Buffer.BlockCopy(inputBuffer!, 3, _dataBuffer, bufferStart, length.GetValueOrDefault());
                if (tag == (byte)UsbHidMessageTag.End)
                {
                    InputReceived?.Invoke(FenderMessageLT.Parser.ParseFrom(_dataBuffer));
                    _dataBuffer = new byte[0];
                }
            }
        }

        private void MockHidDevice_InputReceived(FenderMessageLT message)
        {
            FenderMessageLT outMessage;
            switch(message.TypeCase)
            {
                case FenderMessageLT.TypeOneofCase.FirmwareVersionRequest:
                    outMessage = MessageFactory.Create(new FirmwareVersionStatus() { Version = DeviceState.firmwareVersion }, ResponseType.IsLastAck);
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.ProductIdentificationRequest:
                    outMessage = MessageFactory.Create(new ProductIdentificationStatus() { Id = DeviceState.productId }, ResponseType.IsLastAck);
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.QASlotsRequest:
                    outMessage = MessageFactory.Create(new QASlotsStatus(), ResponseType.IsLastAck);
                    outMessage.QASlotsStatus.Slots.AddRange(DeviceState.qaSlots);
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.QASlotsSet:
                    outMessage = MessageFactory.Create(new QASlotsStatus(), ResponseType.IsLastAck);
                    outMessage.QASlotsStatus.Slots.AddRange(message.QASlotsSet.Slots.ToArray());
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.UsbGainRequest:
                    outMessage = MessageFactory.Create(new UsbGainStatus() { ValueDB = DeviceState.usbGain.GetValueOrDefault() }, ResponseType.IsLastAck);
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.UsbGainSet:
                    outMessage = MessageFactory.Create(new UsbGainStatus() { ValueDB = message.UsbGainSet.ValueDB }, ResponseType.IsLastAck);
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.ModalStatusMessage:
                    outMessage = MessageFactory.Create(new ModalStatusMessage() { Context = message.ModalStatusMessage.Context, State = ModalState.Ok }, ResponseType.IsLastAck);
                    MessageReceived?.Invoke(outMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.RetrievePreset:
                    var index = message.RetrievePreset.Slot;
                    outMessage = MessageFactory.Create(new PresetJSONMessage() { Data = DeviceState?.Presets![index - 1], SlotIndex = index });
                    MessageReceived?.Invoke(outMessage);
                    break;
            }
        }
    }
}
