using LtAmpDotNet.Lib.Events;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class FootswitchCommandDefinition : BaseCommandDefinition
    {
        internal override Command CommandDefinition { get; set; }

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
            Open();
            if(Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(Amp.GetQASlots, handler => Amp.QASlotsStatusMessageReceived += handler, 5);
                Console.WriteLine($"[{eventArgs?.Message?.QASlotsStatus.Slots[0]}],[{eventArgs?.Message?.QASlotsStatus.Slots[1]}]");
            }
        }

        internal void FootswitchSet(uint presetBankIndexA, uint presetBankIndexB)
        {
            Open();
            if (Amp != null)
            {
                uint[] slots = [presetBankIndexA, presetBankIndexB];
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.SetQASlots(slots), handler => Amp.QASlotsStatusMessageReceived += handler, 5);
                Console.WriteLine($"[{eventArgs?.Message?.QASlotsStatus.Slots[0]}],[{eventArgs?.Message?.QASlotsStatus.Slots[1]}]");
            }
        }
    }
}