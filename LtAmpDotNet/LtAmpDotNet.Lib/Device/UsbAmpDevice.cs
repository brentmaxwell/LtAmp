using HidSharp;
using HidSharp.Reports.Input;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Extensions;
using LtAmpDotNet.Lib.Models.Protobuf;
using System.Timers;
using Timer = System.Timers.Timer;

namespace LtAmpDotNet.Lib.Device
{
    public class UsbAmpDevice : IAmpDevice
    {
        #region Constants

        /// <summary>
        /// Default vendor ID to connect to
        /// </summary>
        public const int VENDOR_ID = 0x1ed8;

        /// <summary>
        /// Default product ID to connect to 
        /// </summary>
        public const int PRODUCT_ID = 0x0037;

        #endregion

        #region Public Properties
        public bool IsOpen => _isOpen;

        #endregion

        #region Events
        
        public event EventHandler? DeviceOpened;
        public event EventHandler? DeviceClosed;
        public event MessageReceivedEventHandler? MessageReceived;
        public event MessageSentEventHandler? MessageSent;
        private TimerCallback? _heartbeatCallback { get; set; }

        #endregion

        #region Fields

        /// <summary>
        /// Holds the HidDevice object underlying the connection
        /// </summary>
        private HidDevice? _device;
        
        /// <summary>
        /// Holds the HidStream object underlying the connection
        /// </summary>
        private HidStream? _stream;

        /// <summary>
        /// Input receiver for the incoming data
        /// </summary>
        private HidDeviceInputReceiver? _inputReceiver;
        
        // /// <summary>
        // /// Data length for reports communicated from the amplifier
        // /// </summary>
        private int? ReportLength => _device?.GetMaxInputReportLength();

        /// <summary>
        /// Buffer to store the data as it comes in to string together multi-report messages
        /// </summary>
        private byte[] _dataBuffer = Array.Empty<byte>();

        /// <summary>
        /// Holds the open state of the amp;
        /// </summary>
        private bool _isOpen = false;

        /// <summary>
        /// for disposing
        /// </summary>
        private bool _disposedValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor; will use the first available device connected with the default VID/PID
        /// </summary>
        public UsbAmpDevice()
        {

        }

        /// <summary>
        /// Uses the HidDevice specified
        /// </summary>
        /// <param name="device"></param>
        public UsbAmpDevice(HidDevice device)
        {
            _device = device;
        }

        #endregion

        #region Public Methods

        public void Open()
        {
            Open(true);
        }

        /// <summary>
        /// Opens a connection to the amplifier
        /// </summary>
        /// <exception cref="IOException">Thrown when there is a connection issue with the device</exception>
        public void Open(bool continueTry = true)
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
                _stream.Closed += Stream_DeviceClosed; ;
                _inputReceiver.Start(_stream);
                Timer heartbeatTimer = new Timer(1000);
                heartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
                heartbeatTimer.Start();
                OnDeviceOpened(new EventArgs());
            }
            else if(continueTry)
            {
                DeviceList.Local.Changed += UsbDevices_Changed;
            }
        }
        
        public void Close()
        {
            _isOpen = false;
            _stream?.Close();
            OnDeviceClosed(new EventArgs());
            
        }

        public void Write(FenderMessageLT message)
        {
            foreach (var packet in message.ToUsbMessage())
            {
                _stream?.Write(packet);
            }
            if (message.TypeCase != FenderMessageLT.TypeOneofCase.Heartbeat)
            {
                OnMessageSent(new FenderMessageEventArgs(message));
            }
        }
       
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
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

        #endregion

        #region Private Events

        public void OnDeviceOpened(EventArgs e)
        {
            DeviceOpened?.Invoke(this, e);
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

        /// <summary>
        /// Triggered on change of USB devices connected; used to auto connect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsbDevices_Changed(object? sender, DeviceListChangedEventArgs e)
        {
            DeviceList.Local.Changed -= UsbDevices_Changed;
            Open(true);
        }

        /// <summary>
        /// Parses the data recevied from the input receiver, and triggers a MessageReceived event with the parsed FenderMessageLT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    OnMessageReceived(new FenderMessageEventArgs(message));
                    
                    _dataBuffer = new byte[0];
                }
                inputBuffer = new byte[ReportLength.GetValueOrDefault()];
            }
        }
        
        private void HeartbeatTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Write(new FenderMessageLT()
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
            _isOpen = false;
            OnDeviceClosed(e);
        }

        #endregion
    }

    public enum UsbHidMessageTag
    {
        Start = 0x33,
        Continue = 0x34,
        End = 0x35,
    }
}