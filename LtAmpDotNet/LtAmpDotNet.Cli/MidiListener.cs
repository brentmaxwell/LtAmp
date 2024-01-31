using Google.Protobuf.WellKnownTypes;
using HidSharp;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli
{
    internal class MidiListener
    {
        private LtAmplifier amp = new LtAmplifier();

        private Preset currentPreset;

        //private Dictionary<MidiEvent, Action> events;

        private Dictionary<MidiCommandCode, Dictionary<MidiController, Action<int>>> eventCommands;


        public MidiListener()
        {
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

        public void Open(int device = 0)
        {
            Console.Write($"Connecting to device {0}...");
            amp.AmplifierConnected += Amp_AmplifierConnected;
            amp.MessageReceived += Amp_MessageReceived;
            amp.Open();
            var midiIn = new MidiIn(device);
            midiIn.MessageReceived += MidiIn_MessageReceived; ;
            midiIn.ErrorReceived += MidiIn_ErrorReceived;
            midiIn.Start();
            Console.WriteLine("Connected");
            while (true) { Thread.Sleep(100); }
        }

        private void Amp_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Console.WriteLine($"[AMP] {e.Message}");
        }

        private void Amp_AmplifierConnected(object? sender, EventArgs e)
        {
            amp.CurrentPresetStatusMessageReceived += Amp_CurrentPresetStatusMessageReceived;
        }

        private void Amp_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            currentPreset = Preset.FromString(e.Message.CurrentPresetStatus.CurrentPresetData);
        }

        private void MidiIn_ErrorReceived(object? sender, MidiInMessageEventArgs e)
        {
            Console.WriteLine($"[MIDI] Error: {e.RawMessage}");
        }

        private void MidiIn_MessageReceived(object? sender, MidiInMessageEventArgs e)
        {
            switch (e.MidiEvent.CommandCode)
            {
                case MidiCommandCode.ControlChange:
                    var ccEvent = ((ControlChangeEvent)e.MidiEvent);
                    eventCommands[ccEvent.CommandCode][ccEvent.Controller].Invoke(ccEvent.ControllerValue);
                    Console.WriteLine($"[MIDI] {ccEvent.CommandCode}: {ccEvent.Channel}: {ccEvent.Controller}: {ccEvent.ControllerValue}");
                    break;
                default:
                    Console.WriteLine($"[MIDI] {e.MidiEvent.CommandCode}: {e.MidiEvent.Channel}: {e.RawMessage}");
                    break;
            }
        }

        public void Bypass(string nodeId, int value)
        {
            if (amp.IsOpen)
            {
                amp.SetDspUnitParameter(nodeId, new Lib.Model.Preset.DspUnitParameter() { Name = "bypass", Value = value > 64 });
            }
        }

        public void EnableTuner(int value)
        {
            if (amp.IsOpen)
            {
                amp.SetTuner(value > 64);
            }
        }

        public void BypassAll(int value)
        {
            Bypass("stomp", value);
            Bypass("mod", value);
            Bypass("delay", value);
            Bypass("reverb", value);
        }

        public static void ListDevices()
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                Console.WriteLine($"{device} {MidiIn.DeviceInfo(device).ProductName} {MidiIn.DeviceInfo(device).ProductName}");
            }
        }
        public static void StartListening(int deviceId)
        {
            var midiListener = new MidiListener();
            midiListener.Open(deviceId);
        }
    }
}
