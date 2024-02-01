using Google.Protobuf.WellKnownTypes;
using HidSharp;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli.Commands
{
    internal class MidiCommandDefinition : BaseCommandDefinition
    {
        internal override Command CommandDefinition { get; set; }

        internal Preset? currentPreset;

        internal readonly Dictionary<MidiCommandCode, Dictionary<MidiController, Action<int>>> eventCommands;

        internal MidiCommandDefinition()
        {
            var midiDeviceArgument = new Argument<int>(
               name: "deviceId",
               description: "MIDI deviceId",
               getDefaultValue: () => 0
            );

            var midiCommand = new Command("midi", "Listen to MIDI mesages");
            midiCommand.AddArgument(midiDeviceArgument);
            midiCommand.SetHandler(StartListening, midiDeviceArgument);

            var midiListCommand = new Command("list", "List midi devices");
            midiListCommand.SetHandler(ListDevices);
            midiCommand.AddCommand(midiListCommand);

            CommandDefinition = midiCommand;

            eventCommands = new Dictionary<MidiCommandCode, Dictionary<MidiController, Action<int>>>()
            {
                {
                    MidiCommandCode.ControlChange, new Dictionary<MidiController, Action<int>>()
                    {
                        { (MidiController)20, (value) => EnableTuner(value) },      // Tuner
                        { (MidiController)22, (value) => BypassAll(value) },        // Bypass all
                        { (MidiController)23, (value) => Bypass("stomp", value) },  // Stomp Bypass
                        { (MidiController)24, (value) => Bypass("mod", value) },    // Mod Bypass
                        { (MidiController)25, (value) => Bypass("delay", value) },  // Delay Bypass
                        { (MidiController)26, (value) => Bypass("reverb", value) }, // Reverb Bypass
                    }
                }
            };
        }

        internal void StartListening(int deviceId)
        {
            Open();
            Amp!.AmplifierConnected += Amp_AmplifierConnected;
            Amp!.MessageReceived += Amp_MessageReceived;

            var midiIn = new MidiIn(deviceId);
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

        internal void Amp_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Console.WriteLine($"[AMP] {e.Message}");
        }

        internal void Amp_AmplifierConnected(object? sender, EventArgs e)
        {
            Amp!.CurrentPresetStatusMessageReceived += Amp_CurrentPresetStatusMessageReceived;
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
                    var ccEvent = (ControlChangeEvent)e.MidiEvent;
                    eventCommands[ccEvent.CommandCode][ccEvent.Controller].Invoke(ccEvent.ControllerValue);
                    Console.WriteLine($"[MIDI] {ccEvent.CommandCode}: {ccEvent.Channel}: {ccEvent.Controller}: {ccEvent.ControllerValue}");
                    break;
                default:
                    Console.WriteLine($"[MIDI] {e.MidiEvent.CommandCode}: {e.MidiEvent.Channel}: {e.RawMessage}");
                    break;
            }
        }

        internal void Bypass(string nodeId, int value)
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
            Bypass("stomp", value);
            Bypass("mod", value);
            Bypass("delay", value);
            Bypass("reverb", value);
        }
    }
}
