using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Lib.Models.Protobuf;
using static LtAmpDotNet.Lib.Models.Protobuf.FenderMessageLT;

namespace LtAmpDotNet.Lib
{
    /// <summary>
    /// Represents an amplifier device
    /// </summary>
    public partial class LtAmplifier : IDisposable, ILtAmplifier
    {
        /// <summary>number of presets available in the amplifier</summary>
        public const int NUM_OF_PRESETS = 60;

        /// <summary>definitions of all parts of the presets and DspUnits in the amplifier</summary>
        public static List<DspUnitDefinition>? DspUnitDefinitions => DspUnitDefinition.Load();

        #region public properties

        /// <summary>Current state of the amp connection</summary>
        public bool IsOpen { get; private set; }

        /// <summary>Contains an error type when the amp send an UnsupportedMessageStatus message</summary>
        public ErrorType? ErrorType { get; set; }

        #endregion public properties

        #region private fields and properties

        private readonly IAmpDevice _device;
        private bool _disposedValue;

        #endregion private fields and properties

        #region Constructors

        /// <summary>Creates an instance of LtAmplifier for the default USB connection</summary>
        //public LtAmplifier() : this(new UsbAmpDevice()) { }

        /// <summary>Creates an instance of LtAmplifier with a specific devices</summary>
        /// <param name="device">The device to connect with</param>
        public LtAmplifier(IAmpDevice device)
        {
            SetupMessageEventHandlers();
            _device = device;
            _device.MessageReceived += IAmpDevice_OnMessageReceived;
            _device.MessageSent += IAmpDevice_OnMessageSent;
            _device.DeviceOpened += IAmpDevice_Opened;
            _device.DeviceClosed += IAmpDevice_Closed;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>Open the connection to the amp</summary>
        /// <param name="continueTry">True to continue trying to connect until successful</param>
        public void Open(bool continueTry = true)
        {
            _device.Open(continueTry);
        }

        /// <summary>Asynchronously open the connection to the amp</summary>
        /// <param name="continueTry">True to continue trying to connect until successful</param>
        public async Task OpenAsync(bool continueTry = true)
        {
            TaskCompletionSource tcs = new();
            void eventHandler(object? sender, EventArgs eventArgs)
            {
                AmplifierConnected -= eventHandler;
                tcs.SetResult();
            }
            AmplifierConnected += eventHandler;
            Open(continueTry);
            await tcs.Task;
        }

        /// <summary>Closes the amp connection</summary>
        public void Close()
        {
            IsOpen = false;
            _device.Close();
        }

        /// <summary></summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_device != null)
                    {
                        _device.Close();
                        _device.Dispose();
                    }
                }
                _disposedValue = true;
            }
        }

        /// <summary></summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Sends a specific message to the amp</summary>
        /// <param name="message">Message to send</param>
        public void SendMessage(FenderMessageLT message)
        {
            _device.Write(message);
        }

        /// <summary>Asynchronously sends a specific message to the amp</summary>
        /// <param name="message">Message to send</param>
        /// <param name="responseMessage">The message to wait for</param>
        /// <returns>Resposne message from the amp</returns>
        public async Task<FenderMessageLT> SendMessageAsync(FenderMessageLT message, TypeOneofCase responseMessage = TypeOneofCase.None)
        {
            TaskCompletionSource<FenderMessageLT> tcs = new();
            void eventHandler(object? sender, FenderMessageEventArgs eventArgs)
            {
                if (eventArgs.MessageType == TypeOneofCase.None || eventArgs.MessageType == responseMessage)
                {
                    MessageReceived -= eventHandler;
                    tcs.SetResult(eventArgs.Message!);
                }
            }
            MessageReceived += eventHandler;
            SendMessage(message);
            return await tcs.Task;
        }

        #endregion Public methods

        #region private methods

        /// <summary>Initializes the amplifier connection after opening</summary>
        /// <param name="getData"></param>
        private void InitializeConnection(bool getData = true)
        {
            SetModalState(ModalContext.SyncBegin);
            Thread.Sleep(100);
            if (getData)
            {
                GetFirmwareVersion();
                Thread.Sleep(100);
                GetProductIdentification();
                Thread.Sleep(100);
            }
            SetModalState(ModalContext.SyncEnd);
        }

        #endregion private methods

        #region private event handlers

        /// <summary>Triggered when the device is opened</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IAmpDevice_Opened(object? sender, EventArgs e)
        {
            InitializeConnection();
            IsOpen = true;
            AmplifierConnected?.Invoke(this, null!);
        }

        /// <summary>Triggered when the device is closed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IAmpDevice_Closed(object? sender, EventArgs e)
        {
            IsOpen = false;
            AmplifierDisconnected?.Invoke(this, null!);
        }

        /// <summary>Internal trigger for data sent to the amp</summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void IAmpDevice_OnMessageSent(object? sender, FenderMessageEventArgs eventArgs)
        {
            MessageSent?.Invoke(this, eventArgs);
        }

        /// <summary>Internal trigger for data received from the amp</summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void IAmpDevice_OnMessageReceived(object? sender, FenderMessageEventArgs eventArgs)
        {
            if (MessageEventHandlers.TryGetValue(eventArgs.MessageType.GetValueOrDefault(), out Action<FenderMessageEventArgs>? value))
            {
                value(eventArgs);
            }
            else
            {
                UnknownMessageReceived?.Invoke(this, eventArgs);
            }
            MessageReceived?.Invoke(this, eventArgs);
        }

        #endregion private event handlers
    }
}