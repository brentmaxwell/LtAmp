using LtAmpDotNet.Cli.Commands;
using System.CommandLine;

namespace LtAmpDotNet.Cli
{
    internal class Program
    {
        internal static Configuration? Configuration = default;
        private static async Task Main(string[] args)
        {
            Configuration.Load();
            RootCommand rootCommand = [];
            rootCommand.AddCommand(new PresetCommandDefinition());
            rootCommand.AddCommand(new FootswitchCommandDefinition());
            rootCommand.AddCommand(new UsbGainCommandDefinition());
            rootCommand.AddCommand(new TerminalCommandDefinition());
            rootCommand.AddCommand(new MidiCommandDefinition());

            Command config = new Command("--config", "Generate config file in user's home directory");
            config.SetHandler(WriteConfig);
            rootCommand.AddCommand(config);
            await rootCommand.InvokeAsync(args);
        }

        private static void WriteConfig()
        {
            if (Configuration.WriteDefaultConfigToUsersHomeDirectory())
            {
                Console.WriteLine($"Config saved to {Path.Join(Configuration.ConfigurationPath, Configuration.ConfigurationFileName)}");
            }
            else
            {
                Console.Error.WriteLine($"Error saving config to {Path.Join(Configuration.ConfigurationPath, Configuration.ConfigurationFileName)}");
            }
        }
    }
}