using LtAmpDotNet.Lib.Model.Preset;
using NAudio.Midi;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class MidiCommandDefinition : BaseCommandDefinition
    {
        internal Preset? currentPreset;

        internal readonly Dictionary<MidiCommandCode, Dictionary<MidiController, Action<int>>> eventCommands;

        internal MidiCommandDefinition() : base("midi", "Listen to midi messages")
        {
            Argument<int> midiDeviceArgument = new Argument<int>(
               name: "deviceId",
               description: "MIDI deviceId",
               getDefaultValue: () => 0
            );

            AddArgument(midiDeviceArgument);
            this.SetHandler(StartListening, midiDeviceArgument);

            Command midiListCommand = new Command("list", "List midi devices");
            midiListCommand.SetHandler(ListDevices);
            AddCommand(midiListCommand);

            eventCommands = new Dictionary<MidiCommandCode, Dictionary<MidiController, Action<int>>>()
            {
                {
                    MidiCommandCode.ControlChange, new Dictionary<MidiController, Action<int>>()
                    {
                        { (MidiController)20, (value) => EnableTuner(value) },      // Tuner
                        { (MidiController)22, (value) => BypassAll(value) },        // Bypass all
                        { (MidiController)23, (value) => Bypass(NodeIdType.stomp, value) },  // Stomp Bypass
                        { (MidiController)24, (value) => Bypass(NodeIdType.mod, value) },    // Mod Bypass
                        { (MidiController)25, (value) => Bypass(NodeIdType.delay, value) },  // Delay Bypass
                        { (MidiController)26, (value) => Bypass(NodeIdType.reverb, value) }, // Reverb Bypass
                    }
                }
            };
        }

        internal async Task StartListening(int deviceId)
        {
            await Open();
            Amp!.MessageReceived += Amp_MessageReceived;
            Amp!.CurrentPresetStatusMessageReceived += Amp_CurrentPresetStatusMessageReceived;
            MidiIn midiIn = new MidiIn(deviceId);
            midiIn.MessageReceived += MidiIn_MessageReceived; ;
            midiIn.ErrorReceived += MidiIn_ErrorReceived;
            midiIn.Start();
            Console.WriteLine("Connected");
            while (true) { Thread.Sleep(100); }
        }

        internal void ListDevices()
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                Console.WriteLine($"{device} {MidiIn.DeviceInfo(device).ProductName} {MidiIn.DeviceInfo(device).ProductName}");
            }
        }

        internal void Bypass(NodeIdType nodeId, int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                Amp.SetDspUnitParameter(nodeId, new DspUnitParameter() { Name = "bypass", Value = value > 64 });
            }
        }

        internal void EnableTuner(int value)
        {
            if (Amp != null && Amp.IsOpen)
            {
                Amp.SetTuner(value > 64);
            }
        }

        internal void BypassAll(int value)
        {
            Bypass(NodeIdType.stomp, value);
            Bypass(NodeIdType.mod, value);
            Bypass(NodeIdType.delay, value);
            Bypass(NodeIdType.reverb, value);
        }

        internal void Amp_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Console.WriteLine($"[AMP] {e.Message}");
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
                    eventCommands[ccEvent.CommandCode][ccEvent.Controller].Invoke(ccEvent.ControllerValue);
                    Console.WriteLine($"[MIDI] {ccEvent.CommandCode}: {ccEvent.Channel}: {ccEvent.Controller}: {ccEvent.ControllerValue}");
                    break;
                default:
                    Console.WriteLine($"[MIDI] {e.MidiEvent.CommandCode}: {e.MidiEvent.Channel}: {e.RawMessage}");
                    break;
            }
        }
    }
}

