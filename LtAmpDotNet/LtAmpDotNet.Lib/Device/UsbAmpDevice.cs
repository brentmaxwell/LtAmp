using Google.Protobuf.Reflection;
using HidSharp;
using HidSharp.Reports;
using HidSharp.Reports.Input;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static LtAmpDotNet.Lib.LtAmpDevice;

namespace LtAmpDotNet.Lib.Device
{
    public class UsbAmpDevice : IUsbAmpDevice
    {
        public const int VENDOR_ID = 0x1ed8;
        public const int PRODUCT_ID = 0x0037;

        private HidDevice? _device;
        private HidStream? _stream;
        private HidDeviceInputReceiver? _inputReceiver;
        private byte[] _dataBuffer = Array.Empty<byte>();
        private bool _isOpen = false;
        private bool _disposedValue;
        public event EventHandler? Closed;
        public event MessageReceivedEventHandler? MessageReceived;
        public event MessageSentEventHandler? MessageSent;

        public int? ReportLength => _device?.GetMaxInputReportLength();
        public bool IsOpen => _isOpen;

        public UsbAmpDevice()
        {

        }

        public UsbAmpDevice(HidDevice device)
        {
            _device = device;
        }

        public void Open()
        {
            if (_device == null)
            {
                _device = DeviceList.Local.GetHidDeviceOrNull(VENDOR_ID, PRODUCT_ID);
            }
            if (_device != null && _stream == null)
            {
                _inputReceiver = _device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                _inputReceiver.Received += InputReceiver_Received;
                _stream = _device.Open();
                if (_stream == null)
                {
                    throw new IOException("Could not open connection to device");
                }
                _stream.Closed += _stream_Closed; ;
                _inputReceiver.Start(_stream);
            }
            else
            {
                DeviceList.Local.Changed += UsbDevices_Changed;
            }
        }

        public void Close()
        {
            _stream?.Close();
        }

        public void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_stream != null)
                    {
                        _stream.Close();
                        _stream.Dispose();
                    }
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Write(FenderMessageLT message)
        {
            foreach (var packet in message.ToUsbMessage())
            {
                _stream?.Write(packet);
            }
            if (message.TypeCase != FenderMessageLT.TypeOneofCase.Heartbeat)
            {
                MessageSent?.Invoke(message);
            }
        }

        private void UsbDevices_Changed(object? sender, DeviceListChangedEventArgs e)
        {
            Open();
        }

        private void _stream_Closed(object? sender, EventArgs e)
        {
            _isOpen = false;
            Closed?.Invoke(this, e);
        }

        private void InputReceiver_Received(object? sender, EventArgs e)
        {
            
            while ((_inputReceiver?.Stream.CanRead).GetValueOrDefault())
            {
                var inputBuffer = _inputReceiver?.Stream.Read();
                var tag = inputBuffer?[2];
                var length = inputBuffer?[3];
                var bufferStart = _dataBuffer.Length;
                Array.Resize(ref _dataBuffer, _dataBuffer.Length + length.GetValueOrDefault());
                Buffer.BlockCopy(inputBuffer!, 4, _dataBuffer, bufferStart, length.GetValueOrDefault());
                if (tag == (byte)UsbHidMessageTag.End)
                {
                    var message = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
                    MessageReceived?.Invoke(message);
                    _dataBuffer = new byte[0];
                }
                inputBuffer = new byte[ReportLength.GetValueOrDefault()];
            }
        }
    }
}