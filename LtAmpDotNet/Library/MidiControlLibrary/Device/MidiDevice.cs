using RtMidi.Net;
using RtMidi.Net.Clients;
using RtMidi.Net.Enums;
using RtMidi.Net.Events;

namespace MidiControlLibrary.Device
{
    public class MidiDevice : IMidiDevice
    {
        public MidiApi PlatformApiType { get; set; }

        private uint _deviceId;

        public uint DeviceId
        {
            get => _deviceId;
            set
            {
                _deviceId = value;
                Device = MidiManager.GetDeviceInfo(_deviceId, MidiDeviceType.Input, PlatformApiType);
            }
        }

        public MidiInputClient Client { get; set; }

        private MidiDeviceInfo _device;

        public MidiDeviceInfo Device
        {
            get => _device;
            set
            {
                _device = value;
                Client = new MidiInputClient(_device);
                Client.OnMessageReceived += Client_OnMessageReceived;
            }
        }

        public event EventHandler<MidiMessageReceivedEventArgs> MidiMessageReceived;

        public void Open()
        {
            Client.ActivateMessageReceivedEvent();
            Client.Open();
        }

        public void Close()
        {
            Client.Close();
        }

        public void OnMidiMessageReceived(object? sender, MidiMessageReceivedEventArgs e)
        {
            MidiMessageReceived?.Invoke(sender, e);
        }

        private void Client_OnMessageReceived(object? sender, RtMidi.Net.Events.MidiMessageReceivedEventArgs e)
        {
            OnMidiMessageReceived(sender, e);
        }

        public MidiDevice(uint deviceId, MidiApi platformType = MidiApi.Unspecified)
        {
            DeviceId = deviceId;
            PlatformApiType = platformType;
        }
    }
}