# Protocol Documentation

The arrows indicate the direction the message goes. 
- ← indicates the message is sent from the amp to the computer
- → indicates the message is sent from the computer to the amp

### FenderMessageLT
(← →)
Base message type used to communicate with the amp

All messages are of this type, and encapsulate the actual message

| Field | Type | Label | Description | Direction |
| ----- | ---- | ----- | ----------- | --------- |
| responseType | [ResponseType](#ResponseType) | required | Response type. All messages from the host to the amp are UNSOLICITED Default: UNSOLICITED | ← → |
| processorUtilizationRequest | [ProcessorUtilizationRequest](#ProcessorUtilizationRequest) | optional |  | → |
| processorUtilization | [ProcessorUtilization](#ProcessorUtilization) | optional |  |
| memoryUsageRequest | [MemoryUsageRequest](#MemoryUsageRequest) | optional |  | → |
| memoryUsageStatus | [MemoryUsageStatus](#MemoryUsageStatus) | optional |  | → |
| presetJSONMessageRequest_LT | [PresetJSONMessageRequest_LT](#PresetJSONMessageRequest_LT) | optional |  |
| presetJSONMessage | [PresetJSONMessage](#PresetJSONMessage) | optional |  |
| currentPresetStatus | [CurrentPresetStatus](#CurrentPresetStatus) | optional |  |
| loadPreset | [LoadPreset](#LoadPreset) | optional |  | → |
| setDspUnitParameter | [SetDspUnitParameter](#SetDspUnitParameter) | optional |  | → |
| setDspUnitParameterStatus | [SetDspUnitParameterStatus](#SetDspUnitParameterStatus) | optional |  |
| dspUnitParameterStatus | [DspUnitParameterStatus](#DspUnitParameterStatus) | optional |  |
| currentLoadedPresetIndexStatus | [CurrentLoadedPresetIndexStatus](#CurrentLoadedPresetIndexStatus) | optional |  |
| presetEditedStatus | [PresetEditedStatus](#PresetEditedStatus) | optional |  |
| replaceNode | [ReplaceNode](#ReplaceNode) | optional |  | → |
| replaceNodeStatus | [ReplaceNodeStatus](#ReplaceNodeStatus) | optional |  |
| shiftPreset | [ShiftPreset](#ShiftPreset) | optional |  | → |
| shiftPresetStatus | [ShiftPresetStatus](#ShiftPresetStatus) | optional |  |
| swapPreset | [SwapPreset](#SwapPreset) | optional |  | → |
| swapPresetStatus | [SwapPresetStatus](#SwapPresetStatus) | optional |  |
| currentPresetSet | [CurrentPresetSet](#CurrentPresetSet) | optional |  |
| currentLoadedPresetIndexBypassStatus | [CurrentLoadedPresetIndexBypassStatus](#CurrentLoadedPresetIndexBypassStatus) | optional |  |
| currentDisplayedPresetIndexStatus | [CurrentDisplayedPresetIndexStatus](#CurrentDisplayedPresetIndexStatus) | optional |  |
| presetSavedStatus | [PresetSavedStatus](#PresetSavedStatus) | optional |  |
| clearPreset | [ClearPreset](#ClearPreset) | optional |  | → |
| clearPresetStatus | [ClearPresetStatus](#ClearPresetStatus) | optional |  |
| saveCurrentPreset | [SaveCurrentPreset](#SaveCurrentPreset) | optional |  | → |
| saveCurrentPresetTo | [SaveCurrentPresetTo](#SaveCurrentPresetTo) | optional |  | → |
| savePresetAs | [SavePresetAs](#SavePresetAs) | optional |  | → |
| newPresetSavedStatus | [NewPresetSavedStatus](#NewPresetSavedStatus) | optional |  |
| renamePresetAt | [RenamePresetAt](#RenamePresetAt) | optional |  | → |
| auditionPreset | [AuditionPreset](#AuditionPreset) | optional |  | → |
| auditionPresetStatus | [AuditionPresetStatus](#AuditionPresetStatus) | optional |  |
| exitAuditionPreset | [ExitAuditionPreset](#ExitAuditionPreset) | optional |  | → |
| exitAuditionPresetStatus | [ExitAuditionPresetStatus](#ExitAuditionPresetStatus) | optional |  |
| auditionStateRequest | [AuditionStateRequest](#AuditionStateRequest) | optional |  | → |
| auditionStateStatus | [AuditionStateStatus](#AuditionStateStatus) | optional |  |
| productIdentificationStatus | [ProductIdentificationStatus](#ProductIdentificationStatus) | optional |  |
| productIdentificationRequest | [ProductIdentificationRequest](#ProductIdentificationRequest) | optional |  | → |
| firmwareVersionRequest | [FirmwareVersionRequest](#FirmwareVersionRequest) | optional |  | → |
| firmwareVersionStatus | [FirmwareVersionStatus](#FirmwareVersionStatus) | optional |  |
| currentPresetRequest | [CurrentPresetRequest](#CurrentPresetRequest) | optional |  | → |
| retrievePreset | [RetrievePreset](#RetrievePreset) | optional |  | → |
| usbGainRequest | [UsbGainRequest](#UsbGainRequest) | optional |  | → |
| usbGainStatus | [UsbGainStatus](#UsbGainStatus) | optional |  |
| qASlotsRequest | [QASlotsRequest](#QASlotsRequest) | optional |  | → |
| qASlotsStatus | [QASlotsStatus](#QASlotsStatus) | optional |  |
| lineOutGainRequest | [LineOutGainRequest](#LineOutGainRequest) | optional |  | → |
| lineOutGainStatus | [LineOutGainStatus](#LineOutGainStatus) | optional |  |
| modalStatusMessage | [ModalStatusMessage](#ModalStatusMessage) | optional |  |
| usbGainSet | [UsbGainSet](#UsbGainSet) | optional |  | → |
| lineOutGainSet | [LineOutGainSet](#LineOutGainSet) | optional |  | → |
| qASlotsSet | [QASlotsSet](#QASlotsSet) | optional |  | → |
| unsupportedMessageStatus | [UnsupportedMessageStatus](#UnsupportedMessageStatus) | optional |  |
| heartbeat | [Heartbeat](#Heartbeat) | optional |  | → |
| connectionStatusRequest | [ConnectionStatusRequest](#ConnectionStatusRequest) | optional |  | → |
| connectionStatus | [ConnectionStatus](#ConnectionStatus) | optional |  |
| indexPot | [IndexPot](#IndexPot) | optional |  |  |
| indexButton | [IndexButton](#IndexButton) | optional |  |  |
| indexEncoder | [IndexEncoder](#IndexEncoder) | optional |  |  |
| activeDisplay | [ActiveDisplay](#ActiveDisplay) | optional |  |  |
| frameBufferMessageRequest | [FrameBufferMessageRequest](#FrameBufferMessageRequest) | optional |  | → |
| frameBufferMessage | [FrameBufferMessage](#FrameBufferMessage) | optional |  |
| lt4FootswitchModeRequest | [LT4FootswitchModeRequest](#LT4FootswitchModeRequest) | optional |  | → |
| lt4FootswitchModeStatus | [LT4FootswitchModeStatus](#LT4FootswitchModeStatus) | optional |  |
| loadPreset_TestSuite | [LoadPreset_TestSuite](#LoadPreset_TestSuite) | optional |  |
| loopbackTest | [LoopbackTest](#LoopbackTest) | optional |  |

#### ResponseType
Message response type

| Name | Number | Description |
| ---- | ------ | ----------- |
| UNSOLICITED | 0 | Message sent not as the result of a command |
| NOT_LAST_ACK | 1 | Message sent as the result of a command, but NOT the last message in the batch |
| IS_LAST_ACK | 2 | Message sent as the last result of a command |

## Auditioning

### AuditionPreset
(→)
Sends a preset to the amp to be &#34;auditioned&#34;

response: [AuditionPresetStatus](#auditionpresetstatus) message containing the preset data loaded to the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required | [JSON](json.md) string conaining the preset to be auditioned |

### AuditionPresetStatus
(←)
Response to an [AuditionPreset](#auditionpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required | [JSON data](json.md) conatining preset data being auditioned |

### AuditionStateRequest
(→)
Queries the amp for its audition state

response: [AuditionStateStatus](#auditionstatestatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### AuditionStateStatus
(←)
The current audition state of the amp. Resposne to an [AuditionStateRequest](#auditionstaterequest) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| isAuditioning | [bool](#bool) | required | True if the amp is in audition mode |

### ExitAuditionPreset
(→)
Leaves audition mode

response: [ExitAuditionPresetStatus](#exitauditionpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| exit | [bool](#bool) | required | True to exit audition mode |

### ExitAuditionPresetStatus
(←)
Result message for an [ExitAuditionPreset](#exitauditionpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| isSuccess | [bool](#bool) | required | True if audition mode was exited successfully |

## Preset management

### CurrentPresetRequest
(→)
Requests the current preset from the amp

response: [CurrentPresetStatus](#currentpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### CurrentPresetSet
(→)
Sets the current preset of the amp

response: [CurrentPresetStatus](#currentpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentPresetData | [string](#string) | required | [JSON data](json.md) conatining preset data |

### CurrentPresetStatus
(←)
Returns the state of the current preset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentPresetData | [string](#string) | required | [JSON data](json.md) conatining current preset data |
| currentSlotIndex | [int32](#int32) | required | Current preset bank number |
| currentPresetDirtyStatus | [bool](#bool) | required | True if current preset has been edited and not saved |

### LoadPreset
(→)
Switches the amp to the specified preset number

response: [CurrentLoadedPresetIndexStatus](#currentloadedpresetindexstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetIndex | [int32](#int32) | required | Preset bank number currently loaded |

### CurrentDisplayedPresetIndexStatus
(←)
The current preset displayed on the amp. Sent when the preset is changed at the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentDisplayedPresetIndex | [int32](#int32) | required | The current preset bank number |

### CurrentLoadedPresetIndexStatus
(←)
The current preset displayed on the amp. Sent when the preset is changed at the amp or via a [LoadPreset](#loadpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentLoadedPresetIndex | [int32](#int32) | required | The current preset bank number |

### RetrievePreset
(←)
Queries the amp on the stored presets

response: [PresetJsonMessage](#presetjsonmessage)

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slot | [int32](#int32) | required | Preset bank to retrieve |

### PresetJSONMessage
(←)
Saved preset data

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data | [string](#string) | required | [JSON data](json.md) conatining preset data for requested preset bank |
| slotIndex | [int32](#int32) | required | Preset bank this preset is stored in |

### ShiftPreset
(→)
Moves a preset to the specified point, shifting all other presets

response: [ShiftPresetStatus](#shiftpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexToShiftFrom | [int32](#int32) | required | Preset to shift |
| indexToShiftTo | [int32](#int32) | required | Destination of preset to shift |

### ShiftPresetStatus
(←)
Response to a [ShiftPreset](#shiftpreset) message with the status of the command

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexToShiftFrom | [int32](#int32) | required | Preset shifted |
| indexToShiftTo | [int32](#int32) | required | Destination |

### SwapPreset
(→)
Swaps the presets in indexA and indexB

response: [SwapPresetStatus](#swappresetstatus) message
| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexA | [int32](#int32) | required |  |
| indexB | [int32](#int32) | required |  |

### SwapPresetStatus
(←)
result of a [SwapPreset](#swappreset) message
| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| indexA | [int32](#int32) | required |  |
| indexB | [int32](#int32) | required |  |

### SaveCurrentPreset
(→)
saves the current preset

response: [PresetSavedStatus](#presetsavedstatus)

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| save | [bool](#bool) | required |  |

### SaveCurrentPresetTo
(→)
response: [PresetSavedStatus](#presetsavedstatus)

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetName | [string](#string) | required |  |
| presetSlot | [int32](#int32) | required |  |

### SavePresetAs
(→)
response: [PresetSavedStatus](#presetsavedstatus)
| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required |  |
| isLoadPreset | [bool](#bool) | required |  |
| presetSlot | [int32](#int32) | required |  |

### PresetSavedStatus
(←)
Result of a [SaveCurrentPreset](#savecurrentpreset), [SaveCurrentPresetTo](#savecurrentpresetto), or [SavePresetAs](#savepresetas) message. Also sent when the preset is saved on the amp.

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| name | [string](#string) | required | Name of the preset saved |
| slot | [int32](#int32) | required | Preset bank of the saved preset |

### RenamePresetAt
(→)
Renames a preset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetName | [string](#string) | required |  new preset name |
| presetSlot | [int32](#int32) | required |  slot index to rename |

### NewPresetSavedStatus
(←)

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetData | [string](#string) | required |  |
| presetSlot | [int32](#int32) | required |  |

### ClearPreset
(→)
Clears a preset in the amp

response: [ClearPresetStatus](#clearpresetstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slotIndex | [int32](#int32) | required | Preset bank to clear |
| isLoadPreset | [bool](#bool) | required | ??? the Tone app sets this to true when clearing the preset |

### ClearPresetStatus
(←)
Response to a [ClearPreset](#clearpreset) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slotIndex | [int32](#int32) | required | bank number cleared |

### ReplaceNode
(→)
Swaps out a node (amp, stomp, mod, delay, or reverb)

response: [ReplaceNodeStatus](#replacenodestatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeIdToReplace | [string](#string) | required | nodeid to replace (amp, stomp, mod, delay, or reverb) |
| fenderIdToReplaceWith | [string](#string) | required | FenderId of node to set |

### ReplaceNodeStatus
(←)
Status of a [ReplaceNode](#replacenode) message; also sent when the node is changed at the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeIdReplaced | [string](#string) | required | node replaced (amp, stomp, mod, delay, or reverb) |
| fenderIdReplaced | [string](#string) | required | FenderId of node set |

### SetDspUnitParameter
(→)
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

### SetDspUnitParameterStatus
(←)
Status of the [SetDspUnitParameter](#setdspunitparameter) message. Also sent when parameters are adjusted on the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeId | [string](#string) | required | string id of the node changed (one of amp, stomp, mod, delay, reverb) |
| parameterId | [string](#string) | required | string id of the parameter to change |
| floatParameter | [float](#float) | optional |  |
| stringParameter | [string](#string) | optional |  |
| sint32Parameter | [sint32](#sint32) | optional |  |
| boolParameter | [bool](#bool) | optional |  |

### PresetEditedStatus
(←)

Editing status of the current preset

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetEdited | [bool](#bool) | required | True if the preset is in editing state Default: false |

## System Messages

### ModalStatusMessage (← →)
Changes the state of the amp

response: A [ModalStatusMessage](#modalstatusmessage) with the result of the request

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| context | [ModalContext](#ModalContext) | required | Context to switch to |
| state | [ModalState](#ModalState) | required | The result of the request. Requests to the amp are sent with an OK state |

#### ModalContext

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

#### ModalState

| Name | Number | Description |
| ---- | ------ | ----------- |
| OK | 0 | Success; also the default when sending this message to the amp |
| FAIL | 1 | Failure |

### ConnectionStatusRequest 
Queries the connection status of the amp

response: [ConnectionStatus](#connectionstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### ConnectionStatus
(←)
The current connection state of the amp. Response to a [ConnectionStatusRequest](#connectionstatusrequest) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| isConnected | [bool](#bool) | required | True if the amp is connected |

### FirmwareVersionRequest
(→)
Requests the current filrware version

response: [FirmwareVersionStatus](#firmwareversionstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### FirmwareVersionStatus
(←)
The firmware version of the amp

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| version | [string](#string) | required | Firmware Version |

### Heartbeat
(→)
Heartbeat message sent every second to keep the connection alive

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| dummyField | [bool](#bool) | required |  |

### MemoryUsageRequest
(→)
Requests memory usage statistics from the amp

response: [MemoryUsageStatus](#memoryusagestatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### MemoryUsageStatus
(←)
Message conainting current memory usage statistics

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| stack | [int32](#int32) | required |  |
| heap | [int32](#int32) | required |  |

### ProcessorUtilizationRequest
(→)
Requests processor statistics from the amp

response: [ProcessorUtilization](#processorutilization) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### ProcessorUtilization
(←)
Current processor utilization statistics

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| percent | [float](#float) | required |  |
| minPercent | [float](#float) | required |  |
| maxPercent | [float](#float) | required |  |

### ProductIdentificationRequest
(→)

Identifies the amplifier

response: [ProductIdentificationStatus](#productidentificationstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### ProductIdentificationStatus
(←)

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| id | [string](#string) | required |  |

## Footswitch settings control

### QASlotsRequest
(→)
Requests the settings of the footswitch presets

response: [QASlotsStatus](#qaslotsstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### QASlotsSet
(→)
Assigns preset banks to the footswitch

response: [QASlotsStatus](#qaslotsstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slots | [uint32](#uint32) | repeated | Preset banks to assign to the footswitch |

### QASlotsStatus
(←)
The current settings of the footswitch presets

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| slots | [uint32](#uint32) | repeated | The preset banks assigned to the footswitch |

## USB Gain Control

### UsbGainRequest
(→)
Gets the current USB Gain settings

response: [UsbGainStatus](#usbgainstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required |  |

### UsbGainSet
(→)
Sets the gain for the USB audio device

response: [UsbGainStatus](#usbgainstatus) message

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required | gain in dB |

### UsbGainStatus
(←)
The current setting of the gain for the USB audio device

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required | gain in dB |

### UnsupportedMessageStatus (← →)

Sent when a message is invalid

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| status | [ErrorType](#ErrorType) | required |  |

#### ErrorType

| Name | Number | Description |
| ---- | ------ | ----------- |
| UNSUPPORTED | 0 |  |
| FAILED | 1 |  |
| INVALID_PARAM | 2 |  |
| INVALID_NODE_ID | 3 |  |
| PARAM_OUT_OF_BOUNDS | 4 |  |
| FACTORY_RESTORE_IN_PROGRESS | 5 |  |

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

### ActiveDisplay

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| pageName | [string](#string) | required |  |

### CurrentLoadedPresetIndexBypassStatus
The bypass status of the DSP units.

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentLoadedPresetIndex | [int32](#int32) | required |  |
| bypassStatus | [bool](#bool) | repeated | Indicates whether the DSP unit is bypassed or not |

### DspUnitParameterStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| nodeId | [string](#string) | required |  |
| parameterId | [string](#string) | required |  |
| floatParameter | [float](#float) | optional |  |
| stringParameter | [string](#string) | optional |  |
| sint32Parameter | [sint32](#sint32) | optional |  |
| boolParameter | [bool](#bool) | optional |  |

### FrameBufferMessage

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data | [bytes](#bytes) | required |  |

### FrameBufferMessageRequest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### IndexButton

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| index | [int32](#int32) | required |  |
| event | [IndexButton.Event](#IndexButton-Event) | required |  |
| timestamp_ms | [uint32](#uint32) | optional |  |

#### IndexButton.Event

| Name | Number | Description |
| ---- | ------ | ----------- |
| EVENT_ERROR | 0 |  |
| BUTTON_DOWN | 1 |  |
| BUTTON_UP | 2 |  |
| BUTTON_HELD | 3 |  |

### IndexEncoder

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| index | [int32](#int32) | required |  |
| ticks | [int32](#int32) | required |  |

### IndexPot

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| index | [int32](#int32) | required |  |
| position | [float](#float) | required |  |

### LT4FootswitchModeRequest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### LT4FootswitchModeStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| currentMode | [LT4FtswModes](#LT4FtswModes) | required |  Default: LT4_FTSW_MODE_OFF |

#### LT4FtswModes

| Name | Number | Description |
| ---- | ------ | ----------- |
| LT4_FTSW_MODE_OFF | 0 |  |
| LT4_FTSW_MODE_BANK1 | 1 |  |
| LT4_FTSW_MODE_BANK2 | 2 |  |
| LT4_FTSW_MODE_FX | 3 |  |

### LineOutGainRequest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [bool](#bool) | required | Always true on requests |

### LineOutGainSet

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required |  |

### LineOutGainStatus

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| valueDB | [float](#float) | required |  |

### LoadPreset_TestSuite

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| presetIndex | [int32](#int32) | required |  |

### LoopbackTest

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| data | [string](#string) | required |  |

### PresetJSONMessageRequest_LT

| Field | Type | Label | Description |
| ----- | ---- | ----- | ----------- |
| request | [int32](#int32) | required |  |
