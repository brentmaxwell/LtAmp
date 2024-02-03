using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class FootswitchCommandDefinition : BaseCommandDefinition
    {
        internal FootswitchCommandDefinition() : base("qa", "Footswitch")
        {
            Argument<uint> presetIndexArgumentA = new("bankA", "Index of the preset bank");
            Argument<uint> presetIndexArgumentB = new("bankB", "Index of the preset bank");

            Command qaGetCommand = new("get", "Get footswitch preset indexes");
            qaGetCommand.SetHandler(FootswitchGet);
            AddCommand(qaGetCommand);

            Command qaSetCommand = new("set", "Set footswitch preset indexes");
            qaSetCommand.AddArgument(presetIndexArgumentA);
            qaSetCommand.AddArgument(presetIndexArgumentB);
            qaSetCommand.SetHandler(FootswitchSet, presetIndexArgumentA, presetIndexArgumentB);
            AddCommand(qaSetCommand);
        }

        internal async Task FootswitchGet()
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.QASlotsStatus result = await Amp.GetQASlotsAsync();
                Console.WriteLine($"[{result.Slots[0]}],[{result.Slots[1]}]");
            }
        }

        internal async Task FootswitchSet(uint presetBankIndexA, uint presetBankIndexB)
        {
            await Open();
            if (Amp != null)
            {
                uint[] slots = [presetBankIndexA, presetBankIndexB];
                Lib.Models.Protobuf.QASlotsStatus result = await Amp.SetQASlotsAsync(slots);
                Console.WriteLine($"[{result.Slots[0]}],[{result.Slots[1]}]");
            }
        }
    }
}