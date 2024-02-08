using RtMidi.Net;
using RtMidi.Net.Enums;
using System;

namespace LtAmpDotNet.Services.Midi
{
    public class MidiDevice : IEquatable<MidiDeviceInfo>
    {
        public string? Name { get; set; }
        public uint Port { get; set; }
        public MidiApi Api { get; set; }
        public MidiDeviceType Type { get; set; }

        public bool Equals(MidiDeviceInfo? other)
        {
            return Name == other?.Name && Port == other?.Port && Api == other?.Api && Type == other?.Type;
        }

        public override bool Equals(object? obj)
        {
            MidiDevice? other = obj as MidiDevice;
            return Name == other?.Name && Port == other?.Port && Api == other?.Api && Type == other?.Type;
        }

        public static implicit operator MidiDevice(MidiDeviceInfo info)
        {
            return new()
            {
                Name = info.Name,
                Port = info.Port,
                Api = info.Api,
                Type = info.Type
            };
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}