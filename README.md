# MustangLT
Reverse engineering the USB protocol of the Fender Mustang LT series guitar amps

## Protocol
The amp connects via USB, and exposes two devices: an audio device, and a HID device.

The HID device is used for communication between the computer and amp over interrupt transfers.

Each packet is 64 bytes, with 1 byte of padding, and encoded as TLV in the following format:

Host -> Device
| 1 | 2 | 3-63 | 64 |
|---|---|------|----|
| tag | length | value | 0x00 |

Device -> Host
| 1 | 2 | 3 | 4-64 |
|---|---|------|----|
| 0x00 | tag | length | value |

The tags I've identified are as follows:
| Tag | Meaning |
|---|---|
| 0x33 | Start of multi-packet data |
| 0x34 | Continuation of multi-packet data |
| 0x35 | Final packet (or only packet) |

For multi-packet messages, it's necessary to drop the `0x00` padding and the tag and length bytes of each packet and concatenate them to get the message.

The value in each message is a protobuf encoded message ([`FenderMessageLT`](doc/protobuf.md#fendermessagelt)) that contains a [`responseType`](doc/protobuf.md#ResponseType) and a submessage containing the data for the message ([documentation](doc/protobuf.md)).

The [protobuf definition files](Schema/protobuf) were extracted from the Fender Mustang LT Desktop executable.

## Connection process
The Fender Tone LT Desktop app initializes the connection by sending `SYNC_BEGIN` [`modalStatusMessage`](doc/protobuf.md#modalstatusmessage), and a [`firmwareVersionRequest`](doc/protobuf.md#firmwareversionrequest), followed by a [`retrievePreset`](doc/protobuf.md#retrievepreset) message for all 60 presets:
_(>>> is host to amp, <<< is amp to host)_
```
>>> { "responseType": "UNSOLICITED", "modalStatusMessage": { "context": "SYNC_BEGIN", "state": "OK" } }
<<< { "responseType": "IS_LAST_ACK", "modalStatusMessage": { "context": "SYNC_BEGIN", "state": "OK" } }

>>> { "responseType": "UNSOLICITED", "firmwareVersionRequest": { "request": true } }
<<< { "responseType": "IS_LAST_ACK", "firmwareVersionStatus": { "version": "2.1.4" } }

>>> { "responseType": "UNSOLICITED", "retrievePreset": { "slot": 1 } }
<<< { "responseType": "IS_LAST_ACK", "presetJSONMessage": { "data": "{...}", "slotIndex": 1   } }

>>> { "responseType": "UNSOLICITED", "retrievePreset": { "slot": 2 } }
<<< { "responseType": "IS_LAST_ACK", "presetJSONMessage": { "data": "{...}", "slotIndex": 2   } }

...

>>> { "responseType": "UNSOLICITED", "retrievePreset": { "slot": 60 } }
<<< { "responseType": "IS_LAST_ACK", "presetJSONMessage": { "data": "{...}", "slotIndex": 60   } }
```

In my testing, initializing the connection to the amp only required a `SYNC_BEGIN` [`modalStatusMessage`](doc/protobuf.md#modalstatusmessage), followed by a `SYNC_END` [`modalStatusMessage`](doc/protobuf.md#modalstatusmessage), in order to clear the synchronization message from the screen.
```
>>> { "responseType": "UNSOLICITED", "modalStatusMessage": { "context": "SYNC_BEGIN", "state": "OK" } }
<<< { "responseType": "IS_LAST_ACK", "modalStatusMessage": { "context": "SYNC_BEGIN", "state": "OK" } }

>>> { "responseType": "UNSOLICITED", "modalStatusMessage": { "context": "SYNC_END", "state": "OK" } }
<<< { "responseType": "IS_LAST_ACK", "modalStatusMessage": { "context": "SYNC_END", "state": "OK" } }
```

It's also necessary to send a [`heartbeat`](doc/protobuf.md#heartbeat) message every second to keep the connection open.
```
>>> { "responseType": "UNSOLICITED", "heartbeat": { "dummyField": true } }
```
