using LtAmpDotNet.Models;
using RtMidi.Net;
using RtMidi.Net.Clients;
using RtMidi.Net.Enums;
using System.Collections.Generic;

namespace LtAmpDotNet.Services.Midi
{
    public interface IMidiService
    {
        MidiApi Api { get; set; }
        Dictionary<MidiDeviceInfo, MidiInputClient> MidiDevices { get; set; }

        List<MidiDeviceInfo> ListDevices();

        void SetCommands(Dictionary<MidiCommandType, int?>? commands);

        void OpenDevice(MidiDevice device);

        void CloseDevice(MidiDevice device);

        void OpenAll();

        void OpenAll(List<MidiDevice> devices);

        void CloseAll();

        void AddDevice(MidiDevice device);
    }
}