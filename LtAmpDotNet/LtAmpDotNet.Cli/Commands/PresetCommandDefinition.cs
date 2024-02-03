using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Events;
using LtAmpDotNet.Lib.Model.Preset;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class PresetCommandDefinition : BaseCommandDefinition
    {
        internal PresetCommandDefinition() : base("preset", "Presets")
        {
            Argument<string> presetNameArgument = new("name", "Name of preset");

            Argument<int> presetIndexArgument = new("bank", "Index of the preset bank");
            Argument<int> presetIndexArgumentA = new("bankA", "Index of the preset bank");
            Argument<int> presetIndexArgumentB = new("bankB", "Index of the preset bank");


            Option<string> filenameOption = new("--file", "Filename to parse");
            filenameOption.LegalFileNamesOnly();

            Command presetGetCommand = new("get", "Get Presets");
            presetGetCommand.AddArgument(presetIndexArgument);
            presetGetCommand.AddOption(filenameOption);
            presetGetCommand.SetHandler(PresetGet, presetIndexArgument, filenameOption);
            AddCommand(presetGetCommand);

            Command presetSetCommand = new("set", "Set Presets");
            presetSetCommand.AddArgument(presetIndexArgument);
            presetSetCommand.AddOption(filenameOption);
            presetSetCommand.SetHandler(PresetSet, presetIndexArgument, filenameOption);
            AddCommand(presetSetCommand);

            Command presetRenameCommand = new("rename", "Rename preset");
            presetRenameCommand.AddArgument(presetIndexArgument);
            presetRenameCommand.AddArgument(presetNameArgument);
            presetRenameCommand.SetHandler(PresetRename, presetIndexArgument, presetNameArgument);
            AddCommand(presetRenameCommand);

            Command presetSwapCommand = new("swap", "Swap presets");
            presetSwapCommand.AddArgument(presetIndexArgumentA);
            presetSwapCommand.AddArgument(presetIndexArgumentB);
            presetSwapCommand.SetHandler(PresetSwap, presetIndexArgumentA, presetIndexArgumentB);
            AddCommand(presetSwapCommand);

            Command presetShiftCommand = new("shift", "Shift presets");
            presetShiftCommand.AddArgument(presetIndexArgumentA);
            presetShiftCommand.AddArgument(presetIndexArgumentB);
            presetShiftCommand.SetHandler(PresetSwap, presetIndexArgumentA, presetIndexArgumentB);
            AddCommand(presetShiftCommand);

            Command presetClearCommand = new("clear", "Clear preset");
            presetClearCommand.AddArgument(presetIndexArgument);
            AddCommand(presetClearCommand);
        }

        internal async Task PresetGet(int presetBankIndex, string? filename = null)
        {
            await Open();
            if (Amp != null)
            {
                string outputData = "";
                Lib.Models.Protobuf.PresetJSONMessage result = await Amp.GetPresetAsync(presetBankIndex);
                FenderMessageEventArgs? eventArgs = LtAmplifier.WaitForEvent<FenderMessageEventArgs>(() => Amp.GetPreset(presetBankIndex), handler => Amp.PresetJSONMessageReceived += handler, 5);
                outputData = Preset.FromString(result.Data)!.ToString();
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

        internal async Task PresetSet(int presetBankIndex, string filename)
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
                        await Open();
                        if (Amp != null)
                        {
                            Lib.Models.Protobuf.PresetSavedStatus result = await Amp.SavePresetAsAsync(presetBankIndex, preset);
                            Console.WriteLine($"{result.Slot}: {result.Name}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing {filename}: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading {filename}: {ex.Message}");
            }
        }

        internal async Task PresetRename(int presetBankIndex, string newName)
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.PresetSavedStatus result = await Amp.RenamePresetAtAsync(presetBankIndex, newName);
                Console.WriteLine($"{result.Slot}: {result.Name}");
            }
        }

        internal async Task PresetSwap(int presetBankIndexA, int presetBankIndexB)
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.SwapPresetStatus result = await Amp.SwapPresetAsync(presetBankIndexA, presetBankIndexB);
                Console.WriteLine($"{result.IndexA}: {result.IndexB}");
            }
        }

        internal async Task PresetShift(int presetBankIndexA, int presetBankIndexB)
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.ShiftPresetStatus result = await Amp.ShiftPresetAsync(presetBankIndexA, presetBankIndexB);
                Console.WriteLine($"{result.IndexToShiftFrom}: {result.IndexToShiftTo}");
            }

        }

        internal async Task PresetClear(int presetBankIndex)
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.ClearPresetStatus result = await Amp.ClearPresetAsync(presetBankIndex);
                Console.WriteLine($"{result.SlotIndex}");
            }
        }
    }
}
