using LtAmpDotNet.Cli.Commands;
using System.CommandLine;

namespace LtAmpDotNet.Cli
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            RootCommand rootCommand = [];
            rootCommand.AddCommand(new PresetCommandDefinition());
            rootCommand.AddCommand(new FootswitchCommandDefinition());
            rootCommand.AddCommand(new UsbGainCommandDefinition());
            rootCommand.AddCommand(new TerminalCommandDefinition());
            rootCommand.AddCommand(new MidiCommandDefinition());
            await rootCommand.InvokeAsync(args);
        }
    }
}