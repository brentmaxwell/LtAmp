using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli
{
    internal static class PresetCommands
    {
        internal static async Task PresetGet(int presetBankIndex, string filename = null)
        {
            var wait = new AutoResetEvent(false);
            string outputData = "";
            Program.amp.PresetJSONMessageReceived += (message) =>
            {
                outputData = Preset.FromString(message.Data).ToString();
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

        internal static async Task PresetSet(int presetBankIndex, string filename)
        {
            var inputData = File.ReadAllText(filename);
            var preset = Preset.FromString(inputData);
            var wait = new AutoResetEvent(false);
            Program.amp.PresetSavedStatusMessageReceived += (message) =>
            {
                Console.WriteLine($"{message.Slot}: {message.Name}");
                wait.Set();
            };
            Program.amp.SavePresetAs(presetBankIndex, preset);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal static async Task PresetRename(int presetBankIndex, string newName)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.PresetSavedStatusMessageReceived += (message) =>
            {
                Console.WriteLine($"{message.Slot}: {message.Name}");
                wait.Set();
            };
            Program.amp.RenamePresetAt(presetBankIndex, newName);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal static async Task PresetSwap(int presetBankIndexA, int presetBankIndexB)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.SwapPresetStatusMessageReceived += (message) =>
            {
                Console.WriteLine($"{message.IndexA}: {message.IndexB}");
                wait.Set();
            };
            Program.amp.SwapPreset(presetBankIndexA, presetBankIndexB);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal static async Task PresetShift(int presetBankIndexA, int presetBankIndexB)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.ShiftPresetStatusMessageReceived += (message) =>
            {
                Console.WriteLine($"{message.IndexToShiftFrom}: {message.IndexToShiftTo}");
                wait.Set();
            };
            Program.amp.ShiftPreset(presetBankIndexA, presetBankIndexB);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal static async Task PresetClear(int presetBankIndex)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.ClearPresetStatusMessageReceived += (message) =>
            {
                Console.WriteLine($"{message.SlotIndex}");
                wait.Set();
            };
            Program.amp.ClearPreset(presetBankIndex);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }
    }
}
