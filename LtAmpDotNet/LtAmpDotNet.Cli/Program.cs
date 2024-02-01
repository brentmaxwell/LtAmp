using LtAmpDotNet.Cli.Commands;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using System.CommandLine;

namespace LtAmpDotNet.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand();
            rootCommand.AddCommand(new PresetCommandDefinition().CommandDefinition);
            rootCommand.AddCommand(new FootswitchCommandDefinition().CommandDefinition);
            rootCommand.AddCommand(new UsbGainCommandDefinition().CommandDefinition);
            rootCommand.AddCommand(new OtherCommandDefinition().CommandDefinition);
            rootCommand.AddCommand(new MidiCommandDefinition().CommandDefinition);
            await rootCommand.InvokeAsync(args);
        }
    }
}