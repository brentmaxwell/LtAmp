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

        #endregion Auditioning

        #region Preset management

        /// <summary>Gets the current preset</summary>
        public void GetCurrentPreset()
        {
            SendMessage(MessageFactory.Create(new CurrentPresetRequest() { Request = true }));
        }

        /// <summary>Asynchronously gets the current preset</summary>
        /// <returns>The current preset</returns>
        public async Task<CurrentPresetStatus> GetCurrentPresetAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new CurrentPresetRequest() { Request = true }), FenderMessageLT.TypeOneofCase.CurrentPresetStatus);
            return result.CurrentPresetStatus;
        }

        /// <summary>Sets the current preset</summary>
        /// <param name="preset">The preset to set</param>
        public void SetCurrentPreset(Preset preset)
        {
            SendMessage(MessageFactory.Create(new CurrentPresetSet() { CurrentPresetData = preset.ToString() }));
        }

        /// <summary>Asynchronously sets the current preset</summary>
        /// <param name="preset">The preset to set</param>
        /// <returns>the current preset status</returns>
        public async Task<CurrentPresetStatus> SetCurrentPresetAsync(Preset preset)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new CurrentPresetSet() { CurrentPresetData = preset.ToString() }), FenderMessageLT.TypeOneofCase.CurrentPresetStatus);
            return result.CurrentPresetStatus;
        }

        /// <summary>Changes to the specified preset</summary>
        /// <param name="slotIndex">The bank index to switch to</param>
        public void LoadPreset(int slotIndex)
        {
            SendMessage(MessageFactory.Create(new LoadPreset() { PresetIndex = slotIndex }));
        }

        /// <summary>Asynchronously changes to the specified preset</summary>
        /// <param name="slotIndex">The bank index to switch to</param>
        /// <returns>The new current loaded preset</returns>
        public async Task<CurrentLoadedPresetIndexStatus> LoadPresetAsync(int slotIndex)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new LoadPreset() { PresetIndex = slotIndex }), FenderMessageLT.TypeOneofCase.CurrentLoadedPresetIndexStatus);
            return result.CurrentLoadedPresetIndexStatus;
        }

        /// <summary>Moves the specified preset to a new position, and shifts the other presets</summary>
        /// <param name="from">The preset to shift</param>
        /// <param name="to">The location to shift it to</param>
        public void ShiftPreset(int from, int to)
        {
            SendMessage(MessageFactory.Create(new ShiftPreset() { IndexToShiftFrom = from, IndexToShiftTo = to }));
        }

        /// <summary>Asynchronously moves the specified preset to a new position, and shifts the other presets</summary>
        /// <param name="from">The preset to shift</param>
        /// <param name="to">The location to shift it to</param>
        /// <returns>Status message confirming the shift</returns>
        public async Task<ShiftPresetStatus> ShiftPresetAsync(int from, int to)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ShiftPreset() { IndexToShiftFrom = from, IndexToShiftTo = to }), FenderMessageLT.TypeOneofCase.ShiftPresetStatus);
            return result.ShiftPresetStatus;
        }

        /// <summary>Swaps two preset locations</summary>
        /// <param name="indexA">First index to swap</param>
        /// <param name="indexB">Second index to swap</param>
        public void SwapPreset(int indexA, int indexB)
        {
            SendMessage(MessageFactory.Create(new SwapPreset() { IndexA = indexA, IndexB = indexB }));
        }

        /// <summary>Asynchronously swaps two preset locations</summary>
        /// <param name="indexA">First index to swap</param>
        /// <param name="indexB">Second index to swap</param>
        /// <returns>Status message confirming the swap</returns>
        public async Task<SwapPresetStatus> SwapPresetAsync(int indexA, int indexB)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new SwapPreset() { IndexA = indexA, IndexB = indexB }), FenderMessageLT.TypeOneofCase.SwapPresetStatus);
            return result.SwapPresetStatus;
        }

        /// <summary>Gets the preset in the specified bank</summary>
        /// <param name="slotIndex">The bank to retrieve</param>
        public void GetPreset(int slotIndex)
        {
            SendMessage(MessageFactory.Create(new RetrievePreset() { Slot = slotIndex }));
        }

        /// <summary>Asynchronously gets the preset in the specified bank</summary>
        /// <param name="slotIndex">The bank to retrieve</param>
        /// <returns>The preset in the requested bank</returns>
        public async Task<PresetJSONMessage> GetPresetAsync(int slotIndex)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new RetrievePreset() { Slot = slotIndex }), FenderMessageLT.TypeOneofCase.PresetJSONMessage);
            return result.PresetJSONMessage;
        }

        /// <summary>Saves the current preset in the current bank.</summary>
        public void SaveCurrentPreset()
        {
            SendMessage(MessageFactory.Create(new SaveCurrentPreset() { Save = true }));
        }

        /// <summary>Asynchronously saves the current preset in the current bank.</summary>
        /// <returns>Status message confirming the operation</returns>
        public async Task<PresetSavedStatus> SaveCurrentPresetAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new SaveCurrentPreset() { Save = true }), FenderMessageLT.TypeOneofCase.PresetSavedStatus);
            return result.PresetSavedStatus;
        }

        /// <summary>Saves the current preset to the specified bank with the specified name</summary>
        /// <param name="slotIndex"></param>
        /// <param name="name"></param>
        public void SaveCurrentPresetTo(int slotIndex, string name)
        {
            SendMessage(MessageFactory.Create(new SaveCurrentPresetTo() { PresetName = name, PresetSlot = slotIndex }));
        }

        /// <summary>Asynchronously saves the current preset to the specified bank with the specified name</summary>
        /// <param name="slotIndex"></param>
        /// <param name="name"></param>
        /// <returns>Status message confirming the operation</returns>
        public async Task<PresetSavedStatus> SaveCurrentPresetToAsync(int slotIndex, string name)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new SaveCurrentPresetTo() { PresetName = name, PresetSlot = slotIndex }), FenderMessageLT.TypeOneofCase.PresetSavedStatus);
            return result.PresetSavedStatus;
        }

        /// <summary>Saves a preset to a specified bank</summary>
        /// <param name="slotIndex">The bank to save to</param>
        /// <param name="preset">The preset to save</param>
        /// <param name="loadPreset">Unknown; the official application always sends true</param>
        public void SavePresetAs(int slotIndex, Preset preset, bool loadPreset = true)
        {
            SendMessage(MessageFactory.Create(new SavePresetAs() { PresetSlot = slotIndex, PresetData = preset.ToString(), IsLoadPreset = loadPreset }));
        }

        /// <summary>Saves a preset to a specified bank</summary>
        /// <param name="slotIndex">The bank to save to</param>
        /// <param name="preset">The preset to save</param>
        /// <param name="loadPreset">Unknown; the official application always sends true</param>
        public async Task<PresetSavedStatus> SavePresetAsAsync(int slotIndex, Preset preset, bool loadPreset = true)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new SavePresetAs() { PresetSlot = slotIndex, PresetData = preset.ToString(), IsLoadPreset = loadPreset }), FenderMessageLT.TypeOneofCase.PresetSavedStatus);
            return result.PresetSavedStatus;
        }

        /// <summary>Renames the preset at the specified bank</summary>
        /// <param name="slotIndex">The bank to rename</param>
        /// <param name="name">The new preset name</param>
        public void RenamePresetAt(int slotIndex, string name)
        {
            SendMessage(MessageFactory.Create(new RenamePresetAt() { PresetName = name, PresetSlot = slotIndex }));
        }

        /// <summary>Asynchronously renames the preset at the specified bank</summary>
        /// <param name="slotIndex">The bank to rename</param>
        /// <param name="name">The new preset name</param>
        public async Task<PresetSavedStatus> RenamePresetAtAsync(int slotIndex, string name)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new RenamePresetAt() { PresetName = name, PresetSlot = slotIndex }), FenderMessageLT.TypeOneofCase.PresetSavedStatus);
            return result.PresetSavedStatus;
        }

        /// <summary>Clears the preset in the specified bank</summary>
        /// <param name="slotIndex">The bank to clear</param>
        /// <param name="isLoadPreset">Unknown; the official application always sends true</param>
        public void ClearPreset(int slotIndex, bool isLoadPreset = true)
        {
            SendMessage(MessageFactory.Create(new ClearPreset() { SlotIndex = slotIndex, IsLoadPreset = isLoadPreset }));
        }

        /// <summary>Asynchronously clears the preset in the specified bank</summary>
        /// <param name="slotIndex">The bank to clear</param>
        /// <param name="isLoadPreset">Unknown; the official application always sends true</param>
        /// <returns>A status message confirming the operation</returns>
        public async Task<ClearPresetStatus> ClearPresetAsync(int slotIndex, bool isLoadPreset = true)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ClearPreset() { SlotIndex = slotIndex, IsLoadPreset = isLoadPreset }), FenderMessageLT.TypeOneofCase.ClearPresetStatus);
            return result.ClearPresetStatus;
        }

        /// <summary>Gets the current connection status as reported by the amp</summary>
        public void GetConnectionStatus()
        {
            SendMessage(MessageFactory.Create(new ConnectionStatusRequest() { Request = true }));
        }

        /// <summary>Asynchronously gets the current connection status as reported by the amp </summary>
        /// <returns>A message with the connection status</returns>
        public async Task<ConnectionStatus> GetConnectionStatusAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ConnectionStatusRequest() { Request = true }), FenderMessageLT.TypeOneofCase.ConnectionStatus);
            return result.ConnectionStatus;
        }

        /// <summary>Sets a single parameter in a DspUnit</summary>
        /// <param name="nodeId">The unit type to set</param>
        /// <param name="parameter">The parameter to set</param>
        public void SetDspUnitParameter(NodeIdType nodeId, DspUnitParameter parameter)
        {
            FenderMessageLT message = MessageFactory.Create(new SetDspUnitParameter()
            {
                NodeId = nodeId.ToString(),
                ParameterId = parameter.Name
            });
            dynamic p = parameter.ParameterType switch
            {
                DspUnitParameterDataType.Boolean => message.SetDspUnitParameter.BoolParameter = parameter.Value,
                DspUnitParameterDataType.Integer => message.SetDspUnitParameter.Sint32Parameter = parameter.Value,
                DspUnitParameterDataType.String => message.SetDspUnitParameter.StringParameter = parameter.Value,
                DspUnitParameterDataType.Float => message.SetDspUnitParameter.FloatParameter = parameter.Value,
            };
            SendMessage(message);
        }

        /// <summary>Asynchronously sets a single parameter in a DspUnit</summary>
        /// <param name="nodeId">The unit type to set</param>
        /// <param name="parameter">The parameter to set</param>
        /// <returns>A status message confirming the operation</returns>
        public async Task<SetDspUnitParameterStatus> SetDspUnitParameterAsync(NodeIdType nodeId, DspUnitParameter parameter)
        {
            FenderMessageLT message = MessageFactory.Create(new SetDspUnitParameter()
            {
                NodeId = nodeId.ToString(),
                ParameterId = parameter.Name
            });
            dynamic p = parameter.ParameterType switch
            {
                DspUnitParameterDataType.Boolean => message.SetDspUnitParameter.BoolParameter = parameter.Value,
                DspUnitParameterDataType.Integer => message.SetDspUnitParameter.Sint32Parameter = parameter.Value,
                DspUnitParameterDataType.String => message.SetDspUnitParameter.StringParameter = parameter.Value,
                DspUnitParameterDataType.Float => message.SetDspUnitParameter.FloatParameter = parameter.Value,
            };
            FenderMessageLT result = await SendMessageAsync(message, FenderMessageLT.TypeOneofCase.SetDspUnitParameterStatus);
            return result.SetDspUnitParameterStatus;
        }

        /// <summary>Replaces a node in the current preset</summary>
        /// <param name="nodeId"></param>
        /// <param name="fenderId"></param>
        public void ReplaceNode(NodeIdType nodeId, string fenderId)
        {
            SendMessage(MessageFactory.Create(new ReplaceNode() { NodeIdToReplace = nodeId.ToString(), FenderIdToReplaceWith = fenderId }));
        }

        /// <summary>Asynchronously replaces a node in the current preset</summary>
        /// <param name="nodeId"></param>
        /// <param name="fenderId"></param>
        /// <returns>A status message confirming the operation</returns>
        public async Task<ReplaceNodeStatus> ReplaceNodeAsync(NodeIdType nodeId, string fenderId)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ReplaceNode() { NodeIdToReplace = nodeId.ToString(), FenderIdToReplaceWith = fenderId }), FenderMessageLT.TypeOneofCase.ReplaceNodeStatus);
            return result.ReplaceNodeStatus;
        }

        #endregion Preset management

        #region Device Info

        /// <summary>Sets the modal state of the amp</summary>
        /// <param name="modalContext">The state to set</param>
        public void SetModalState(ModalContext modalContext)
        {
            SendMessage(MessageFactory.Create(new ModalStatusMessage() { Context = modalContext, State = ModalState.Ok }));
        }

        /// <summary>Asynchronously sets the modal state of the amp</summary>
        /// <param name="modalContext">The state to set</param>
        /// <returns>A message confirming the operation</returns>
        public async Task<ModalStatusMessage> SetModalStateAsync(ModalContext modalContext)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ModalStatusMessage() { Context = modalContext, State = ModalState.Ok }), FenderMessageLT.TypeOneofCase.ModalStatusMessage);
            return result.ModalStatusMessage;
        }

        /// <summary>Requests the current firmware version of the amp</summary>
        public void GetFirmwareVersion()
        {
            SendMessage(MessageFactory.Create(new FirmwareVersionRequest() { Request = true }));
        }

        /// <summary>Asynchronously requests the current firmware version of the amp</summary>
        /// <returns>A message containing the current firmware version</returns>
        public async Task<FirmwareVersionStatus> GetFirmwareVersionAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new FirmwareVersionRequest() { Request = true }), FenderMessageLT.TypeOneofCase.FirmwareVersionStatus);
            return result.FirmwareVersionStatus;
        }

        /// <summary>Gets the memory usage of the amp</summary>
        public void GetMemoryUsage()
        {
            SendMessage(MessageFactory.Create(new MemoryUsageRequest() { Request = true }));
        }

        /// <summary>Asynchronously gets the memory usage of the amp</summary>
        /// <returns>A message with the current memory usage</returns>
        public async Task<MemoryUsageStatus> GetMemoryUsageAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new MemoryUsageRequest() { Request = true }), FenderMessageLT.TypeOneofCase.MemoryUsageStatus);
            return result.MemoryUsageStatus;
        }

        /// <summary>Requestes the current processor utilization from the amp</summary>
        public void GetProcessorUtilization()
        {
            SendMessage(MessageFactory.Create(new ProcessorUtilizationRequest() { Request = true }));
        }

        /// <summary>Asynchronously requestes the current processor utilization from the amp</summary>
        /// <returns>A message with the current processor utilization</returns>
        public async Task<ProcessorUtilization> GetProcessorUtilizationAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ProcessorUtilizationRequest() { Request = true }), FenderMessageLT.TypeOneofCase.MemoryUsageStatus);
            return result.ProcessorUtilization;
        }

        /// <summary>Requests the product information from the amp</summary>
        public void GetProductIdentification()
        {
            SendMessage(MessageFactory.Create(new ProductIdentificationRequest() { Request = true }));
        }

        /// <summary>Asynchronously requests the product information from the amp</summary>
        /// <returns>A message with the prodcut information</returns>
        public async Task<ProductIdentificationStatus> GetProductIdentificationAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new ProductIdentificationRequest() { Request = true }), FenderMessageLT.TypeOneofCase.ProductIdentificationStatus);
            return result.ProductIdentificationStatus;
        }

        /// <summary>Sends a heartbeat message to keep the connection with the amp alive</summary>
        public void Heartbeat()
        {
            SendMessage(MessageFactory.Create(new Heartbeat() { DummyField = true }));
        }

        /// <summary>Requests the current footswitch settings from the amp</summary>
        public void GetQASlots()
        {
            SendMessage(MessageFactory.Create(new QASlotsRequest() { Request = true }));
        }

        /// <summary>Asynchronously requests the current footswitch settings from the amp</summary>
        /// <returns>A message containing the current footswitch settings</returns>
        public async Task<QASlotsStatus> GetQASlotsAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new QASlotsRequest() { Request = true }), FenderMessageLT.TypeOneofCase.QASlotsStatus);
            return result.QASlotsStatus;
        }

        /// <summary>Sets the footswitch settings</summary>
        public void SetQASlots(uint[] slotIndexes)
        {
            FenderMessageLT message = MessageFactory.Create(new QASlotsSet());
            message.QASlotsSet.Slots.Add(slotIndexes);
            SendMessage(message);
        }

        /// <summary>Asynchronously sets the footswitch settings</summary>
        /// <returns>A message containing the new footswitch settings</returns>
        public async Task<QASlotsStatus> SetQASlotsAsync(uint[] slotIndexes)
        {
            FenderMessageLT message = MessageFactory.Create(new QASlotsSet());
            message.QASlotsSet.Slots.Add(slotIndexes);
            FenderMessageLT returnMessage = await SendMessageAsync(message, FenderMessageLT.TypeOneofCase.QASlotsStatus);
            return returnMessage.QASlotsStatus;
        }

        /// <summary>Requests the USB gain on the amp</summary>
        public void GetUsbGain()
        {
            SendMessage(MessageFactory.Create(new UsbGainRequest() { Request = true }));
        }

        /// <summary>Asynchronously gets the USB gain on the amp</summary>
        /// <returns>A message with the current gain setting</returns>
        public async Task<UsbGainStatus> GetUsbGainAsync()
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new UsbGainRequest() { Request = true }), FenderMessageLT.TypeOneofCase.UsbGainStatus);
            return result.UsbGainStatus;
        }

        /// <summary>Sets the USB gain on the amp</summary>
        /// <param name="value">Gain (in dB)</param>
        public void SetUsbGain(float value)
        {
            SendMessage(MessageFactory.Create(new UsbGainSet() { ValueDB = value }));
        }

        /// <summary>Asynchronously sets the USB gain on the amp</summary>
        /// <param name="value">Gain (in dB)</param>
        /// <returns>A message confirming the operation</returns>
        public async Task<UsbGainStatus> SetUsbGainAsync(float value)
        {
            FenderMessageLT result = await SendMessageAsync(MessageFactory.Create(new UsbGainSet() { ValueDB = value }), FenderMessageLT.TypeOneofCase.UsbGainStatus);
            return result.UsbGainStatus;
        }

        #endregion Device Info

        #region pre-formed commands

        /// <summary>Retrieves all presets from the amp</summary>
        public void GetAllPresets()
        {
            for (int i = 1; i <= NUM_OF_PRESETS; i++)
            {
                GetPreset(i);
                Thread.Sleep(100);
            }
        }

        /// <summary>Asynchronously retrieves all presets from the amp</summary>
        /// <returns>A list of preset messages</returns>
        public async Task<List<PresetJSONMessage>> GetAllPresetsAsync()
        {
            List<PresetJSONMessage> presetMessages = [];
            for (int i = 1; i <= NUM_OF_PRESETS; i++)
            {
                presetMessages.Add(await GetPresetAsync(i));
            }
            return presetMessages;
        }

        /// <summary>Sets the tuner on or off</summary>
        /// <param name="on_off">True for on; false for off</param>
        public void SetTuner(bool on_off)
        {
            SetModalState(on_off ? ModalContext.TunerEnable : ModalContext.TunerDisable);
        }

        /// <summary>Asynchronously sets the tuner on or off</summary>
        /// <param name="on_off">True for on; false for off</param>
        /// <returns>A message confirming the operation</returns>
        public async Task<ModalStatusMessage> SetTunerAsync(bool on_off)
        {
            return await SetModalStateAsync(on_off ? ModalContext.TunerEnable : ModalContext.TunerDisable);
        }

        #endregion pre-formed commands

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

        #endregion Unknown messages
    }
}