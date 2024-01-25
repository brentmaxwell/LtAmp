# LtAmp
Reverse engineering the USB protocol of the Fender Mustang LT series guitar amps.

## Protocol Documentation
Communication with the amp is over USB HID using protobuf. Documentation can be found [here](/Docs/Protocol.md)

## LtAmpDotNet
A .NET Core library for communicating with the amp. It uses [HidSharp](https://github.com/IntergatedCircuits/HidSharp) for cross-platfrom compatibility.

A cross-platform GUI is in the roadmap.

## Roadmap
Initally, this has been deceloped in dotnet to allow for quick scaffolding of the protocol.

### Future plans:
- Cross-platform GUI
- Import/export presets to/from files for sharing
- Integrated preset library to store an unlimited number of presets in the computer
- Footswitch "playlists", using a sequence of presets to be cycled through
- Arduino library (imagine a foot pedal eith switches to toggle bypass of thr individual effects!)
- Keyboard shortcuts to control effect parameters; specifically to be able to set up conplex changes via macros
- MIDI control of effect parameters
- Integrated VU meter to monitor sound levels from the USB audio inteface for volume leveling across presets (and maybe a spectrogram if the latency isnt too bad)
- Cooy and paste effect units between presets

