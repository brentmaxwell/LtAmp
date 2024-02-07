using HidSharp;
using LtAmpDotNet.Extensions;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using NAudio.Midi;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class MidiCommandDefinition : BaseCommandDefinition
    {
        internal Preset? currentPreset;

        internal Node? currentAmp => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.amp);
        internal Node? currentStomp => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.stomp);
        internal Node? currentMod => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.mod);
        internal Node? currentDelay => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.delay);
        internal Node? currentReverb => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.reverb);

        internal Dictionary<MidiCommandCode, Dictionary<int, Action<int>>> eventCommands = [];

        internal MidiCommandDefinition() : base("midi", "Listen to midi messages")
        {
            Argument<List<int>> midiDeviceArgument = new(
               name: "deviceId",
               description: "MIDI deviceId",
               getDefaultValue: () => [0]
            );

            AddArgument(midiDeviceArgument);
            this.SetHandler(StartListening, midiDeviceArgument);

            Command midiListCommand = new("list", "List midi devices");
            midiListCommand.SetHandler(ListDevices);
            AddCommand(midiListCommand);

            //eventCommands = new Dictionary<MidiCommandCode, Dictionary<int, Action<int>>>()
            //{
            //    {
            //        MidiCommandCode.ControlChange, new Dictionary<int, Action<int>>()
            //        {
            //            { 20, EnableTuner },      // Tuner
            //            { 22, BypassAll },        // Bypass all
            //            { 23, (value) => SetBypass(NodeIdType.stomp, value) },  // Stomp Bypass
            //            { 24, (value) => SetBypass(NodeIdType.mod, value) },    // Mod Bypass
            //            { 25, (value) => SetBypass(NodeIdType.delay, value) },  // Delay Bypass
            //            { 26, (value) => SetBypass(NodeIdType.reverb, value) }, // Reverb Bypass
            //            { 69, (value) => SetDspParameter(NodeIdType.amp, "gain", value) }, // Amp gain
            //            { 70, (value) => SetDspParameter(NodeIdType.amp, "volume", value) }, // Amp volume
            //            { 71, (value) => SetDspParameter(NodeIdType.amp, "treble", value) }, // Amp
            //            { 72, (value) => SetDspParameter(NodeIdType.amp, "mid", value) }, // Amp
            //            { 73, (value) => SetDspParameter(NodeIdType.amp, "bass", value) }, // Amp
            //        }
            //    },
            //    {
            //        MidiCommandCode.PatchChange, new Dictionary<int, Action<int>>()
            //        {
            //            { 0, LoadPreset }
            //        }
            //    }
            //};
        }

        internal async Task StartListening(List<int> deviceIds)
        {
            List<MidiIn> midiDevices = [];
            foreach (int deviceId in deviceIds)
            {
                if (deviceId + 1 > MidiIn.NumberOfDevices)
                {
                    Console.Error.WriteLine($"Error: No such midi device (device {deviceId})");
                    return;
                }
                else
                {
                    MidiIn midiIn = new(deviceId);
                    midiIn.MessageReceived += MidiIn_MessageReceived; ;
                    midiIn.ErrorReceived += MidiIn_ErrorReceived;
                    midiIn.Start();
                    midiDevices.Add(midiIn);
                }

                if (Program.Configuration?.MidiCommands != null)
                {
                    Configuration.Load();
                    eventCommands = new Dictionary<MidiCommandCode, Dictionary<int, Action<int>>>(){
                        { MidiCommandCode.ControlChange, new Dictionary<int, Action<int>>() },
                        { MidiCommandCode.PatchChange, new Dictionary<int, Action<int>>() }
                    };
                    foreach (MidiCommand item in Program.Configuration.MidiCommands)
                    {
                        switch (item.Value)
                        {
                            case "EnableTuner":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), EnableTuner);
                                break;
                            case "SetBypassAll":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), BypassAll);
                                break;
                            case "SetStompBypass":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.stomp, value));
                                break;
                            case "SetStompParameter1":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.stomp, 1, value));
                                break;
                            case "SetStompParameter2":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.stomp, 2, value));
                                break;
                            case "SetStompParameter3":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.stomp, 3, value));
                                break;
                            case "SetStompParameter4":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.stomp, 4, value));
                                break;
                            case "SetStompParameter5":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.stomp, 5, value));
                                break;
                            case "SetModBypass":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.mod, value));
                                break;
                            case "SetModParameter1":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.mod, 1, value));
                                break;
                            case "SetModParameter2":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.mod, 2, value));
                                break;
                            case "SetModParameter3":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.mod, 3, value));
                                break;
                            case "SetModParameter4":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.mod, 4, value));
                                break;
                            case "SetModParameter5":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.mod, 5, value));
                                break;
                            case "SetDelayBypass":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.delay, value));
                                break;
                            case "SetDelayParameter1":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.delay, 1, value));
                                break;
                            case "SetDelayParameter2":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.delay, 2, value));
                                break;
                            case "SetDelayParameter3":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.delay, 3, value));
                                break;
                            case "SetDelayParameter4":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.delay, 4, value));
                                break;
                            case "SetDelayParameter5":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.delay, 5, value));
                                break;
                            case "SetReverbBypass":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.reverb, value));
                                break;
                            case "SetReverbParameter1":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.reverb, 1, value));
                                break;
                            case "SetReverbParameter2":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.reverb, 2, value));
                                break;
                            case "SetReverbParameter3":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.reverb, 3, value));
                                break;
                            case "SetReverbParameter4":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.reverb, 4, value));
                                break;
                            case "SetReverbParameter5":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetParameterByIndex(NodeIdType.reverb, 5, value));
                                break;
                            case "SetAmpGain":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetDspParameter(NodeIdType.amp, "gain", value));
                                break;
                            case "SetAmpVolume":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetDspParameter(NodeIdType.amp, "volume", value));
                                break;
                            case "SetAmpTreble":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetDspParameter(NodeIdType.amp, "treb", value));
                                break;
                            case "SetAmpMiddle":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetDspParameter(NodeIdType.amp, "mid", value));
                                break;
                            case "SetAmpBass":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetDspParameter(NodeIdType.amp, "bass", value));
                                break;
                            case "LoadPreset":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), LoadPreset);
                                break;
                        }
                    }
                }

                await Open();
                Amp!.MessageReceived += Amp_MessageReceived;
                Amp!.CurrentPresetStatusMessageReceived += Amp_CurrentPresetStatusMessageReceived;
                Console.WriteLine("Connected");
                while (true) { Thread.Sleep(100); }
            }
        }

        internal void ListDevices()
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                Console.WriteLine($"{device} {MidiIn.DeviceInfo(device).ProductName} {MidiIn.DeviceInfo(device).ProductName}");
            }
        }

        internal void EnableTuner(int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                Amp.SetTuner(value > 64);
            }
        }

        internal void LoadPreset(int value)
        {
            if (Amp != null && Amp.IsOpen && value > 0 && value <= LtAmplifier.NUM_OF_PRESETS)
            {
                Amp.LoadPreset(value);
            }
        }

        internal void SetBypass(NodeIdType nodeId, int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                Amp.SetDspUnitParameter(nodeId, new DspUnitParameter() { Name = "bypass", Value = value > 64 });
            }
        }

        internal void BypassAll(int value)
        {
            SetBypass(NodeIdType.stomp, value);
            SetBypass(NodeIdType.mod, value);
            SetBypass(NodeIdType.delay, value);
            SetBypass(NodeIdType.reverb, value);
        }

        internal void SetDspParameter(NodeIdType nodeId, string parameter, int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                DspUnitUiParameter currentParameterDefinition = currentStomp?.Definition.Ui?.UiParameters?.SingleOrDefault(x => x.ControlId == parameter)!;
                DspUnitParameter currentParameter = currentStomp?.DspUnitParameters?.SingleOrDefault(x => x.Name == parameter)!;
                if (currentParameterDefinition != null)
                {
                    switch (currentParameter.ParameterType)
                    {
                        case DspUnitParameterType.Boolean:
                            currentParameter.Value = value > 64;
                            break;
                        case DspUnitParameterType.String:
                            int itemNumber = (int)Math.Round(((float)value).Remap(0, 128, 0, (float)currentParameterDefinition.ListItems?.Count()), 0);
                            currentParameter.Value = currentParameterDefinition.ListItems.ToArray()[itemNumber];
                            break;
                        case DspUnitParameterType.Integer:
                            currentParameter.Value = currentParameterDefinition.NumTicks > 0
                                ? (int)Math.Round(((float)value).Remap(0, 128, 0, 91), 0)
                                : (dynamic)(int)Math.Round(((float)value).Remap(0, 128, currentParameterDefinition.Min.Value, currentParameterDefinition.Max.Value), 0);
                            break;
                        case DspUnitParameterType.Float:
                            currentParameter.Value = currentParameterDefinition.NumTicks > 0
                                ? ((float)value).Remap(0, 128, 0, 91)
                                : (dynamic)((float)value).Remap(0, 128, currentParameterDefinition.Min.Value, currentParameterDefinition.Max.Value);
                            break;
                    }
                    Amp.SetDspUnitParameter(nodeId, new DspUnitParameter() { Name = currentParameterDefinition.ControlId, Value = value });
                }
            }
        }

        internal void SetParameterByIndex(NodeIdType nodeId, int paramNumber, int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                if (currentStomp?.Definition?.Ui?.UiParameters?.Count() < paramNumber)
                {
                    string parameterName = currentStomp.Definition.Ui.UiParameters.ToArray()[paramNumber].ControlId!;
                    SetDspParameter(nodeId, parameterName, value);
                }
            }
        }

        internal void Amp_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Console.WriteLine($"[AMP ] {e.Message}");
        }

        internal void Amp_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            currentPreset = Preset.FromString(e.Message!.CurrentPresetStatus.CurrentPresetData)!;
        }

        internal void MidiIn_ErrorReceived(object? sender, MidiInMessageEventArgs e)
        {
            Console.WriteLine($"[MIDI] Error: {e.RawMessage}");
        }

        internal void MidiIn_MessageReceived(object? sender, MidiInMessageEventArgs e)
        {
            switch (e.MidiEvent.CommandCode)
            {
                case MidiCommandCode.ControlChange:
                    ControlChangeEvent ccEvent = (ControlChangeEvent)e.MidiEvent;
                    if (eventCommands[ccEvent.CommandCode].ContainsKey((byte)ccEvent.Controller))
                    {
                        eventCommands[ccEvent.CommandCode][(byte)ccEvent.Controller].Invoke(ccEvent.ControllerValue);
                    }
                    Console.WriteLine($"[MIDI] {ccEvent.CommandCode}: {ccEvent.Channel}: {ccEvent.Controller}: {ccEvent.ControllerValue}");
                    break;
                case MidiCommandCode.PatchChange:
                    PatchChangeEvent pEvent = (PatchChangeEvent)e.MidiEvent;
                    if (eventCommands.TryGetValue(pEvent.CommandCode, out Dictionary<int, Action<int>>? patchValue))
                    {
                        patchValue[0].Invoke(pEvent.Patch);
                    }
                    Console.WriteLine($"[MIDI] {pEvent.CommandCode}: {pEvent.Channel}: {pEvent.Patch}");
                    break;
                default:
                    Console.WriteLine($"[MIDI] {e.MidiEvent.CommandCode}: {e.MidiEvent.Channel}: {e.RawMessage}");
                    break;
            }
        }
    }
}

