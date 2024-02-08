using LtAmpDotNet.Extensions;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using RtMidi.Net;
using RtMidi.Net.Clients;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class MidiCommandDefinition : BaseCommandDefinition
    {
        internal Preset? currentPreset;

        internal Node? CurrentAmp => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.amp);
        internal Node? CurrentStomp => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.stomp);
        internal Node? CurrentMod => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.mod);
        internal Node? CurrentDelay => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.delay);
        internal Node? CurrentReverb => currentPreset?.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.reverb);

        internal Dictionary<MidiMessageType, Dictionary<int, Action<int>>> eventCommands = [];

        internal List<MidiInputClient> clients = [];

        internal MidiCommandDefinition() : base("midi", "Listen to midi messages")
        {
            Argument<List<uint>> midiDeviceArgument = new(
               name: "deviceId",
               description: "MIDI deviceId",
               getDefaultValue: () => [0]
            );

            AddArgument(midiDeviceArgument);
            this.SetHandler(Start, midiDeviceArgument);

            Command midiListCommand = new("list", "List midi devices");
            midiListCommand.SetHandler(ListDevices);
            AddCommand(midiListCommand);
        }

        internal async Task Start(List<uint> deviceIds)
        {
            Console.WriteLine("Connecting");
            BuildConfig();
            await OpenMidi(deviceIds);
            await OpenAmp();
            Amp!.MessageReceived += Amp_MessageReceived;
            Amp!.CurrentPresetStatusMessageReceived += Amp_CurrentPresetStatusMessageReceived;
            Amp!.GetCurrentPreset();
            Console.WriteLine("Connected");
            while ((Console.ReadLine()) != null)
            {

            }
        }

        internal async Task OpenMidi(List<uint> deviceIds)
        {
            foreach (uint deviceId in deviceIds)
            {
                try
                {
                    var device = MidiManager.GetDeviceInfo(deviceId, RtMidi.Net.Enums.MidiDeviceType.Input);
                    MidiInputClient client = new MidiInputClient(device);
                    client.OnMessageReceived += MidiIn_MessageReceived;
                    client.ActivateMessageReceivedEvent();
                    client.Open();
                    clients.Add(client);
                    Console.WriteLine($"Device {deviceId} connected");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to {deviceId}: {ex.Message}");
                    Environment.Exit(1);
                }
            }
        }

        internal void ListDevices()
        {
            foreach (var device in MidiManager.GetAvailableDevices())
            {
                Console.WriteLine($"{device.Port} {device.Name} {device.Type} {device.Api}");
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
                Amp!.GetCurrentPreset();
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
                try
                {
                    DspUnitUiParameter currentParameterDefinition = currentPreset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == nodeId)?.Definition.Ui?.UiParameters?.SingleOrDefault(x => x.ControlId == parameter)!;
                    DspUnitParameter currentParameter = currentPreset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == nodeId)?.DspUnitParameters?.SingleOrDefault(x => x.Name == parameter)!;

                    if (currentParameterDefinition != null)
                    {
                        switch (currentParameter.ParameterType)
                        {
                            case DspUnitParameterType.Boolean:
                                currentParameter.Value = value > 64;
                                break;
                            case DspUnitParameterType.String:
                                int itemNumber = (int)Math.Round(((float)value).Remap(0, 128, 0, currentParameterDefinition.ListItems!.Count()), 0);
                                currentParameter.Value = currentParameterDefinition.ListItems!.ToArray()[itemNumber];
                                break;
                            case DspUnitParameterType.Integer:
                                currentParameter.Value = currentParameterDefinition.NumTicks > 0
                                    ? (int)Math.Round(((float)value).Remap(0, 128, 0, 91), 0)
                                    : (dynamic)(int)Math.Round(((float)value).Remap(0, 128, currentParameterDefinition.Min!.Value, currentParameterDefinition.Max!.Value), 0);
                                break;
                            case DspUnitParameterType.Float:
                                currentParameter.Value = currentParameterDefinition.NumTicks > 0
                                    ? ((float)value).Remap(0, 128, 0, 91)
                                    : (dynamic)((float)value).Remap(0, 128, currentParameterDefinition.Min!.Value, currentParameterDefinition.Max!.Value);
                                break;
                        }
                        Amp.SetDspUnitParameter(nodeId, new DspUnitParameter() { Name = currentParameterDefinition.ControlId, Value = value });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error setting DSP parameter");
                }
            }
        }

        internal void SetParameterByIndex(NodeIdType nodeId, int paramNumber, int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                if (CurrentStomp?.Definition?.Ui?.UiParameters?.Count() < paramNumber)
                {
                    string parameterName = CurrentStomp.Definition.Ui.UiParameters.ToArray()[paramNumber].ControlId!;
                    SetDspParameter(nodeId, parameterName, value);
                }
            }
        }

        internal void BuildConfig()
        {
            if (Program.Configuration?.MidiCommands != null)
            {
                eventCommands = new Dictionary<MidiMessageType, Dictionary<int, Action<int>>>(){
                    { MidiMessageType.ControlChange, new Dictionary<int, Action<int>>() },
                    { MidiMessageType.ProgramChange, new Dictionary<int, Action<int>>() }
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
        }

        internal void Amp_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Console.WriteLine($"[AMP ] {e.Message}");
        }

        internal void Amp_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            currentPreset = Preset.FromString(e.Message!.CurrentPresetStatus.CurrentPresetData)!;
        }

        internal void MidiIn_MessageReceived(object? sender, RtMidi.Net.Events.MidiMessageReceivedEventArgs e)
        {
            switch (e.Message.Type)
            {
                case RtMidi.Net.Enums.MidiMessageType.ControlChange:
                    var ccMessage = (MidiMessageControlChange)e.Message;
                    if (eventCommands[MidiMessageType.ControlChange].ContainsKey(ccMessage.ControlFunction))
                    {
                        eventCommands[MidiMessageType.ControlChange][ccMessage.ControlFunction].Invoke(ccMessage.Value);
                    }
                    Console.WriteLine($"[MIDI] {ccMessage.Type}: {ccMessage.ControlFunction}: {ccMessage.Value}");
                    var configOption = Program.Configuration.MidiCommands.SingleOrDefault(x => x.CommandType == MidiMessageType.ControlChange && x.Command == ccMessage.ControlFunction);
                    Console.WriteLine(configOption.Value);
                    break;
                case RtMidi.Net.Enums.MidiMessageType.ProgramChange:
                    var pcMessage = (MidiMessageProgramChange)e.Message;
                    if (eventCommands.TryGetValue(MidiMessageType.ProgramChange, out Dictionary<int, Action<int>>? patchValue))
                    {
                        patchValue[0].Invoke(pcMessage.Program);
                    }
                    Console.WriteLine($"[MIDI] {pcMessage.Type}: {pcMessage.Program}");
                    break;
                default:
                    Console.WriteLine($"[MIDI] {e.Message.Type}");
                    break;
            }
        }
    }
}

