using LtAmpDotNet.Cli.Commands;
using LtAmpDotNet.Lib;
using System.CommandLine;

namespace LtAmpDotNet.Cli
{
    internal class Program
    {
        internal static LtAmplifier amp;
        static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand();
            rootCommand.AddCommand(new PresetCommandsDefinition().CommandDefinition);
            rootCommand.AddCommand(new FootswitchCommandDefinition().CommandDefinition);
            rootCommand.AddCommand(new UsbGainCommandDefinition().CommandDefinition);

            var midiDeviceArgument = new Argument<int>(
                name: "deviceId",
                description: "MIDI deviceId",
                getDefaultValue: () => 0
            );
            var midiCommand = new Command("midi", "Listen to MIDI mesages");
            midiCommand.AddArgument(midiDeviceArgument);
            midiCommand.SetHandler(MidiListener.StartListening, midiDeviceArgument);
            var midiListCommand = new Command("list", "List midi devices");
            midiListCommand.SetHandler(MidiListener.ListDevices);
            midiCommand.AddCommand(midiListCommand);

            rootCommand.AddCommand(midiCommand);

            var wait = new AutoResetEvent(false);
            amp = new LtAmplifier();
            amp.AmplifierConnected += (sender, eventArgs) => {
                wait.Set();
            };
            amp.Open();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            await rootCommand.InvokeAsync(args);
        }

    }
}