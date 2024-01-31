using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmplifier : IDisposable
    {
        public const int NUM_OF_PRESETS = 60;
        public static List<DspUnitDefinition>? DspUnitDefinitions { get; set; }

        #region public properties

        /// <summary>
        /// Current state of the amp connection
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen; }
        }

        /// <summary>
        /// Contains an error type when the amp send an UnsupportedMessageStatus message
        /// </summary>
        public ErrorType ErrorType { get; set; }
        
        #endregion

        #region private fields and properties

        private IAmpDevice _device { get; set; }
        private bool _isOpen;
        private bool _disposedValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of LtAmplifier for the default USB connection
        /// </summary>
        public LtAmplifier() : this(new UsbAmpDevice()){ }


        /// <summary>
        /// Creates an instance of LtAmplifier with a specific devices
        /// </summary>
        /// <param name="device">The device to connect with</param>
        public LtAmplifier(IAmpDevice device)
        {
            SetupEventHandlers();
            _device = device;
            ImportDspUnitDefinitions();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Open the connection to the amp
        /// </summary>
        /// <param name="continueTry">True to continue trying to connect until successful</param>
        public void Open(bool continueTry = true)
        {
            _device.MessageReceived += IAmpDevice_OnMessageReceived;
            _device.MessageSent += IAmpDevice_OnMessageSent;
            _device.DeviceOpened += IAmpDevice_Opened;
            _device.DeviceClosed += IAmpDevice_Closed;
            _device.Open(continueTry);
        }

        /// <summary>
        /// Closes the amp connection
        /// </summary>
        public void Close()
        {
            _isOpen = false;
            _device.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if(_device != null)
                    {
                        _device.Close();
                        _device.Dispose();
                    }
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Sends a specific message to the amp
        /// </summary>
        /// <param name="message">Message to send</param>
        public void SendMessage(FenderMessageLT message)
        {
            _device.Write(message);
        }

        #endregion

        #region private methods

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
                GetQASlots();
                Thread.Sleep(100);
                GetUsbGain();
                Thread.Sleep(100);
            }
            SetModalState(ModalContext.SyncEnd);
        }

        private void ImportDspUnitDefinitions(string? deviceType = null)
        {
            var rawData = File.ReadAllText(Path.Join(Environment.CurrentDirectory, "JsonDefinitions", "mustang", "dsp_units.json"));
            DspUnitDefinitions = JsonConvert.DeserializeObject<List<DspUnitDefinition>>(rawData);
        }

        #endregion   

        #region private event handlers

        private void IAmpDevice_Opened(object? sender, EventArgs e){
            InitializeConnection();
            AmplifierConnected?.Invoke(this, null!);
            _isOpen = true;
        }

        private void IAmpDevice_Closed(object? sender, EventArgs e){
            _isOpen = false;
            AmplifierDisconnected?.Invoke(this, null!);
        }

        private void IAmpDevice_OnMessageSent(object sender, FenderMessageEventArgs eventArgs) => MessageSent?.Invoke(this, eventArgs);

        private void IAmpDevice_OnMessageReceived(object sender, FenderMessageEventArgs eventArgs)
        {
            if (MessageEventHandlers.ContainsKey(eventArgs.MessageType))
            {
                MessageEventHandlers[eventArgs.MessageType](eventArgs);
            }
            else
            {
                UnknownMessageReceived?.Invoke(this, eventArgs);
            }
            MessageReceived?.Invoke(this, eventArgs);
        }

        #endregion

        
    }
}
