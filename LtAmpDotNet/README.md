# LtAmpDotNet

This is a library to handle communication with the Fender LT series amps.

I have a Mustang LT 25, so that is the only amp that has been tested.

The entire thing is completely event driven; messages are sent to the amp, and then events are fired when messages are returned.

For example, you attach a method to the `CurrentLoadedPresetIndexStatusMessageReceived` event, and then when the knob on the amp is turned to change the preset, the event will fire.

Or, you attach a method to the `PresetJSONMessageReceived` event, and then call `GetPreset()`, and the event will fire when the data comes back.