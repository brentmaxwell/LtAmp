using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Preset;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class PresetCommandDefinition : BaseCommandDefinition
    {
        internal override Command CommandDefinition { get; set; }
        internal PresetCommandDefinition()
        {
            var presetNameArgument = new Argument<string>("name", "Name of preset");

            var presetIndexArgument = new Argument<int>("bank", "Index of the preset bank");
            presetIndexArgument.FromAmong(Enumerable.Range(1, LtAmplifier.NUM_OF_PRESETS).Select(x => x.ToString()).ToArray());

            var filenameOption = new Option<string>("--file", "Filename to parse");
            filenameOption.LegalFileNamesOnly();

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
            Open();
            if(Amp != null)
            {
                string outputData = "";
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.GetPreset(presetBankIndex), handler => Amp.PresetJSONMessageReceived += handler, 5);
                outputData = Preset.FromString(eventArgs!.Message!.PresetJSONMessage.Data)!.ToString();
                if (filename == null)
                {
                    Console.WriteLine(outputData);
                }
                else
                {
                    File.WriteAllText(filename, outputData);
                }
            }
        }

        internal void PresetSet(int presetBankIndex, string filename)
        {
            string inputData;
            Preset preset;
            try
            {
                inputData = File.ReadAllText(filename);
                try
                {
                    preset = Preset.FromString(inputData)!;
                    if (preset != null)
                    {
                        Open();
                        if(Amp != null)
                        {
                            FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.SavePresetAs(presetBankIndex, preset), handler => Amp.PresetSavedStatusMessageReceived += handler, 5);
                            Console.WriteLine($"{eventArgs?.Message?.PresetSavedStatus.Slot}: {eventArgs?.Message?.PresetSavedStatus.Name}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing {filename}: {ex.Message}");
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Error loading {filename}: {ex.Message}");
            }
        }

        internal void PresetRename(int presetBankIndex, string newName)
        {
            Open();
            if(Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.RenamePresetAt(presetBankIndex, newName), handler => Amp.PresetSavedStatusMessageReceived += handler, 5);
                Console.WriteLine($"{eventArgs?.Message?.PresetSavedStatus.Slot}: {eventArgs?.Message?.PresetSavedStatus.Name}");
            }
        }

        internal void PresetSwap(int presetBankIndexA, int presetBankIndexB)
        {
            Open();
            if (Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.SwapPreset([presetBankIndexA, presetBankIndexB]), handler => Amp.SwapPresetStatusMessageReceived += handler, 5);
                Console.WriteLine($"{eventArgs?.Message?.SwapPresetStatus.IndexA}: {eventArgs?.Message?.SwapPresetStatus.IndexB}");
            }
        }

        internal void PresetShift(int presetBankIndexA, int presetBankIndexB)
        {
            Open();
            if(Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.ShiftPreset(presetBankIndexA, presetBankIndexB), handler => Amp.ShiftPresetStatusMessageReceived += handler, 5);
                Console.WriteLine($"{eventArgs?.Message?.ShiftPresetStatus.IndexToShiftFrom}: {eventArgs?.Message?.ShiftPresetStatus.IndexToShiftTo}");
            }
            
        }

        internal void PresetClear(int presetBankIndex)
        {
            Open();
            if (Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.ClearPreset(presetBankIndex), handler => Amp.ClearPresetStatusMessageReceived += handler, 5);
                Console.WriteLine($"{eventArgs?.Message?.ClearPreset.SlotIndex}");
            }
        }
    }
}
