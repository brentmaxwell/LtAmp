using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class FootswitchCommandDefinition
    {
        internal Command CommandDefinition { get; set; }

        internal FootswitchCommandDefinition()
        {
            var presetIndexArgument = new Argument<uint>("bank", "Index of the preset bank");
            var qaCommand = new Command("qa", "Footswitch");

            var qaGetCommand = new Command("get", "Get footswitch preset indexes");
            qaGetCommand.SetHandler(FootswitchGet);
            qaCommand.AddCommand(qaGetCommand);

            var qaSetCommand = new Command("set", "Set footswitch preset indexes");
            qaSetCommand.AddArgument(presetIndexArgument);
            qaSetCommand.AddArgument(presetIndexArgument);
            qaSetCommand.SetHandler(FootswitchSet, presetIndexArgument, presetIndexArgument);
            qaCommand.AddCommand(qaSetCommand);

            CommandDefinition = qaCommand;
        }

        internal void FootswitchGet()
        {
            var wait = new AutoResetEvent(false);
            Program.amp.QASlotsStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"[{eventArgs.Message.QASlotsStatus.Slots[0]}],[{eventArgs.Message.QASlotsStatus.Slots[1]}]");
                wait.Set();
            };
            Program.amp.GetQASlots();
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal void FootswitchSet(uint presetBankIndexA, uint presetBankIndexB)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.QASlotsStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"[{eventArgs.Message.QASlotsStatus.Slots[0]}],[{eventArgs.Message.QASlotsStatus.Slots[1]}]");
                wait.Set();
            };
            uint[] slots = new uint[] { presetBankIndexA, presetBankIndexB };
            Program.amp.SetQASlots(slots);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }
    }
}