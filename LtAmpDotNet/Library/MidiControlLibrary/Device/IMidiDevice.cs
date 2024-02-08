using RtMidi.Net;
using RtMidi.Net.Events;

namespace MidiControlLibrary.Device
{
    public interface IMidiDevice
    {
        uint DeviceId { get; set; }

        void Open();

        void Close();

        static IEnumerable<MidiDeviceInfo> GetDevices()
        {
            return MidiManager.GetAvailableDevices();
        }

        event EventHandler<MidiMessageReceivedEventArgs> MidiMessageReceived;

        void OnMidiMessageReceived(object? sender, MidiMessageReceivedEventArgs e);
    }
}