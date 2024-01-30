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

        public bool IsOpen
        {
            get { return _isOpen; }
        }
        
        public ErrorType ErrorType { get; set; }
        
        #endregion

        #region private fields and properties

        private IAmpDevice _device { get; set; }
        private bool _isOpen;
        private bool _disposedValue;
        
        #endregion

        public LtAmplifier() : this(new UsbAmpDevice()){ }

        public LtAmplifier(IAmpDevice device)
        {
            _device = device;
            ImportDspUnitDefinitions();
        }
        
        public void Open(bool continueTry = true)
        {
            _device.MessageReceived += IAmpDevice_OnMessageReceived;
            _device.MessageSent += IAmpDevice_OnMessageSent;
            _device.DeviceOpened += IAmpDevice_Opened;
            _device.DeviceClosed += IAmpDevice_Closed;
            _device.Open(continueTry);
        }

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

        public void SendMessage(FenderMessageLT message)
        {
            _device.Write(message);
        }

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
            switch (eventArgs.MessageType)
            {
                case FenderMessageLT.TypeOneofCase.AuditionPresetStatus:
                    AuditionPresetStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.AuditionStateStatus:
                    AuditionStateStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ClearPresetStatus:
                    ClearPresetStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ConnectionStatus:
                    ConnectionStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentDisplayedPresetIndexStatus:
                    CurrentDisplayedPresetIndexStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexStatus:
                    CurrentLoadedPresetIndexStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexBypassStatus:
                    CurrentLoadedPresetIndexBypassStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentPresetStatus:
                    CurrentPresetStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.DspUnitParameterStatus:
                    DspUnitParameterStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ExitAuditionPresetStatus:
                    ExitAuditionPresetStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.FirmwareVersionStatus:
                    FirmwareVersionStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.FrameBufferMessage:
                    FrameBufferMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.IndexButton:
                    IndexButtonMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.IndexEncoder:
                    IndexEncoderMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.IndexPot:
                    IndexPotMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.LineOutGainStatus:
                    LineOutGainStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.Lt4FootswitchModeStatus:
                    LT4FootswitchModeStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.MemoryUsageStatus:
                    MemoryUsageStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ModalStatusMessage:
                    ModalStatusMessageMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.NewPresetSavedStatus:
                    NewPresetSavedStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.PresetEditedStatus:
                    PresetEditedStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.PresetJSONMessage:
                    PresetJSONMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.PresetSavedStatus:
                    PresetSavedStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ProcessorUtilization:
                    ProcessorUtilizationMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ProductIdentificationStatus:
                    ProductIdentificationStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.QASlotsStatus:
                    QASlotsStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ReplaceNodeStatus:
                    ReplaceNodeStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.SetDspUnitParameterStatus:
                    SetDspUnitParameterStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ShiftPresetStatus:
                    ShiftPresetStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.SwapPresetStatus:
                    SwapPresetStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.UnsupportedMessageStatus:
                    ErrorType = eventArgs.Message.UnsupportedMessageStatus.Status;
                    UnsupportedMessageStatusReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.UsbGainStatus:
                    UsbGainStatusMessageReceived?.Invoke(this, eventArgs);
                    break;
                case FenderMessageLT.TypeOneofCase.ActiveDisplay:
                    ActiveDisplayMessageReceived?.Invoke(this, eventArgs);
                    break;
                default:
                    UnknownMessageReceived?.Invoke(this, eventArgs);
                    break;
            }
            MessageReceived?.Invoke(this, eventArgs);
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
    }
}
