using LtAmpDotNet.Lib.Model.Preset;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class PresetCommandsDefinition
    {
        internal Command CommandDefinition { get; set; }
        internal PresetCommandsDefinition()
        {
            var presetIndexArgument = new Argument<int>("bank", "Index of the preset bank");
            var presetNameArgument = new Argument<string>("name", "Name of preset");
            var filenameOption = new Option<string>("--file", "Filename to parse");

            var presetCommand = new Command("preset", "Presets");

            var presetGetCommand = new Command("get", "Get Presets");
            presetGetCommand.AddArgument(presetIndexArgument);
            presetGetCommand.AddOption(filenameOption);
            presetGetCommand.SetHandler(PresetGet, presetIndexArgument, filenameOption);
            presetCommand.AddCommand(presetGetCommand);

            var presetSetCommand = new Command("set", "Set Presets");
            presetSetCommand.AddArgument(presetIndexArgument);
            presetSetCommand.AddOption(filenameOption);
            presetSetCommand.SetHandler(PresetSet, presetIndexArgument, filenameOption);
            presetCommand.AddCommand(presetSetCommand);

            var presetRenameCommand = new Command("rename", "Rename preset");
            presetRenameCommand.AddArgument(presetIndexArgument);
            presetRenameCommand.AddArgument(presetNameArgument);
            presetRenameCommand.SetHandler(PresetRename, presetIndexArgument, presetNameArgument);
            presetCommand.AddCommand(presetRenameCommand);

            var presetSwapCommand = new Command("swap", "Swap presets");
            presetSwapCommand.AddArgument(presetIndexArgument);
            presetSwapCommand.AddArgument(presetIndexArgument);
            presetSwapCommand.SetHandler(PresetSwap, presetIndexArgument, presetIndexArgument);
            presetCommand.AddCommand(presetSwapCommand);

            var presetShiftCommand = new Command("shift", "Shift presets");
            presetShiftCommand.AddArgument(presetIndexArgument);
            presetShiftCommand.AddArgument(presetIndexArgument);
            presetShiftCommand.SetHandler(PresetSwap, presetIndexArgument, presetIndexArgument);
            presetCommand.AddCommand(presetShiftCommand);

            var presetClearCommand = new Command("clear", "Clear preset");
            presetClearCommand.AddArgument(presetIndexArgument);

            CommandDefinition = presetCommand;
        }
        internal void PresetGet(int presetBankIndex, string? filename = null)
        {
            var wait = new AutoResetEvent(false);
            string outputData = "";
            Program.amp.PresetJSONMessageReceived += (sender, eventArgs) =>
            {
                outputData = Preset.FromString(eventArgs.Message.PresetJSONMessage.Data).ToString();
                wait.Set();
            };
            Program.amp.GetPreset(presetBankIndex);
            wait.WaitOne(TimeSpan.FromSeconds(5));
            if (filename == null)
            {
                Console.WriteLine(outputData);
            }
            else
            {
                File.WriteAllText(filename, outputData);
            }
        }

        internal void PresetSet(int presetBankIndex, string filename)
        {
            var inputData = File.ReadAllText(filename);
            var preset = Preset.FromString(inputData);
            var wait = new AutoResetEvent(false);
            Program.amp.PresetSavedStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.PresetSavedStatus.Slot}: {eventArgs.Message.PresetSavedStatus.Name}");
                wait.Set();
            };
            Program.amp.SavePresetAs(presetBankIndex, preset);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal void PresetRename(int presetBankIndex, string newName)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.PresetSavedStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.PresetSavedStatus.Slot}: {eventArgs.Message.PresetSavedStatus.Name}");
                wait.Set();
            };
            Program.amp.RenamePresetAt(presetBankIndex, newName);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal void PresetSwap(int presetBankIndexA, int presetBankIndexB)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.SwapPresetStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.SwapPresetStatus.IndexA}: {eventArgs.Message.SwapPresetStatus.IndexB}");
                wait.Set();
            };
            Program.amp.SwapPreset(new int[] { presetBankIndexA, presetBankIndexB });
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal void PresetShift(int presetBankIndexA, int presetBankIndexB)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.ShiftPresetStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.ShiftPresetStatus.IndexToShiftFrom}: {eventArgs.Message.ShiftPresetStatus.IndexToShiftTo}");
                wait.Set();
            };
            Program.amp.ShiftPreset(presetBankIndexA, presetBankIndexB);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal void PresetClear(int presetBankIndex)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.ClearPresetStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.ClearPreset.SlotIndex}");
                wait.Set();
            };
            Program.amp.ClearPreset(presetBankIndex);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }
    }
}
