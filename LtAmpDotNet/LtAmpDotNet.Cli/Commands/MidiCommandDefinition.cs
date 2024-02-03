using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using NAudio.Midi;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class MidiCommandDefinition : BaseCommandDefinition
    {
        internal Preset? currentPreset;

        internal Dictionary<MidiCommandCode, Dictionary<int, Action<int>>> eventCommands = [];

        internal MidiCommandDefinition() : base("midi", "Listen to midi messages")
        {
            Argument<int> midiDeviceArgument = new(
               name: "deviceId",
               description: "MIDI deviceId",
               getDefaultValue: () => 0
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

        internal async Task StartListening(int deviceId)
        {
            if (deviceId + 1 > MidiIn.NumberOfDevices)
            {
                Console.Error.WriteLine("Error: No such midi device");
            }
            else
            {
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
                            case "SetBypassStomp":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.stomp, value));
                                break;
                            case "SetBypassMod":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.mod, value));
                                break;
                            case "SetBypassDelay":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.delay, value));
                                break;
                            case "SetBypassReverb":
                                eventCommands[item.CommandType].Add(item.Command.GetValueOrDefault(), (value) => SetBypass(NodeIdType.reverb, value));
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
                MidiIn midiIn = new(deviceId);
                midiIn.MessageReceived += MidiIn_MessageReceived; ;
                midiIn.ErrorReceived += MidiIn_ErrorReceived;
                midiIn.Start();
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

        internal void SetDspParameter(NodeIdType nodeId, string parameter, int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                Amp.SetDspUnitParameter(nodeId, new DspUnitParameter() { Name = parameter, Value = value });
            }
        }

        internal void BypassAll(int value)
        {
            SetBypass(NodeIdType.stomp, value);
            SetBypass(NodeIdType.mod, value);
            SetBypass(NodeIdType.delay, value);
            SetBypass(NodeIdType.reverb, value);
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

