# JSON Preset format

## Preset

| Property | Type | Description |
| -------- | ---- | ----------- |
| nodeId | string | "preset" |
| nodeType | string | "preset" |
| version | string | firmware version for the preset? Only seen "1.1" |
| numInputs | integer | Number of inputs (assuming this refers to L/R stereo?). Only seen 2 as a value |
| numOutputs | integer | Number of outputs (assuming this refers to L/R stereo?). Only seen 2 as a value |
| info | Object | Metadata about the preset |
| audioGraph | Object | Audio settings for the preset |

## Info

| Property | Type | Description |
| -------- | ---- | ----------- |
| displayName | string | Name to be displayed on the amp. 16 characters, displayed on the amp in two 8-charachter lines |
| product_id | string | Product ID this preset is compatible with.
| author | string | Author of the preset? I've only seen empty strings |
| timestamp | integer | Unix timestamp of the preset. Not sure if this is created or modified time |
| created_at | integer | Unix timestamp of created time? Only seen 0 |
| bpm | integer | BPM value for effects that utilize this (certain delays and reverbs, etc.) |
| preset_id | guid | All presets I've seen have the same id (82701e3e-caf7-11e7-b721-171e6c7d3090), or an empty string |
| source_id | string | unknown; all presets I've seen are blank |
| is_factory_default | boolean | Strangely, I've only seen this set to true |

## AudioGraph

| Property | Type | Description |
| -------- | ---- | ----------- |
| connections | Array(Object) | The connection graph for the DSP nodes. Pretty much the same for every preset; unsure if changing this has any effect |
| nodes | Array(Object) | The specific DSP units for the preset |

## Connections

This connection graph traces the linkage between the DSP units, from Preset to Amp. I've only seen the same connection graph for each preset, and the default is this:

Preset -> Stomp -> Mod -> Amp -> Delay -> Reverb -> Preset

| Property | Type | Description |
| -------- | ---- | ----------- |
| input | Object | the input point for this connection
| output | Object | the output point for this connection


### Input/Output

| Property | Type | Description |
| -------- | ---- | ----------- |
| index | integer | index of input or output; assuming it refers to L/R in a stereo connection, so I've only seen 0 and 1 |
| nodeId | string | ID of node pertaining to this connection (amp, stomp, mod, delay, reverb) |

## Nodes

| Property | Type | Description |
| -------- | ---- | ----------- |
| nodeId | string | type of DSP Unit (amp, stomp, mod, delay, reverb) |
| nodeType | string | always "dspUnit" |
| FenderId | string | FenderID of the DSP unit |
| dspUnitParameters | Object | Parameters for the DSP Unit |

### dspUnitParameters

Each DSP unit has it's own set of parameters, with their own data types. They are all stored in a simple "key": "value" form, and are all one of integer, float, boolean, or string.

For stomp, mod, delay, and reverb DSP units, their are two common parameters: "bypass" and "bypassType".
