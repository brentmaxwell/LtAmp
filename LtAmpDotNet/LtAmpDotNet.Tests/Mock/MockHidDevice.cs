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
using static LtAmpDotNet.Lib.LtAmplifier;
using LtAmpDotNet.Lib.Models.Protobuf;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Events;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace LtAmpDotNet.Tests.Mock
{
    public class MockHidDevice : IAmpDevice
    {
        public MockDeviceState DeviceState;
        private bool _isOpen;
        public bool IsOpen => _isOpen;
        public int? ReportLength => 65;


        public event EventHandler? DeviceOpened;
        public event EventHandler? DeviceClosed;
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
            DeviceOpened?.Invoke(this, new EventArgs());
        }

        public void Open(bool continueTry)
        {
            OnDeviceOpened(new EventArgs());
        }

        public void OnDeviceOpened(EventArgs e)
        {
            DeviceOpened?.Invoke(this, new EventArgs());
        }

        public void OnDeviceClosed(EventArgs e)
        {
            DeviceClosed?.Invoke(this, new EventArgs());
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
            var inBinaryMessage = message.ToUsbMessage();
            var inStringMessage = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
            FenderMessageLT outMessage = new FenderMessageLT();
            OnMessageSent(new FenderMessageEventArgs(message));
            
        }

        private void MockHidDevice_MessageSent(object sender, FenderMessageEventArgs eventArgs)
        {
            var inBuffer = eventArgs.Message.ToUsbMessage();
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
                    var message = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
                    InputReceived?.Invoke(this, new FenderMessageEventArgs(message));
                    _dataBuffer = new byte[0];
                }
            }
        }

        private void MockHidDevice_InputReceived(object sender, FenderMessageEventArgs eventArgs)
        {
            FenderMessageLT outMessage = new FenderMessageLT();
            switch(eventArgs.Message.TypeCase)
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
                    var index = eventArgs.Message.RetrievePreset.Slot;
                    outMessage = MessageFactory.Create(new PresetJSONMessage() { Data = DeviceState?.Presets![index - 1], SlotIndex = index });
                    break;
            }
            OnMessageReceived(new FenderMessageEventArgs(outMessage));
        }
    }
}
