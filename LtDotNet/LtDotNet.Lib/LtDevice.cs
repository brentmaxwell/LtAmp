using Google.Protobuf.Collections;
using HidSharp;
using HidSharp.Reports;
using HidSharp.Reports.Input;
using LtDotNet.Lib.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Windows.Markup;
using LtDotNet.Lib.Model.Preset;
using LtDotNet.Lib.Model.Profile;
using LtDotNet.Lib.Extensions;

namespace LtDotNet.Lib
{
    public partial class LtDevice : IDisposable
    {

        #region public properties

        public bool IsOpen
        {
            get { return _isOpen; }
        }
        public static LtDeviceInfo DeviceInfo { get; set; }
        public static List<DspUnitDefinition> DspUnitDefinitions { get; set; }
        public ErrorType ErrorType { get; set; }

        #endregion

        #region private fields and properties

        private HidDevice _device { get; set; }
        private HidStream _deviceStream { get; set; }
        private ReportDescriptor _descriptor { get; set; }
        private HidDeviceInputReceiver _inputReceiver { get; set; }
        private bool _isOpen;
        private int _reportLength = 0;
        private byte[] _inputBuffer;
        private byte[] _dataBuffer = new byte[0];
        private bool disposedValue;

        private TimerCallback _heartbeatCallback { get; set; }
        
        #endregion
        public LtDevice()
        {
            DeviceInfo = new LtDeviceInfo();
            DeviceInfo.Presets = new List<Preset>(LtDeviceInfo.NUM_OF_PRESETS);
            for(var i = DeviceInfo.Presets.Count; i <= LtDeviceInfo.NUM_OF_PRESETS; i++)
            {
                DeviceInfo.Presets.Add(new Preset());
            }
        }
        
        public void Open(bool waitAndConnect = true)
        {
            DeviceConnected += LtDevice_DeviceConnected;
            _device = DeviceList.Local.GetHidDeviceOrNull(LtDeviceInfo.VENDOR_ID, LtDeviceInfo.PRODUCT_ID);
            if(_device != null)
            {
                Connect();
            }
            else
            {
                DeviceList.Local.Changed += UsbDevices_Changed;
            }
        }
        
        public void Connect()
        {
            _inputBuffer = new byte[_device.GetMaxInputReportLength()];
            _descriptor = _device.GetReportDescriptor();
            _inputReceiver = _descriptor.CreateHidDeviceInputReceiver();
            _inputReceiver.Received += InputReceiver_Received;
            _deviceStream = _device.Open();
            if (_deviceStream == null)
            {
                throw new IOException("Could not open connection to device");
            }
            _deviceStream.Closed += _deviceStream_Closed;
            _inputReceiver.Start(_deviceStream);
            InitializeConnection();
            Timer heartbeatTimer = new Timer(1000);
            heartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
            heartbeatTimer.Start();
            _isOpen = true;
            DeviceConnected.Invoke(this, null);
        }

        public void Close()
        {
            _deviceStream.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _deviceStream.Close();
                    _deviceStream.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void GetAllPresets()
        {
            for (int i = 1; i <= LtDeviceInfo.NUM_OF_PRESETS; i++)
            {
                GetPreset(i);
                Thread.Sleep(100);
            }
        }

        public void SendMessage(FenderMessageLT message)
        {
            foreach (var packet in message.ToUsbMessage())
            {
                _deviceStream.Write(packet);
            }
            if (message.TypeCase != FenderMessageLT.TypeOneofCase.Heartbeat)
            {
                MessageSent?.Invoke(message);
            }
        }

        #region private event handlers

        private void _deviceStream_Closed(object? sender, EventArgs e)
        {
            _isOpen = false;
            DeviceDisconnected.Invoke(this, null);
        }

        private void UsbDevices_Changed(object? sender, DeviceListChangedEventArgs e)
        {
            _device = DeviceList.Local.GetHidDeviceOrNull(LtDeviceInfo.VENDOR_ID, LtDeviceInfo.PRODUCT_ID);
            if (_device != null)
            {
                DeviceList.Local.Changed -= UsbDevices_Changed;
                Connect();
            }
        }

        private void LtDevice_DeviceConnected(object sender, EventArgs e)
        {
            ImportDspUnitDefinitions(DeviceInfo.ProductId);
        }

        private void HeartbeatTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SendMessage(new FenderMessageLT()
            {
                ResponseType = ResponseType.Unsolicited,
                Heartbeat = new Heartbeat()
                {
                    DummyField = true
                }
            });
        }

        private void InputReceiver_Received(object? sender, EventArgs e)
        {
            while (_inputReceiver.Stream.CanRead)
            {
                var inputBuffer = _inputReceiver.Stream.Read();
                var tag = inputBuffer[2];
                var length = inputBuffer[3];
                var bufferStart = _dataBuffer.Length;
                Array.Resize(ref _dataBuffer, _dataBuffer.Length + length);
                Buffer.BlockCopy(inputBuffer, 4, _dataBuffer, bufferStart, length);
                if (tag == (byte)UsbHidMessageTag.End)
                {
                    var message = FenderMessageLT.Parser.ParseFrom(_dataBuffer);
                    OnMessageReceived(message);
                    _dataBuffer = new byte[0];
                }
                inputBuffer = new byte[_reportLength];
            }
        }

        private void OnMessageReceived(FenderMessageLT message)
        {
            switch (message.TypeCase)
            {
                case FenderMessageLT.TypeOneofCase.AuditionPresetStatus:
                    DeviceInfo.AuditioningPreset = Preset.FromString(message.AuditionPresetStatus.PresetData);
                    AuditionPresetStatusMessageReceived?.Invoke(message.AuditionPresetStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.AuditionStateStatus:
                    DeviceInfo.IsAuditioning = message.AuditionStateStatus.IsAuditioning;
                    if (!DeviceInfo.IsAuditioning)
                    {
                        DeviceInfo.AuditioningPreset = null;
                    }
                    AuditionStateStatusMessageReceived?.Invoke(message.AuditionStateStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ClearPresetStatus:
                    ClearPresetStatusMessageReceived?.Invoke(message.ClearPresetStatus);
                    DeviceInfo.Presets[message.ClearPresetStatus.SlotIndex] = new Preset();
                    break;
                case FenderMessageLT.TypeOneofCase.ConnectionStatus:
                    DeviceInfo.IsConnected = message.ConnectionStatus.IsConnected;
                    ConnectionStatusMessageReceived?.Invoke(message.ConnectionStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentDisplayedPresetIndexStatus:
                    DeviceInfo.DisplayedPresetIndex = message.CurrentDisplayedPresetIndexStatus.CurrentDisplayedPresetIndex;
                    CurrentDisplayedPresetIndexStatusMessageReceived?.Invoke(message.CurrentDisplayedPresetIndexStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexStatus:
                    DeviceInfo.ActivePresetIndex = message.CurrentLoadedPresetIndexStatus.CurrentLoadedPresetIndex;
                    CurrentLoadedPresetIndexStatusMessageReceived?.Invoke(message.CurrentLoadedPresetIndexStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexBypassStatus:
                    CurrentLoadedPresetIndexBypassStatusMessageReceived?.Invoke(message.CurrentLoadedPresetIndexBypassStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.CurrentPresetStatus:
                    DeviceInfo.CurrentPreset = Preset.FromString(message.CurrentPresetStatus.CurrentPresetData);
                    CurrentPresetStatusMessageReceived?.Invoke(message.CurrentPresetStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.DspUnitParameterStatus:
                    DspUnitParameterStatusMessageReceived?.Invoke(message.DspUnitParameterStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ExitAuditionPresetStatus:
                    ExitAuditionPresetStatusMessageReceived?.Invoke(message.ExitAuditionPresetStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.FirmwareVersionStatus:
                    DeviceInfo.FirmwareVersion = message.FirmwareVersionStatus.Version;
                    FirmwareVersionStatusMessageReceived?.Invoke(message.FirmwareVersionStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.FrameBufferMessage:
                    FrameBufferMessageReceived?.Invoke(message.FrameBufferMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.IndexButton:
                    IndexButtonMessageReceived?.Invoke(message.IndexButton);
                    break;
                case FenderMessageLT.TypeOneofCase.IndexEncoder:
                    IndexEncoderMessageReceived?.Invoke(message.IndexEncoder);
                    break;
                case FenderMessageLT.TypeOneofCase.IndexPot:
                    IndexPotMessageReceived?.Invoke(message.IndexPot);
                    break;
                case FenderMessageLT.TypeOneofCase.LineOutGainStatus:
                    LineOutGainStatusMessageReceived?.Invoke(message.LineOutGainStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.Lt4FootswitchModeStatus:
                    LT4FootswitchModeStatusMessageReceived?.Invoke(message.Lt4FootswitchModeStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.MemoryUsageStatus:
                    DeviceInfo.MemoryUsageStatus = message.MemoryUsageStatus;
                    MemoryUsageStatusMessageReceived?.Invoke(message.MemoryUsageStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ModalStatusMessage:
                    DeviceInfo.ModalContext = message.ModalStatusMessage.Context;
                    DeviceInfo.ModalState = message.ModalStatusMessage.State;
                    ModalStatusMessageMessageReceived?.Invoke(message.ModalStatusMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.NewPresetSavedStatus:
                    NewPresetSavedStatusMessageReceived?.Invoke(message.NewPresetSavedStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.PresetEditedStatus:
                    DeviceInfo.IsPresetEdited = message.PresetEditedStatus.PresetEdited;
                    PresetEditedStatusMessageReceived?.Invoke(message.PresetEditedStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.PresetJSONMessage:
                    DeviceInfo.Presets[message.PresetJSONMessage.SlotIndex] = Preset.FromString(message.PresetJSONMessage.Data);
                    PresetJSONMessageReceived?.Invoke(message.PresetJSONMessage);
                    break;
                case FenderMessageLT.TypeOneofCase.PresetSavedStatus:
                    PresetSavedStatusMessageReceived?.Invoke(message.PresetSavedStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ProcessorUtilization:
                    DeviceInfo.ProcessorUtilization = message.ProcessorUtilization;
                    ProcessorUtilizationMessageReceived?.Invoke(message.ProcessorUtilization);
                    break;
                case FenderMessageLT.TypeOneofCase.ProductIdentificationStatus:
                    DeviceInfo.ProductId = message.ProductIdentificationStatus.Id;
                    ProductIdentificationStatusMessageReceived?.Invoke(message.ProductIdentificationStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.QASlotsStatus:
                    DeviceInfo.FootswitchPresets = message.QASlotsStatus.Slots.ToArray();
                    QASlotsStatusMessageReceived?.Invoke(message.QASlotsStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ReplaceNodeStatus:
                    ReplaceNodeStatusMessageReceived?.Invoke(message.ReplaceNodeStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.SetDspUnitParameterStatus:
                    SetDspUnitParameterStatusMessageReceived?.Invoke(message.SetDspUnitParameterStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ShiftPresetStatus:
                    ShiftPresetStatusMessageReceived?.Invoke(message.ShiftPresetStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.SwapPresetStatus:
                    SwapPresetStatusMessageReceived?.Invoke(message.SwapPresetStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.UnsupportedMessageStatus:
                    ErrorType = message.UnsupportedMessageStatus.Status;
                    UnsupportedMessageStatusReceived?.Invoke(message.UnsupportedMessageStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.UsbGainStatus:
                    DeviceInfo.UsbGain = message.UsbGainStatus.ValueDB;
                    UsbGainStatusMessageReceived?.Invoke(message.UsbGainStatus);
                    break;
                case FenderMessageLT.TypeOneofCase.ActiveDisplay:
                    ActiveDisplayMessageReceived?.Invoke(message.ActiveDisplay);
                    UnknownMessageReceived?.Invoke(message);
                    break;
                default:
                    break;
            }
            MessageReceived?.Invoke(message);
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
                GetLineOutGain();
                Thread.Sleep(100);
                GetQASlots();
                Thread.Sleep(100);
                GetUsbGain();
                Thread.Sleep(100);
            }
            SetModalState(ModalContext.SyncEnd);
        }

        private void ImportDspUnitDefinitions(string deviceType)
        {
            var rawData = File.ReadAllText(Path.Join(Environment.CurrentDirectory, "JsonDefinitions", "mustang", "dsp_units.json"));
            DspUnitDefinitions = JsonConvert.DeserializeObject<List<DspUnitDefinition>>(rawData);
        }

        #endregion

        #region Commands

        #region Auditioning

        // AuditionPreset
        // response: AuditionPresetStatus
        public void SetAuditionPreset(Preset preset)
        {
            SendMessage(MessageFactory.Create(new AuditionPreset() { PresetData = preset.ToString() }));
        }

        // AuditionStateRequest
        // response: AuditionStateStatus
        public void GetAuditionState()
        {
            SendMessage(MessageFactory.Create(new AuditionStateRequest() { Request = true }));
        }

        // ExitAuditionPreset
        // response: ExitAuditionPresetStatus
        public void ExitAuditionPreset(bool exitStatus = true)
        {
            SendMessage(MessageFactory.Create(new ExitAuditionPreset() { Exit = exitStatus }));
        }

        #endregion

        #region Preset management

        // CurrentPresetRequest
        // response: CurrentPresetStatus
        public void GetCurrentPreset()
        {
            SendMessage(MessageFactory.Create(new CurrentPresetRequest() { Request = true }));
        }

        // CurrentPresetSet
        // response: CurrentPresetStatus
        public void SetCurrentPreset(Preset preset)
        {
            SendMessage(MessageFactory.Create(new CurrentPresetSet() { CurrentPresetData = preset.ToString() }));
        }

        // LoadPreset
        // response: CurrentLoadedPresetIndexStatus
        public void LoadPreset(int slotIndex)
        {
            SendMessage(MessageFactory.Create(new LoadPreset() { PresetIndex = slotIndex }));
        }

        // ShiftPreset
        // response: ShiftPresetStatus
        public void ShiftPreset(int from, int to)
        {
            SendMessage(MessageFactory.Create(new ShiftPreset() { IndexToShiftFrom = from, IndexToShiftTo = to }));
        }

        // SwapPreset
        // response: SwapPresetStatus
        public void SwapPreset(int slotIndexA, int slotIndexB)
        {
            SendMessage(MessageFactory.Create(new SwapPreset() { IndexA = slotIndexA, IndexB = slotIndexB }));
        }

        // RetrievePreset
        // response: PresetJsonMessage
        public void GetPreset(int slotIndex)
        {
            SendMessage(MessageFactory.Create(new RetrievePreset() { Slot = slotIndex }));
        }
        
        // SaveCurrentPreset
        // response: PresetSavedStatus
        public void SaveCurrentPreset()
        {
            SendMessage(MessageFactory.Create(new SaveCurrentPreset() { Save = true }));
        }

        // SaveCurrentPresetTo
        // response: PresetSavedStatus
        public void SaveCurrentPresetTo(int slotIndex, string name)
        {
            SendMessage(MessageFactory.Create(new SaveCurrentPresetTo() { PresetName = name, PresetSlot = slotIndex }));
        }

        // SavePresetAs
        // response: PresetSavedStatus
        public void SavePresetAs(int slotIndex, Preset preset, bool loadPreset = true)
        {
            SendMessage(MessageFactory.Create(new SavePresetAs() { PresetSlot = slotIndex, PresetData = preset.ToString(), IsLoadPreset = loadPreset }));
        }

        // RenamePresetAt
        public void RenamePresetAt(int slotIndex, string name)
        {
            SendMessage(MessageFactory.Create(new RenamePresetAt() { PresetName = name, PresetSlot = slotIndex }));
        }


        // ClearPreset
        // response: ClearPresetStatus
        public void ClearPreset(int slotIndex, bool isLoadPreset = true)
        {
            SendMessage(MessageFactory.Create(new ClearPreset() { SlotIndex = slotIndex, IsLoadPreset = isLoadPreset }));
        }

        // ConnectionStatusRequest
        // response: ConnectionStatus
        public void GetConnectionStatus()
        {
            SendMessage(MessageFactory.Create(new ConnectionStatusRequest() { Request = true }));
        }

        // SetDspUnitParameter
        // response: setDspUnitParameterStatus
        public void SetDspUnitParameter(string nodeId, DspUnitParameter parameter)
        {
            var message = MessageFactory.Create(new SetDspUnitParameter()
            {
                NodeId = nodeId,
                ParameterId = parameter.Name
            });
            switch (parameter.ParameterType)
            {
                case DspUnitParameterType.Boolean:
                    message.SetDspUnitParameter.BoolParameter = parameter.Value;
                    break;
                case DspUnitParameterType.Integer:
                    message.SetDspUnitParameter.Sint32Parameter = parameter.Value;
                    break;
                case DspUnitParameterType.String:
                    message.SetDspUnitParameter.StringParameter = parameter.Value;
                    break;
                case DspUnitParameterType.Float:
                    message.SetDspUnitParameter.FloatParameter = parameter.Value;
                    break;
            }
            SendMessage(message);
        }

        // ReplaceNode
        public void ReplaceNode(string nodeId, string fenderId)
        {
            SendMessage(MessageFactory.Create(new ReplaceNode() { NodeIdToReplace = nodeId, FenderIdToReplaceWith = fenderId }));
        }

        #endregion

        #region device info

        // ModalState
        // response: ModalState
        public void SetModalState(ModalContext modalContext)
        {
            SendMessage(MessageFactory.Create(new ModalStatusMessage() { Context = modalContext, State = ModalState.Ok }));
        }

        // FirmwareVersionRequest
        // response: FirmwareVersionStatus
        public void GetFirmwareVersion()
        {
            SendMessage(MessageFactory.Create(new FirmwareVersionRequest() { Request = true }));
        }

        // MemoryUsageRequest
        public void GetMemoryUsage()
        {
            SendMessage(MessageFactory.Create(new MemoryUsageRequest() { Request = true }));
        }

        public void GetProcessorUtilization()
        {
            SendMessage(MessageFactory.Create(new ProcessorUtilizationRequest() { Request = true }));
        }

        public void GetProductIdentification()
        {
            SendMessage(MessageFactory.Create(new ProductIdentificationRequest() { Request = true }));
        }

        // Heartbeat
        public void Heartbeat()
        {
            SendMessage(MessageFactory.Create(new Heartbeat() { DummyField = true }));
        }

        public void GetQASlots()
        {
            SendMessage(MessageFactory.Create(new QASlotsRequest() { Request = true }));
        }

        public void SetQASlots(uint[] slotIndexes)
        {
            var message = MessageFactory.Create(new QASlotsSet());
            message.QASlotsSet.Slots.Add(slotIndexes);
            SendMessage(message);
        }

        public void GetUsbGain()
        {
            SendMessage(MessageFactory.Create(new UsbGainRequest() { Request = true }));
        }

        public void SetUsbGain(float value)
        {
            SendMessage(MessageFactory.Create(new UsbGainSet() { ValueDB = value }));
        }

        #endregion

        #region pre-formed commands

        public void SetTuner(bool on_off)
        {
            SetModalState(on_off ? ModalContext.TunerEnable : ModalContext.TunerDisable);
        }

        #endregion

        #region Unknown messages
        // ActiveDisplay
        public void ActiveDisplay(string pageName)
        {
            SendMessage(MessageFactory.Create(new ActiveDisplay() { PageName = pageName }));
        }

        // PresetJSONMessageRequest_LT
        public void GetPresetLT(int request)
        {
            SendMessage(MessageFactory.Create(new PresetJSONMessageRequest_LT() { Request = request }));
        }

        // FrameBufferMessageRequest
        // response: FrameBufferMessage
        public void GetFramebuffer()
        {
            SendMessage(MessageFactory.Create(new FrameBufferMessageRequest() { Request = true }));
        }

        // LineOutGainRequest
        // response: LineOutGainStatus
        public void GetLineOutGain()
        {
            SendMessage(MessageFactory.Create(new LineOutGainRequest() { Request = true }));
        }

        // LineOutGainSet
        // response: LineOutGainStatus
        public void SetLineOutGain(float value)
        {
            SendMessage(MessageFactory.Create(new LineOutGainSet() { ValueDB = value }));
        }

        public void LoopbackTest(string data)
        {
            SendMessage(MessageFactory.Create(new LoopbackTest() { Data = data }));
        }

        public void GetLt4FootswitchMode()
        {
            SendMessage(MessageFactory.Create(new LT4FootswitchModeRequest() { Request = true }));
        }

        #endregion

        #endregion

    }

    public enum UsbHidMessageTag
    {
        Start = 0x33,
        Continue = 0x34,
        End = 0x35,
    }
}
