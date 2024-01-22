# Protocol Documentation
<a name="top"></a>

<a name="FenderMessageLT-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## FenderMessageLT.proto

<a name="-FenderMessageLT"></a>

### FenderMessageLT
Base message type used to communicate with the amp

All messages are of this type, and encapsulate the actual message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| responseType | [ResponseType](#ResponseType) | required | Response type. All messages from the host to the amp are UNSOLICITED Default: UNSOLICITED |
| indexPot | [IndexPot](#IndexPot) | optional |  |
| indexButton | [IndexButton](#IndexButton) | optional |  |
| indexEncoder | [IndexEncoder](#IndexEncoder) | optional |  |
| activeDisplay | [ActiveDisplay](#ActiveDisplay) | optional |  |
| processorUtilizationRequest | [ProcessorUtilizationRequest](#ProcessorUtilizationRequest) | optional |  |
| processorUtilization | [ProcessorUtilization](#ProcessorUtilization) | optional |  |
| memoryUsageRequest | [MemoryUsageRequest](#MemoryUsageRequest) | optional |  |
| memoryUsageStatus | [MemoryUsageStatus](#MemoryUsageStatus) | optional |  |
| presetJSONMessageRequest_LT | [PresetJSONMessageRequest_LT](#PresetJSONMessageRequest_LT) | optional |  |
| frameBufferMessageRequest | [FrameBufferMessageRequest](#FrameBufferMessageRequest) | optional |  |
| frameBufferMessage | [FrameBufferMessage](#FrameBufferMessage) | optional |  |
| lt4FootswitchModeRequest | [LT4FootswitchModeRequest](#LT4FootswitchModeRequest) | optional |  |
| lt4FootswitchModeStatus | [LT4FootswitchModeStatus](#LT4FootswitchModeStatus) | optional |  |
| loadPreset_TestSuite | [LoadPreset_TestSuite](#LoadPreset_TestSuite) | optional |  |
| loopbackTest | [LoopbackTest](#LoopbackTest) | optional |  |
| presetJSONMessage | [PresetJSONMessage](#PresetJSONMessage) | optional |  |
| currentPresetStatus | [CurrentPresetStatus](#CurrentPresetStatus) | optional |  |
| loadPreset | [LoadPreset](#LoadPreset) | optional |  |
| setDspUnitParameter | [SetDspUnitParameter](#SetDspUnitParameter) | optional |  |
| setDspUnitParameterStatus | [SetDspUnitParameterStatus](#SetDspUnitParameterStatus) | optional |  |
| dspUnitParameterStatus | [DspUnitParameterStatus](#DspUnitParameterStatus) | optional |  |
| currentLoadedPresetIndexStatus | [CurrentLoadedPresetIndexStatus](#CurrentLoadedPresetIndexStatus) | optional |  |
| presetEditedStatus | [PresetEditedStatus](#PresetEditedStatus) | optional |  |
| replaceNode | [ReplaceNode](#ReplaceNode) | optional |  |
| replaceNodeStatus | [ReplaceNodeStatus](#ReplaceNodeStatus) | optional |  |
| shiftPreset | [ShiftPreset](#ShiftPreset) | optional |  |
| shiftPresetStatus | [ShiftPresetStatus](#ShiftPresetStatus) | optional |  |
| swapPreset | [SwapPreset](#SwapPreset) | optional |  |
| swapPresetStatus | [SwapPresetStatus](#SwapPresetStatus) | optional |  |
| currentPresetSet | [CurrentPresetSet](#CurrentPresetSet) | optional |  |
| currentLoadedPresetIndexBypassStatus | [CurrentLoadedPresetIndexBypassStatus](#CurrentLoadedPresetIndexBypassStatus) | optional |  |
| currentDisplayedPresetIndexStatus | [CurrentDisplayedPresetIndexStatus](#CurrentDisplayedPresetIndexStatus) | optional |  |
| presetSavedStatus | [PresetSavedStatus](#PresetSavedStatus) | optional |  |
| clearPreset | [ClearPreset](#ClearPreset) | optional |  |
| clearPresetStatus | [ClearPresetStatus](#ClearPresetStatus) | optional |  |
| saveCurrentPreset | [SaveCurrentPreset](#SaveCurrentPreset) | optional |  |
| saveCurrentPresetTo | [SaveCurrentPresetTo](#SaveCurrentPresetTo) | optional |  |
| savePresetAs | [SavePresetAs](#SavePresetAs) | optional |  |
| newPresetSavedStatus | [NewPresetSavedStatus](#NewPresetSavedStatus) | optional |  |
| renamePresetAt | [RenamePresetAt](#RenamePresetAt) | optional |  |
| auditionPreset | [AuditionPreset](#AuditionPreset) | optional |  |
| auditionPresetStatus | [AuditionPresetStatus](#AuditionPresetStatus) | optional |  |
| exitAuditionPreset | [ExitAuditionPreset](#ExitAuditionPreset) | optional |  |
| exitAuditionPresetStatus | [ExitAuditionPresetStatus](#ExitAuditionPresetStatus) | optional |  |
| auditionStateRequest | [AuditionStateRequest](#AuditionStateRequest) | optional |  |
| auditionStateStatus | [AuditionStateStatus](#AuditionStateStatus) | optional |  |
| productIdentificationStatus | [ProductIdentificationStatus](#ProductIdentificationStatus) | optional |  |
| productIdentificationRequest | [ProductIdentificationRequest](#ProductIdentificationRequest) | optional |  |
| firmwareVersionRequest | [FirmwareVersionRequest](#FirmwareVersionRequest) | optional |  |
| firmwareVersionStatus | [FirmwareVersionStatus](#FirmwareVersionStatus) | optional |  |
| currentPresetRequest | [CurrentPresetRequest](#CurrentPresetRequest) | optional |  |
| retrievePreset | [RetrievePreset](#RetrievePreset) | optional |  |
| usbGainRequest | [UsbGainRequest](#UsbGainRequest) | optional |  |
| usbGainStatus | [UsbGainStatus](#UsbGainStatus) | optional |  |
| qASlotsRequest | [QASlotsRequest](#QASlotsRequest) | optional |  |
| qASlotsStatus | [QASlotsStatus](#QASlotsStatus) | optional |  |
| lineOutGainRequest | [LineOutGainRequest](#LineOutGainRequest) | optional |  |
| lineOutGainStatus | [LineOutGainStatus](#LineOutGainStatus) | optional |  |
| modalStatusMessage | [ModalStatusMessage](#ModalStatusMessage) | optional |  |
| usbGainSet | [UsbGainSet](#UsbGainSet) | optional |  |
| lineOutGainSet | [LineOutGainSet](#LineOutGainSet) | optional |  |
| qASlotsSet | [QASlotsSet](#QASlotsSet) | optional |  |
| unsupportedMessageStatus | [UnsupportedMessageStatus](#UnsupportedMessageStatus) | optional |  |
| heartbeat | [Heartbeat](#Heartbeat) | optional |  |
| connectionStatusRequest | [ConnectionStatusRequest](#ConnectionStatusRequest) | optional |  |
| connectionStatus | [ConnectionStatus](#ConnectionStatus) | optional |  |

<a name="-ResponseType"></a>

### ResponseType
Message response type

| Name | Number | Description |
| ---- | ------ | ----------- |
| UNSOLICITED | 0 | Message sent not as the result of a command |
| NOT_LAST_ACK | 1 | Message sent as the result of a command, but NOT the last message in the batch |
| IS_LAST_ACK | 2 | Message sent as the last result of a command |

<a name="AudioStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## AudioStatus.proto

<a name="-PresetEditedStatus"></a>

### PresetEditedStatus
Editing status of the current preset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetEdited | [bool](#bool) | required | True if the preset is in editing state Default: false |

<a name="AuditionPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## AuditionPreset.proto

<a name="-AuditionPreset"></a>

### AuditionPreset
Sends a preset to the amp to be &#34;auditioned&#34;

response: [AuditionPresetStatus](#auditionpresetstatus) message containing the preset data loaded to the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required | [JSON](json.md) string conaining the preset to be auditioned |

<a name="AuditionPresetStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## AuditionPresetStatus.proto

<a name="-AuditionPresetStatus"></a>

### AuditionPresetStatus
Response to an [AuditionPreset](#auditionpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required | [JSON data](json.md) conatining preset data being auditioned |

<a name="AuditionStateRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## AuditionStateRequest.proto

<a name="-AuditionStateRequest"></a>

### AuditionStateRequest
Queries the amp for its audition state

response: [AuditionStateStatus](#auditionstatestatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="AuditionStateStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## AuditionStateStatus.proto

<a name="-AuditionStateStatus"></a>

### AuditionStateStatus
The current audition state of the amp. Resposne to an [AuditionStateRequest](#auditionstaterequest) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| isAuditioning | [bool](#bool) | required | True if the amp is in audition mode |

<a name="ClearPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ClearPreset.proto

<a name="-ClearPreset"></a>

### ClearPreset
Clears a preset in the amp

response: [ClearPresetStatus](#clearpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slotIndex | [int32](#int32) | required | Preset bank to clear |
| isLoadPreset | [bool](#bool) | required | ??? the Tone app sets this to true when clearing the preset |

<a name="ClearPresetStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ClearPresetStatus.proto

<a name="-ClearPresetStatus"></a>

### ClearPresetStatus
Response to a [ClearPreset](#clearpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slotIndex | [int32](#int32) | required | bank number cleared |

<a name="ConnectionStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ConnectionStatus.proto

<a name="-ConnectionStatus"></a>

### ConnectionStatus
The current connection state of the amp. Response to a [ConnectionStatusRequest](#connectionstatusrequest) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| isConnected | [bool](#bool) | required | True if the amp is connected |

<a name="ConnectionStatusRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ConnectionStatusRequest.proto

<a name="-ConnectionStatusRequest"></a>

### ConnectionStatusRequest
Queries the connection status of the amp

response: [ConnectionStatus](#connectionstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="CurrentDisplayedPresetIndexStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## CurrentDisplayedPresetIndexStatus.proto

<a name="-CurrentDisplayedPresetIndexStatus"></a>

### CurrentDisplayedPresetIndexStatus
The current preset displayed on the amp. Sent when the preset is changed at the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentDisplayedPresetIndex | [int32](#int32) | required | The current preset bank number |

<a name="CurrentLoadedPresetIndexStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## CurrentLoadedPresetIndexStatus.proto

<a name="-CurrentLoadedPresetIndexStatus"></a>

### CurrentLoadedPresetIndexStatus
The current preset displayed on the amp. Sent when the preset is changed at the amp or via a [LoadPreset](#loadpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentLoadedPresetIndex | [int32](#int32) | required | The current preset bank number |

<a name="CurrentPresetRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## CurrentPresetRequest.proto

<a name="-CurrentPresetRequest"></a>

### CurrentPresetRequest
Requests the current preset from the amp

response: [CurrentPresetStatus](#currentpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="CurrentPresetSet-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## CurrentPresetSet.proto

<a name="-CurrentPresetSet"></a>

### CurrentPresetSet
Sets the current preset of the amp

response: [CurrentPresetStatus](#currentpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentPresetData | [string](#string) | required | [JSON data](json.md) conatining preset data |

<a name="CurrentPresetStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## CurrentPresetStatus.proto

<a name="-CurrentPresetStatus"></a>

### CurrentPresetStatus
Returns the state of the current preset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentPresetData | [string](#string) | required | [JSON data](json.md) conatining current preset data |
| currentSlotIndex | [int32](#int32) | required | Current preset bank number |
| currentPresetDirtyStatus | [bool](#bool) | required | True if current preset has been edited and not saved |

<a name="ExitAuditionPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ExitAuditionPreset.proto

<a name="-ExitAuditionPreset"></a>

### ExitAuditionPreset
Leaves audition mode

response: [ExitAuditionPresetStatus](#exitauditionpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| exit | [bool](#bool) | required | True to exit audition mode |

<a name="ExitAuditionPresetStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ExitAuditionPresetStatus.proto

<a name="-ExitAuditionPresetStatus"></a>

### ExitAuditionPresetStatus
Result message for an [ExitAuditionPreset](#exitauditionpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| isSuccess | [bool](#bool) | required | True if audition mode was exited successfully |

<a name="FirmwareVersionRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## FirmwareVersionRequest.proto

<a name="-FirmwareVersionRequest"></a>

### FirmwareVersionRequest
Requests the current filrware version

response: [FirmwareVersionStatus](#firmwareversionstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="FirmwareVersionStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## FirmwareVersionStatus.proto

<a name="-FirmwareVersionStatus"></a>

### FirmwareVersionStatus
The firmware version of the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| version | [string](#string) | required | Firmware Version |

<a name="Heartbeat-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## Heartbeat.proto

<a name="-Heartbeat"></a>

### Heartbeat
Heartbeat message sent every second to keep the connection alive

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| dummyField | [bool](#bool) | required |  |


<a name="LoadPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LoadPreset.proto

<a name="-LoadPreset"></a>

### LoadPreset
Switches the amp to the specified preset number

response: [CurrentLoadedPresetIndexStatus](#currentloadedpresetindexstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetIndex | [int32](#int32) | required | Preset bank number currently loaded |

<a name="MemoryUsageRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## MemoryUsageRequest.proto

<a name="-MemoryUsageRequest"></a>

### MemoryUsageRequest
Requests memory usage statistics from the amp

response: [MemoryUsageStatus](#memoryusagestatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="MemoryUsageStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## MemoryUsageStatus.proto

<a name="-MemoryUsageStatus"></a>

### MemoryUsageStatus
Message conainting current memory usage statistics

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| stack | [int32](#int32) | required |  |
| heap | [int32](#int32) | required |  |

<a name="ModalStatusMessage-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ModalStatusMessage.proto

<a name="-ModalStatusMessage"></a>

### ModalStatusMessage
Changes the state of the amp

response: A [ModalStatusMessage](#modalstatusmessage) with the result of the request

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| context | [ModalContext](#ModalContext) | required | Context to switch to |
| state | [ModalState](#ModalState) | required | The result of the request. Requests to the amp are sent with an OK state |

<a name="-ModalContext"></a>

### ModalContext

| Name | Number | Description |
| ---- | ------ | ----------- |
| SYNC_BEGIN | 0 | Used during the initialization of the connection |
| SYNC_END | 1 | Used during the initialization of the connection |
| BACKUP_BEGIN | 2 |  |
| BACKUP_END | 3 |  |
| RESTORE_BEGIN | 4 |  |
| RESTORE_END | 5 |  |
| TUNER_ENABLE | 6 | Enables the tuner |
| TUNER_DISABLE | 7 | Disables the tuner |
| FACTORY_RESTORE_BEGIN | 8 |  |
| FACTORY_RESTORE_END | 9 |  |
| TONE_BUSY_BEGIN | 10 |  |
| TONE_BUSY_END | 11 |  |

<a name="-ModalState"></a>

### ModalState

| Name | Number | Description |
| ---- | ------ | ----------- |
| OK | 0 | Success; also the default when sending this message to the amp |
| FAIL | 1 | Failure |

<a name="NewPresetSavedStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## NewPresetSavedStatus.proto

<a name="-NewPresetSavedStatus"></a>

### NewPresetSavedStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required |  |
| presetSlot | [int32](#int32) | required |  |

<a name="PresetJSONMessage-proto"></a>
<p align="right"><a href="#top">Top</a></p>


## PresetJSONMessage.proto

<a name="-PresetJSONMessage"></a>

### PresetJSONMessage
Saved preset data

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data | [string](#string) | required | [JSON data](json.md) conatining preset data for requested preset bank |
| slotIndex | [int32](#int32) | required | Preset bank this preset is stored in |


<a name="PresetSavedStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## PresetSavedStatus.proto

<a name="-PresetSavedStatus"></a>

### PresetSavedStatus
Result of a SaveCurrentPreset, SaveCurrentPresetTo, or SavePresetAs message. Also sent when the preset is saved on the amp.

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) | required | Name of the preset saved |
| slot | [int32](#int32) | required | Preset bank of the saved preset |

<a name="ProcessorUtilization-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ProcessorUtilization.proto

<a name="-ProcessorUtilization"></a>

### ProcessorUtilization
Current processor utilization statistics

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| percent | [float](#float) | required |  |
| minPercent | [float](#float) | required |  |
| maxPercent | [float](#float) | required |  |

<a name="ProcessorUtilizationRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ProcessorUtilizationRequest.proto

<a name="-ProcessorUtilizationRequest"></a>

### ProcessorUtilizationRequest
Requests processor statistics from the amp

response: [ProcessorUtilization](#processorutilization) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="ProductIdentificationRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ProductIdentificationRequest.proto

<a name="-ProductIdentificationRequest"></a>

### ProductIdentificationRequest

Identified the amplifier

response: [ProductIdentificationStatus](#productidentificationstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="ProductIdentificationStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ProductIdentificationStatus.proto

<a name="-ProductIdentificationStatus"></a>

### ProductIdentificationStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| id | [string](#string) | required |  |

<a name="QASlotsRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## QASlotsRequest.proto

<a name="-QASlotsRequest"></a>

### QASlotsRequest
Requests the settings of the footswitch presets

response: [QASlotsStatus](#qaslotsstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="QASlotsSet-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## QASlotsSet.proto

<a name="-QASlotsSet"></a>

### QASlotsSet
Assigns preset banks to the footswitch

response: [QASlotsStatus](#qaslotsstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slots | [uint32](#uint32) | repeated | Preset banks to assign to the footswitch |

<a name="QASlotsStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## QASlotsStatus.proto

<a name="-QASlotsStatus"></a>

### QASlotsStatus
The current settings of the footswitch presets

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slots | [uint32](#uint32) | repeated | The preset banks assigned to the footswitch |

<a name="RenamePresetAt-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## RenamePresetAt.proto

<a name="-RenamePresetAt"></a>

### RenamePresetAt

Renames a preset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetName | [string](#string) | required |  new preset name |
| presetSlot | [int32](#int32) | required |  slot index to rename |

<a name="ReplaceNode-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ReplaceNode.proto

<a name="-ReplaceNode"></a>

### ReplaceNode
Swaps out a node (amp, stomp, mod, delay, or reverb)

response: [ReplaceNodeStatus](#replacenodestatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeIdToReplace | [string](#string) | required | nodeid to replace (amp, stomp, mod, delay, or reverb) |
| fenderIdToReplaceWith | [string](#string) | required | FenderId of node to set |

<a name="ReplaceNodeStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ReplaceNodeStatus.proto

<a name="-ReplaceNodeStatus"></a>

### ReplaceNodeStatus
Status of a [ReplaceNode](#replacenode) message; also sent when the node is changed at the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeIdReplaced | [string](#string) | required | node replaced (amp, stomp, mod, delay, or reverb) |
| fenderIdReplaced | [string](#string) | required | FenderId of node set |

<a name="RetrievePreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## RetrievePreset.proto

<a name="-RetrievePreset"></a>

### RetrievePreset
Queries the amp on the stored presets

response: [PresetJsonMessage](#presetjsonmessage)

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slot | [int32](#int32) | required | Preset bank to retrieve |

<a name="SaveCurrentPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SaveCurrentPreset.proto

<a name="-SaveCurrentPreset"></a>

### SaveCurrentPreset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| save | [bool](#bool) | required |  |

<a name="SaveCurrentPresetTo-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SaveCurrentPresetTo.proto

<a name="-SaveCurrentPresetTo"></a>

### SaveCurrentPresetTo

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetName | [string](#string) | required |  |
| presetSlot | [int32](#int32) | required |  |

<a name="SavePresetAs-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SavePresetAs.proto

<a name="-SavePresetAs"></a>

### SavePresetAs

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required |  |
| isLoadPreset | [bool](#bool) | required |  |
| presetSlot | [int32](#int32) | required |  |

<a name="SetDspUnitParameter-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SetDspUnitParameter.proto

<a name="-SetDspUnitParameter"></a>

### SetDspUnitParameter
Changes the parameters of the DSP units

response: [SetDspUnitParameterStatus](#setdspunitparameterstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeId | [string](#string) | required | string id of the node to change (one of amp, stomp, mod, delay, reverb) |
| parameterId | [string](#string) | required | string id of the parameter to change |
| floatParameter | [float](#float) | optional |  |
| stringParameter | [string](#string) | optional |  |
| sint32Parameter | [sint32](#sint32) | optional |  |
| boolParameter | [bool](#bool) | optional |  |

<a name="SetDspUnitParameterStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SetDspUnitParameterStatus.proto

<a name="-SetDspUnitParameterStatus"></a>

### SetDspUnitParameterStatus
Status of the [SetDspUnitParameter](#setdspunitparameter) message. Also sent when parameters are adjusted on the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeId | [string](#string) | required | string id of the node changed (one of amp, stomp, mod, delay, reverb) |
| parameterId | [string](#string) | required | string id of the parameter to change |
| floatParameter | [float](#float) | optional |  |
| stringParameter | [string](#string) | optional |  |
| sint32Parameter | [sint32](#sint32) | optional |  |
| boolParameter | [bool](#bool) | optional |  |

<a name="ShiftPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ShiftPreset.proto

<a name="-ShiftPreset"></a>

### ShiftPreset
Moves a preset to the specified point, shifting all other presets

response: [ShiftPresetStatus](#shiftpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexToShiftFrom | [int32](#int32) | required | Preset to shift |
| indexToShiftTo | [int32](#int32) | required | Destination of preset to shift |

<a name="ShiftPresetStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ShiftPresetStatus.proto

<a name="-ShiftPresetStatus"></a>

### ShiftPresetStatus
Response to a [ShiftPreset](#shiftpreset) message with the status of the command

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexToShiftFrom | [int32](#int32) | required | Preset shifted |
| indexToShiftTo | [int32](#int32) | required | Destination |

<a name="SwapPreset-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SwapPreset.proto

<a name="-SwapPreset"></a>

### SwapPreset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexA | [int32](#int32) | required |  |
| indexB | [int32](#int32) | required |  |

<a name="SwapPresetStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## SwapPresetStatus.proto

<a name="-SwapPresetStatus"></a>

### SwapPresetStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexA | [int32](#int32) | required |  |
| indexB | [int32](#int32) | required |  |

<a name="UnsupportedMessageStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## UnsupportedMessageStatus.proto

<a name="-UnsupportedMessageStatus"></a>

### UnsupportedMessageStatus

Sent when a message is invalid

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| status | [ErrorType](#ErrorType) | required |  |

<a name="-ErrorType"></a>

### ErrorType

| Name | Number | Description |
| ---- | ------ | ----------- |
| UNSUPPORTED | 0 |  |
| FAILED | 1 |  |
| INVALID_PARAM | 2 |  |
| INVALID_NODE_ID | 3 |  |
| PARAM_OUT_OF_BOUNDS | 4 |  |
| FACTORY_RESTORE_IN_PROGRESS | 5 |  |

<a name="UsbGainRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## UsbGainRequest.proto

<a name="-UsbGainRequest"></a>

### UsbGainRequest
Gets the current USB Gain settings

response: [UsbGainStatus](#usbgainstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required |  |

<a name="UsbGainSet-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## UsbGainSet.proto

<a name="-UsbGainSet"></a>

### UsbGainSet
Sets the gain for the USB audio device

response: [UsbGainStatus](#usbgainstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required | gain in dB |

<a name="UsbGainStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## UsbGainStatus.proto

<a name="-UsbGainStatus"></a>

### UsbGainStatus
The current setting of the gain for the USB audio device

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required | gain in dB |

## Scalar Value Types

| .proto Type | Notes | C++ | Java | Python | Go | C# | PHP | Ruby |
| ----------- | ----- | --- | ---- | ------ | -- | -- | --- | ---- |
| <a name="double" /> double |  | double | double | float | float64 | double | float | Float |
| <a name="float" /> float |  | float | float | float | float32 | float | float | Float |
| <a name="int32" /> int32 | Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint32 instead. | int32 | int | int | int32 | int | integer | Bignum or Fixnum (as required) |
| <a name="int64" /> int64 | Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint64 instead. | int64 | long | int/long | int64 | long | integer/string | Bignum |
| <a name="uint32" /> uint32 | Uses variable-length encoding. | uint32 | int | int/long | uint32 | uint | integer | Bignum or Fixnum (as required) |
| <a name="uint64" /> uint64 | Uses variable-length encoding. | uint64 | long | int/long | uint64 | ulong | integer/string | Bignum or Fixnum (as required) |
| <a name="sint32" /> sint32 | Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int32s. | int32 | int | int | int32 | int | integer | Bignum or Fixnum (as required) |
| <a name="sint64" /> sint64 | Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int64s. | int64 | long | int/long | int64 | long | integer/string | Bignum |
| <a name="fixed32" /> fixed32 | Always four bytes. More efficient than uint32 if values are often greater than 2^28. | uint32 | int | int | uint32 | uint | integer | Bignum or Fixnum (as required) |
| <a name="fixed64" /> fixed64 | Always eight bytes. More efficient than uint64 if values are often greater than 2^56. | uint64 | long | int/long | uint64 | ulong | integer/string | Bignum |
| <a name="sfixed32" /> sfixed32 | Always four bytes. | int32 | int | int | int32 | int | integer | Bignum or Fixnum (as required) |
| <a name="sfixed64" /> sfixed64 | Always eight bytes. | int64 | long | int/long | int64 | long | integer/string | Bignum |
| <a name="bool" /> bool |  | bool | boolean | boolean | bool | bool | boolean | TrueClass/FalseClass |
| <a name="string" /> string | A string must always contain UTF-8 encoded or 7-bit ASCII text. | string | String | str/unicode | string | string | string | String (UTF-8) |
| <a name="bytes" /> bytes | May contain any arbitrary sequence of bytes. | string | ByteString | str | []byte | ByteString | string | String (ASCII-8BIT) |

<a name="unknown-messages">

## Unknown messages

<a name="ActiveDisplay-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## ActiveDisplay.proto

<a name="-ActiveDisplay"></a>

### ActiveDisplay

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| pageName | [string](#string) | required |  |

<a name="CurrentLoadedPresetIndexBypassStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## CurrentLoadedPresetIndexBypassStatus.proto

<a name="-CurrentLoadedPresetIndexBypassStatus"></a>

### CurrentLoadedPresetIndexBypassStatus
The bypass status of the DSP units.

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentLoadedPresetIndex | [int32](#int32) | required |  |
| bypassStatus | [bool](#bool) | repeated | Indicates whether the DSP unit is bypassed or not |

<a name="DspUnitParameterStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## DspUnitParameterStatus.proto

<a name="-DspUnitParameterStatus"></a>

### DspUnitParameterStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeId | [string](#string) | required |  |
| parameterId | [string](#string) | required |  |
| floatParameter | [float](#float) | optional |  |
| stringParameter | [string](#string) | optional |  |
| sint32Parameter | [sint32](#sint32) | optional |  |
| boolParameter | [bool](#bool) | optional |  |

<a name="FrameBufferMessage-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## FrameBufferMessage.proto

<a name="-FrameBufferMessage"></a>

### FrameBufferMessage

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data | [bytes](#bytes) | required |  |

<a name="FrameBufferMessageRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## FrameBufferMessageRequest.proto

<a name="-FrameBufferMessageRequest"></a>

### FrameBufferMessageRequest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="IndexButton-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## IndexButton.proto

<a name="-IndexButton"></a>

### IndexButton

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| index | [int32](#int32) | required |  |
| event | [IndexButton.Event](#IndexButton-Event) | required |  |
| timestamp_ms | [uint32](#uint32) | optional |  |

<a name="-IndexButton-Event"></a>

### IndexButton.Event

| Name | Number | Description |
| ---- | ------ | ----------- |
| EVENT_ERROR | 0 |  |
| BUTTON_DOWN | 1 |  |
| BUTTON_UP | 2 |  |
| BUTTON_HELD | 3 |  |

<a name="IndexEncoder-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## IndexEncoder.proto

<a name="-IndexEncoder"></a>

### IndexEncoder

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| index | [int32](#int32) | required |  |
| ticks | [int32](#int32) | required |  |

<a name="IndexPot-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## IndexPot.proto

<a name="-IndexPot"></a>

### IndexPot

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| index | [int32](#int32) | required |  |
| position | [float](#float) | required |  |

<a name="LT4FootswitchModeRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LT4FootswitchModeRequest.proto

<a name="-LT4FootswitchModeRequest"></a>

### LT4FootswitchModeRequest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="LT4FootswitchModeStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LT4FootswitchModeStatus.proto

<a name="-LT4FootswitchModeStatus"></a>

### LT4FootswitchModeStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentMode | [LT4FtswModes](#LT4FtswModes) | required |  Default: LT4_FTSW_MODE_OFF |

<a name="-LT4FtswModes"></a>

### LT4FtswModes

| Name | Number | Description |
| ---- | ------ | ----------- |
| LT4_FTSW_MODE_OFF | 0 |  |
| LT4_FTSW_MODE_BANK1 | 1 |  |
| LT4_FTSW_MODE_BANK2 | 2 |  |
| LT4_FTSW_MODE_FX | 3 |  |

<a name="LineOutGainRequest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LineOutGainRequest.proto

<a name="-LineOutGainRequest"></a>

### LineOutGainRequest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

<a name="LineOutGainSet-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LineOutGainSet.proto

<a name="-LineOutGainSet"></a>

### LineOutGainSet

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required |  |

<a name="LineOutGainStatus-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LineOutGainStatus.proto

<a name="-LineOutGainStatus"></a>

### LineOutGainStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required |  |

<a name="LoadPreset_TestSuite-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LoadPreset_TestSuite.proto

<a name="-LoadPreset_TestSuite"></a>

### LoadPreset_TestSuite

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetIndex | [int32](#int32) | required |  |

<a name="LoopbackTest-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## LoopbackTest.proto

<a name="-LoopbackTest"></a>

### LoopbackTest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data | [string](#string) | required |  |

<a name="PresetJSONMessageRequest_LT-proto"></a>
<p align="right"><a href="#top">Top</a></p>

## PresetJSONMessageRequest_LT.proto

<a name="-PresetJSONMessageRequest_LT"></a>

### PresetJSONMessageRequest_LT

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [int32](#int32) | required |  |
