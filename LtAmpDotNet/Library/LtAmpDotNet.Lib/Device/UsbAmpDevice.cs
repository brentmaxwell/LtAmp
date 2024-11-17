using HidSharp;
using HidSharp.Reports.Input;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Models.Protobuf;
using System.Timers;
using Timer = System.Timers.Timer;

namespace LtAmpDotNet.Lib.Device
{
    /// <summary>
    /// Represents a USB connection to the amplifier
    /// </summary>
    public class UsbAmpDevice : IAmpDevice
    {
        #region Constants

        /// <summary>Default vendor ID to connect to</summary>
        public const int VENDOR_ID = 0x1ed8;

        /// <summary>Default product ID to connect to</summary>
        public const int PRODUCT_ID = 0x0037;

        #endregion Constants

        #region Public Properties

        /// <summary>State of the connection to the amplifier</summary>
        public bool IsOpen { get; private set; } = false;

        #endregion Public Properties

        #region Events

        /// <summary>Triggered when the device connection is opened and successful</summary>
        public event EventHandler? DeviceOpened;

        event EventHandler? IAmpDevice.DeviceOpened
        {
            add => DeviceOpened += value;
            remove => DeviceOpened -= value;
        }

        /// <summary>Triggered when the device connection closes</summary>
        public event EventHandler? DeviceClosed;

        event EventHandler? IAmpDevice.DeviceClosed
        {
            add => DeviceClosed += value;
            remove => DeviceClosed -= value;
        }

        /// <summary>Triggered when a message is sent to the amp</summary>
        public event EventHandler<FenderMessageEventArgs>? MessageSent;

        /// <summary>Triggered when a message is received from the amp</summary>
        public event EventHandler<FenderMessageEventArgs>? MessageReceived;

        #endregion Events

        #region Fields

        /// <summary>Holds the HidDevice object underlying the connection</summary>
        private HidDevice? _device;

        /// <summary>Holds the HidStream object underlying the connection</summary>
        private HidStream? _stream;

        /// <summary>Input receiver for the incoming data</summary>
        private HidDeviceInputReceiver? _inputReceiver;

        /// <summary>Timer for the heartbeat</summary>
        private readonly Timer heartbeatTimer = new(1000);

        /// <summary>Data length for reports communicated from the amplifier</summary>
        private int? reportLength => _device?.GetMaxInputReportLength();

        /// <summary>Buffer to store the data as it comes in to string together multi-report messages</summary>
        private byte[] _dataBuffer = [];

        /// <summary>for disposing</summary>
        private bool _disposedValue;

        #endregion Fields

        #region Constructors

        /// <summary>Default constructor; will use the first available device connected with the default VID/PID</summary>
        public UsbAmpDevice()
        { }

        /// <summary>Uses the HidDevice specified</summary>
        /// <param name="device"></param>
        public UsbAmpDevice(HidDevice device)
        {
            _device = device;
            DeviceList.Local.Changed += DetectDisconnect;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>Opens a connection to the amplifier</summary>
        public void Open()
        {
            Open(true);
        }

        /// <summary>Opens a connection to the amplifier</summary>
        /// <param name="continueTry">True to wait for the device to be enumerated on the computer, or false to fail after one try</param>
        /// <exception cref="IOException">Thrown when there is a connection issue with the device</exception>
        public void Open(bool continueTry = true)
        {
            if (_device == null)
            {
                try
                {
                    DeviceList.Local.Changed -= UsbDevices_Changed;
                    _device = DeviceList.Local.GetHidDeviceOrNull(VENDOR_ID, PRODUCT_ID);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception trying to enumerate USB devices: {ex.Message}", ex);
                }
            }
            if (_device != null && _device.CanOpen && _stream == null)
            {
                try
                {
                    _inputReceiver = _device.GetReportDescriptor().CreateHidDeviceInputReceiver();
                    _inputReceiver.Received += InputReceiver_Received;
                    _inputReceiver.Stopped += inputReceiver_Stopped;
                    _stream = _device.Open();
                    if (_stream == null)
                    {
                        throw new IOException("Could not open connection to device");
                    }
                    IsOpen = true;
                    _stream.Closed += Stream_DeviceClosed;
                    _inputReceiver.Start(_stream);
                    heartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
                    heartbeatTimer.Start();
                    OnDeviceOpened(new EventArgs());
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception opening device {_device.GetFriendlyName()}: {ex.Message}", ex);
                }
            }
            else if (continueTry)
            {
                DeviceList.Local.Changed += UsbDevices_Changed;
            }
        }

        private void inputReceiver_Stopped(object? sender, EventArgs e)
        {
            _inputReceiver = null;
        }

        /// <summary>Closes the connection to the amplifier</summary>
        public void Close()
        {
            IsOpen = false;
            heartbeatTimer.Stop();
            if (_inputReceiver != null)
            {
                _inputReceiver.Received -= InputReceiver_Received;
                _inputReceiver = null;
            }
            _stream?.Close();
            OnDeviceClosed(new EventArgs());
        }

        /// <summary>Sends a message to the amp</summary>
        /// <param name="message">The message to send to the amp</param>
        /// <exception cref="Exception">Thrown when the device is not connected</exception>
        public void Write(FenderMessageLT message)
        {
            if (_stream != null && _stream.CanWrite && IsOpen)
            {
                try
                {
                    foreach (byte[] packet in message.ToUsbMessage())
                    {
                        _stream.Write(packet);
                    }
                    if (message.TypeCase != FenderMessageLT.TypeOneofCase.Heartbeat)
                    {
                        OnMessageSent(new FenderMessageEventArgs(message));
                    }
                }
                catch
                {
                }
            }
        }

        public async Task WriteAsync(FenderMessageLT message)
        {
            if (_stream == null || !IsOpen)
            {
                //throw new Exception($"Exception: device is not connected");
            }
            else if (_stream.CanWrite && IsOpen)
            {
                try
                {
                    foreach (byte[] packet in message.ToUsbMessage())
                    {
                        if (IsOpen)
                        {
                            await _stream.WriteAsync(packet);
                        }
                    }
                    if (message.TypeCase != FenderMessageLT.TypeOneofCase.Heartbeat)
                    {
                        OnMessageSent(new FenderMessageEventArgs(message));
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>Disposes underlying objects</summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Disposes underlying objects</summary>
        /// <param name="disposing"></param>
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

        #endregion Public Methods

        #region Private Events

        /// <summary>Triggers the DeviceOpened event</summary>
        /// <param name="e"></param>
        public void OnDeviceOpened(EventArgs e)
        {
            DeviceOpened?.Invoke(this, e);
        }

        /// <summary>Triggers the DeviceClosed event</summary>
        /// <param name="e"></param>
        public void OnDeviceClosed(EventArgs e)
        {
            DeviceClosed?.Invoke(this, e);
        }

        /// <summary>Triggers the MessageReceived event</summary>
        /// <param name="e"></param>
        public void OnMessageReceived(FenderMessageEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        /// <summary>Triggers the MessageSent event</summary>
        /// <param name="e"></param>
        public void OnMessageSent(FenderMessageEventArgs e)
        {
            MessageSent?.Invoke(this, e);
        }

        /// <summary>Triggered on change of USB devices connected; used to auto connect.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsbDevices_Changed(object? sender, DeviceListChangedEventArgs e)
        {
            DeviceList.Local.Changed -= UsbDevices_Changed;

            Open(true);
        }

        private void DetectDisconnect(object? sender, DeviceListChangedEventArgs e)
        {
            Console.WriteLine(e);
        }

        /// <summary>Parses the data recevied from the input receiver, and triggers a MessageReceived event with the parsed FenderMessageLT message</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputReceiver_Received(object? sender, EventArgs e)
        {
            while (_inputReceiver != null && _inputReceiver.IsRunning && IsOpen)
            {
                try
                {
                    byte[]? inputBuffer = _inputReceiver.Stream.Read();
                    byte? tag = inputBuffer?[2];
                    byte? length = inputBuffer?[3];
                    int bufferStart = _dataBuffer.Length;
                    Array.Resize(ref _dataBuffer, _dataBuffer.Length + length.GetValueOrDefault());
                    Buffer.BlockCopy(inputBuffer!, 4, _dataBuffer, bufferStart, length.GetValueOrDefault());
                    if (tag == (byte)UsbHidMessageTag.End)
                    {
                        FenderMessageLT message = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
                        OnMessageReceived(new FenderMessageEventArgs(message));
                        _dataBuffer = [];
                    }
                }
                catch (IOException)
                {
                    _stream?.Close();
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        /// Ticks with the heartbeatTimer to send a heartbeat message to the amp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HeartbeatTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await WriteAsync(new FenderMessageLT()
            {
                ResponseType = ResponseType.Unsolicited,
                Heartbeat = new Heartbeat()
                {
                    DummyField = true
                }
            });
        }

        /// <summary>
        /// Triggered when the underlying data stream closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stream_DeviceClosed(object? sender, EventArgs e)
        {
            IsOpen = false;
            heartbeatTimer.Stop();
            if (_inputReceiver != null)
            {
                _inputReceiver.Received -= InputReceiver_Received;
                _inputReceiver = null;
            }
            _stream = null;
            _device = null;
            OnDeviceClosed(e);
        }

        #endregion Private Events
    }

    /// <summary>
    /// The first byte of a USB data packet from the amp
    /// </summary>
    public enum UsbHidMessageTag
    {
        /// <summary>First packet in a series</summary>
        Start = 0x33,

        /// <summary>A message in a series of data packets</summary>
        Continue = 0x34,

        /// <summary>Last or only packet in a series</summary>
        End = 0x35,
    }
}