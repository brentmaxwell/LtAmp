using LtAmpDotNet.Base;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services.Messages;
using net.thebrent.dotnet.helpers;
using net.thebrent.dotnet.helpers.Collections;
using RtMidi.Net;
using RtMidi.Net.Clients;
using RtMidi.Net.Enums;
using RtMidi.Net.Events;
using System.Collections.Generic;
using System.Linq;

namespace LtAmpDotNet.Services.Midi
{
    public class MidiService : ObservableModel, IMidiService
    {
        #region Constructors

        public MidiService(MidiApi api)
        {
            Api = api;
            MidiDevices = [];
            IsActive = true;
            MidiCommands = [];
            MidiCommandDefinitions = new() {
                { MidiCommandType.BypassStomp, (x) => Send(new ParameterChangedMessage(Lib.Model.Preset.NodeIdType.stomp, "bypass", x > 127)) },
                { MidiCommandType.BypassMod, (x) => Send(new ParameterChangedMessage(Lib.Model.Preset.NodeIdType.mod, "bypass", x > 127)) },
                { MidiCommandType.BypassDelay, (x) => Send(new ParameterChangedMessage(Lib.Model.Preset.NodeIdType.delay, "bypass", x > 127)) },
                { MidiCommandType.BypassReverb, (x) => Send(new ParameterChangedMessage(Lib.Model.Preset.NodeIdType.reverb, "bypass", x > 127)) },
            };
        }

        #endregion Constructors

        #region Fields and properties

        public MidiApi Api { get; set; }
        public Dictionary<MidiDeviceInfo, MidiInputClient> MidiDevices { get; set; }

        public ExecutableDictionary<int, dynamic> MidiCommands { get; set; }
        public MidiCommandDictionary MidiCommandDefinitions { get; set; }

        #endregion Fields and properties

        #region Event Methods

        private void OnMidiMessageReceived(object? sender, MidiMessageReceivedEventArgs e)
        {
            MidiMessageControlChange command = (MidiMessageControlChange)e.Message;
            MidiCommands[command.ControlFunction]?.Invoke(command.Value);
        }

        #endregion Event Methods

        #region Methods

        public void SetCommands(Dictionary<MidiCommandType, int?>? commands)
        {
            MidiCommands.Clear();
            commands?.Where(x => x.Value.HasValue).ForEach(x =>
            {
                MidiCommands.Add(x.Value!.Value, MidiCommandDefinitions[x.Key]);
            });
        }

        public List<MidiDeviceInfo> ListDevices()
        {
            return MidiManager.GetAvailableDevices(Api).Where(x => x.Type == RtMidi.Net.Enums.MidiDeviceType.Input).ToList();
        }

        public void OpenDevice(MidiDevice device)
        {
            MidiDeviceInfo midiDeviceInfo = MidiManager.GetDeviceInfo(device.Port, device.Type, device.Api);
            MidiDevices.TryGetValue(midiDeviceInfo, out MidiInputClient? midiInputClient);
            midiInputClient ??= new MidiInputClient(midiDeviceInfo);
            midiInputClient.OnMessageReceived += OnMidiMessageReceived;
            midiInputClient.ActivateMessageReceivedEvent();
            midiInputClient.Open();
            MidiDevices.Add(midiDeviceInfo, midiInputClient);
        }

        public void OpenAll(List<MidiDevice> devices)
        {
            //devices.ForEach(x => OpenDevice(x));
        }

        public void OpenAll()
        {
            foreach (KeyValuePair<MidiDeviceInfo, MidiInputClient> item in MidiDevices)
            {
                OpenDevice(item.Key);
            }
        }

        public void CloseDevice(MidiDevice device)
        {
            KeyValuePair<MidiDeviceInfo, MidiInputClient> midiClient = MidiDevices.SingleOrDefault(x => x.Key.Name == device.Name && x.Key.Type == device.Type && x.Key.Api == device.Api);
            midiClient.Value.Close();
        }

        public void CloseAll()
        {
            foreach (KeyValuePair<MidiDeviceInfo, MidiInputClient> item in MidiDevices)
            {
                CloseDevice(item.Key);
            }
        }

        public void AddDevice(MidiDevice device)
        {
            MidiDeviceInfo deviceInfo = MidiManager.GetDeviceInfo(device.Port, device.Type, device.Api);
            MidiInputClient midiInputClient = new(deviceInfo);
            midiInputClient.OnMessageReceived += OnMidiMessageReceived;
            midiInputClient.ActivateMessageReceivedEvent();
            midiInputClient.Open();
            if (MidiDevices.SingleOrDefault(x => x.Key.Name == device.Name && x.Key.Type == device.Type && x.Key.Api == device.Api).Key == null)
            {
                MidiDevices.Add(deviceInfo, midiInputClient);
            }
        }

        public void Remove(MidiDevice device)
        {
            KeyValuePair<MidiDeviceInfo, MidiInputClient> midiClient = MidiDevices.SingleOrDefault(x => x.Key.Name == device.Name && x.Key.Type == device.Type && x.Key.Api == device.Api);
            midiClient.Value.Close();
            midiClient.Value.Dispose();
            MidiDevices.Remove(midiClient.Key);
        }

        #endregion Methods
    }
}