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

namespace LtAmpDotNet.Tests.Mock
{
    internal class MockHidDevice : IUsbAmpDevice
    {
        public MockDeviceState DeviceState;
        private bool _isOpen;
        public bool IsOpen => _isOpen;
        public int? ReportLength => 65;

        public event EventHandler? Closed;
        public event MessageReceivedEventHandler? MessageReceived;
        public event MessageSentEventHandler? MessageSent;

        public MockHidDevice(MockDeviceState deviceState)
        {
            DeviceState = deviceState;
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
            FenderMessageLT outMessage = new FenderMessageLT();
            MessageSent?.Invoke(message);
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
