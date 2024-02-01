using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;

namespace LtAmpDotNet.Lib
{
    /// <summary>
    /// Represents an amplifier device
    /// </summary>
    public partial class LtAmplifier : IDisposable
    {
        /// <summary>number of presets available in the amplifier</summary>
        public const int NUM_OF_PRESETS = 60;

        /// <summary>definitions of all parts of the presets and DspUnits in the amplifier</summary>
        public static List<DspUnitDefinition>? DspUnitDefinitions { get; set; }
        
        /// <summary>Imports the DspUnit definitions from the JSON configuration files</summary>
        private static void ImportDspUnitDefinitions()
        {
            try
            {
                var rawData = File.ReadAllText(Path.Join(Environment.CurrentDirectory, "JsonDefinitions", "mustang", "dsp_units.json"));
                DspUnitDefinitions = JsonConvert.DeserializeObject<List<DspUnitDefinition>>(rawData);
            }
            catch (Exception ex)
            {
                throw (new Exception($"Exception opening DspUnit definitions file: {ex.Message}", ex));
            }
        }

        #region public properties

        /// <summary>Current state of the amp connection</summary>
        public bool IsOpen => _isOpen;

        /// <summary>Contains an error type when the amp send an UnsupportedMessageStatus message</summary>
        public ErrorType ErrorType { get; set; }

        #endregion

        #region private fields and properties

        private readonly IAmpDevice _device;
        private bool _isOpen;
        private bool _disposedValue;

        #endregion

        #region Constructors

        /// <summary>Creates an instance of LtAmplifier for the default USB connection</summary>
        public LtAmplifier() : this(new UsbAmpDevice()){ }

        /// <summary>Creates an instance of LtAmplifier with a specific devices</summary>
        /// <param name="device">The device to connect with</param>
        /// <param name="importDspDefinitions">False to ignore the json dsp unit definitions</param>
        public LtAmplifier(IAmpDevice device, bool importDspDefinitions = true)
        {
            SetupMessageEventHandlers();
            _device = device;
            if(importDspDefinitions) ImportDspUnitDefinitions();
        }

        #endregion

        #region Public methods

        /// <summary>Open the connection to the amp</summary>
        /// <param name="continueTry">True to continue trying to connect until successful</param>
        public void Open(bool continueTry = true)
        {
            _device.MessageReceived += IAmpDevice_OnMessageReceived;
            _device.MessageSent += IAmpDevice_OnMessageSent;
            _device.DeviceOpened += IAmpDevice_Opened;
            _device.DeviceClosed += IAmpDevice_Closed;
            _device.Open(continueTry);
        }

        /// <summary>Closes the amp connection</summary>
        public void Close()
        {
            _isOpen = false;
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
                    if(_device != null)
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
        public void SendMessage(FenderMessageLT message) => _device.Write(message);

        #endregion

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
                GetQASlots();
                Thread.Sleep(100);
                GetUsbGain();
                Thread.Sleep(100);
            }
            SetModalState(ModalContext.SyncEnd);
        }

        #endregion   

        #region private event handlers

        /// <summary>Triggered when the device is opened</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IAmpDevice_Opened(object? sender, EventArgs e){
            InitializeConnection();
            AmplifierConnected?.Invoke(this, null!);
            _isOpen = true;
        }

        /// <summary>Triggered when the device is closed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IAmpDevice_Closed(object? sender, EventArgs e){
            _isOpen = false;
            AmplifierDisconnected?.Invoke(this, null!);
        }

        /// <summary>Internal trigger for data sent to the amp</summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void IAmpDevice_OnMessageSent(object sender, FenderMessageEventArgs eventArgs) => MessageSent?.Invoke(this, eventArgs);

        /// <summary>Internal trigger for data received from the amp</summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void IAmpDevice_OnMessageReceived(object sender, FenderMessageEventArgs eventArgs)
        {
            if (MessageEventHandlers.TryGetValue(eventArgs.MessageType, out Action<FenderMessageEventArgs>? value))
            {
                value(eventArgs);
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
