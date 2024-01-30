using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmplifier
    {
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

        #region Device Info

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

        public void GetAllPresets()
        {
            for (int i = 1; i <= NUM_OF_PRESETS; i++)
            {
                GetPreset(i);
                Thread.Sleep(100);
            }
        }

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
    }
}