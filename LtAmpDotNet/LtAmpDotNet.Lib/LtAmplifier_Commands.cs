using LtAmpDotNet.Lib.Model;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Lib
{
    public partial class LtAmplifier
    {
        #region Auditioning

        /// <summary>
        /// Sets the audition preset
        /// </summary>
        /// <param name="preset">The preset to audition</param>
        public void SetAuditionPreset(Preset preset)
        {
            SendMessage(MessageFactory.Create(new AuditionPreset() { PresetData = preset.ToString() }));
        }

        /// <summary>
        /// Gets the current audition state
        /// </summary>
        public void GetAuditionState()
        {
            SendMessage(MessageFactory.Create(new AuditionStateRequest() { Request = true }));
        }

        /// <summary>
        /// Exits audition mode
        /// </summary>
        /// <param name="exitStatus"></param>
        public void ExitAuditionPreset(bool exitStatus = true)
        {
            SendMessage(MessageFactory.Create(new ExitAuditionPreset() { Exit = exitStatus }));
        }

        #endregion

        #region Preset management

        /// <summary>
        /// Gets the current preset
        /// </summary>
        public void GetCurrentPreset()
        {
            SendMessage(MessageFactory.Create(new CurrentPresetRequest() { Request = true }));
        }

        /// <summary>
        /// Sets the current preset
        /// </summary>
        /// <param name="preset">The preset to set</param>
        public void SetCurrentPreset(Preset preset)
        {
            SendMessage(MessageFactory.Create(new CurrentPresetSet() { CurrentPresetData = preset.ToString() }));
        }

        /// <summary>
        /// Changes to the specified preset
        /// </summary>
        /// <param name="slotIndex">The bank index to switch to</param>
        public void LoadPreset(int slotIndex)
        {
            SendMessage(MessageFactory.Create(new LoadPreset() { PresetIndex = slotIndex }));
        }

        /// <summary>
        /// Moves the specified preset to a new position, and shifts the other presets
        /// </summary>
        /// <param name="from">The preset to shift</param>
        /// <param name="to">The location to shift it to</param>
        public void ShiftPreset(int from, int to)
        {
            SendMessage(MessageFactory.Create(new ShiftPreset() { IndexToShiftFrom = from, IndexToShiftTo = to }));
        }

        /// <summary>
        /// Swaps two preset locations
        /// </summary>
        /// <param name="slotIndexes">The location of the presets to swap</param>
        public void SwapPreset(int[] slotIndexes)
        {
            SendMessage(MessageFactory.Create(new SwapPreset() { IndexA = slotIndexes[0], IndexB = slotIndexes[1] }));
        }

        /// <summary>
        /// Gets the preset in the specified bank
        /// </summary>
        /// <param name="slotIndex">The bank to retrieve</param>
        public void GetPreset(int slotIndex)
        {
            SendMessage(MessageFactory.Create(new RetrievePreset() { Slot = slotIndex }));
        }

        /// <summary>
        /// Saves the current preset in the current bank.
        /// response: PresetSavedStatus
        /// </summary>
        public void SaveCurrentPreset()
        {
            SendMessage(MessageFactory.Create(new SaveCurrentPreset() { Save = true }));
        }

        /// <summary>
        /// Saves the current preset to the specified bank with the specified name
        /// response: PresetSavedStatus
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <param name="name"></param>
        public void SaveCurrentPresetTo(int slotIndex, string name)
        {
            SendMessage(MessageFactory.Create(new SaveCurrentPresetTo() { PresetName = name, PresetSlot = slotIndex }));
        }

        /// <summary>
        /// Saves a preset to a specified bank
        /// response: PresetSavedStatus
        /// </summary>
        /// <param name="slotIndex">The bank to save to</param>
        /// <param name="preset">The preset to save</param>
        /// <param name="loadPreset">Unknown; the official application always sends true</param>
        public void SavePresetAs(int slotIndex, Preset preset, bool loadPreset = true)
        {
            SendMessage(MessageFactory.Create(new SavePresetAs() { PresetSlot = slotIndex, PresetData = preset.ToString(), IsLoadPreset = loadPreset }));
        }

        /// <summary>
        /// Renames the preset at the specified bank
        /// </summary>
        /// <param name="slotIndex">The bank to rename</param>
        /// <param name="name">The new preset name</param>
        public void RenamePresetAt(int slotIndex, string name)
        {
            SendMessage(MessageFactory.Create(new RenamePresetAt() { PresetName = name, PresetSlot = slotIndex }));
        }


        /// <summary>
        /// Clears the preset in the specified bank
        /// response: ClearPresetStatus
        /// </summary>
        /// <param name="slotIndex">The bank to clear</param>
        /// <param name="isLoadPreset">Unknown; the official application always sends true</param>

        public void ClearPreset(int slotIndex, bool isLoadPreset = true)
        {
            SendMessage(MessageFactory.Create(new ClearPreset() { SlotIndex = slotIndex, IsLoadPreset = isLoadPreset }));
        }

        /// <summary>
        /// Gets the current connection status as reported by the amp 
        /// response: ConnectionStatus
        /// </summary>
        public void GetConnectionStatus()
        {
            SendMessage(MessageFactory.Create(new ConnectionStatusRequest() { Request = true }));
        }

        /// <summary>
        /// Sets a single parameter in a DspUnit 
        /// response: setDspUnitParameterStatus
        /// </summary>
        /// <param name="nodeId">The unit type to set</param>
        /// <param name="parameter">The parameter to set</param>
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
        public void ReplaceNode(string nodeId, string fenderId)
        {
            SendMessage(MessageFactory.Create(new ReplaceNode() { NodeIdToReplace = nodeId, FenderIdToReplaceWith = fenderId }));
        }

        #endregion

        #region Device Info

        /// <summary>
        /// Sets the modal state of the amp
        /// response: ModalState
        /// </summary>
        /// <param name="modalContext">The state to set</param>
        public void SetModalState(ModalContext modalContext)
        {
            SendMessage(MessageFactory.Create(new ModalStatusMessage() { Context = modalContext, State = ModalState.Ok }));
        }

        /// <summary>
        /// Requests the current firmware version of the amp
        /// response: FirmwareVersionStatus
        /// </summary>
        public void GetFirmwareVersion()
        {
            SendMessage(MessageFactory.Create(new FirmwareVersionRequest() { Request = true }));
        }

        /// <summary>
        /// Gets the memory usage of the amp
        /// response: MemoryUsageStatus
        /// </summary>
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

        /// <summary>retrieves all presets from the amp</summary>
        public void GetAllPresets()
        {
            for (int i = 1; i <= NUM_OF_PRESETS; i++)
            {
                GetPreset(i);
                Thread.Sleep(100);
            }
        }

        /// <summary>Sets the tuner on or off</summary>
        /// <param name="on_off">True for on; false for off</param>
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