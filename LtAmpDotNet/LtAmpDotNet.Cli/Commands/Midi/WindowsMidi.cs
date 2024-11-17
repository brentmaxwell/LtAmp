using LtAmpDotNet.Cli.Commands.Midi.Interfaces;
using LtAmpDotNet.Lib.Models.Protobuf;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli.Commands.Midi
{
    internal class WindowsMidi : IMidiSystem
    {
        public event EventHandler<IMidiMessage> MessageReceived;

        public List<IMidiDevice> ListDevices()
        {
            List<IMidiDevice> devices = new List<IMidiDevice>();
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                var nDevice = MidiIn.DeviceInfo(device);
                devices.Add(new WindowsMidiDevice() { Id = device, Name = nDevice.ProductName });
            }
            return devices;
        }
        public void Start(int deviceId)
        {
            if (deviceId + 1 > MidiIn.NumberOfDevices)
            {
                Console.Error.WriteLine($"Error: No such midi device (device {deviceId})");
                return;
            }
            else
            {
                MidiIn midiIn = new(deviceId);
                midiIn.MessageReceived += MidiIn_OnMessageReceived1;
                midiIn.Start();
            }
        }

        private void MidiIn_OnMessageReceived1(object? sender, MidiInMessageEventArgs e)
        {
            Interfaces.MidiMessage message = new Interfaces.MidiMessage()
            {
                CommandType = (MidiMessageType)e.MidiEvent.CommandCode,
                MidiData = BitConverter.GetBytes(e.MidiEvent.GetAsShortMessage())
            };
            MessageReceived?.Invoke(sender, message);
        }
    }
}
