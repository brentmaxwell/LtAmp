using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Tests;
using LtAmpDotNet.Tests.Mock;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.CommandLine;

namespace LtAmpDotNet.Cli
{
    internal class Program
    {
        internal static LtAmpDevice amp;
        private static MockDeviceState mockDeviceState = MockDeviceState.Load();
        static async Task Main(string[] args)
        {
            var presetIndexArgument = new Argument<int>("bank", "Index of the preset bank");
            var presetNameArgument = new Argument<string>("name", "Name of preset");
            var filenameOption = new Option<string>("--file", "Filename to parse");
            
            var rootCommand = new RootCommand();
            
            var presetCommand = new Command("preset", "Presets");
            rootCommand.AddCommand(presetCommand);
            
            var presetGetCommand = new Command("get", "Get Presets");
            presetGetCommand.AddArgument(presetIndexArgument);
            presetGetCommand.AddOption(filenameOption);
            presetGetCommand.SetHandler(PresetCommands.PresetGet, presetIndexArgument, filenameOption);
            presetCommand.AddCommand(presetGetCommand);
            
            var presetSetCommand = new Command("set", "Set Presets");
            presetSetCommand.AddArgument(presetIndexArgument);
            presetSetCommand.AddOption(filenameOption);
            presetSetCommand.SetHandler(PresetCommands.PresetSet, presetIndexArgument, filenameOption);
            presetCommand.AddCommand(presetSetCommand);

            var presetRenameCommand = new Command("rename", "Rename preset");
            presetRenameCommand.AddArgument(presetIndexArgument);
            presetRenameCommand.AddArgument(presetNameArgument);
            presetRenameCommand.SetHandler(PresetCommands.PresetRename, presetIndexArgument, presetNameArgument);
            presetCommand.AddCommand(presetRenameCommand);

            var presetSwapCommand = new Command("swap", "Swap presets");
            presetSwapCommand.AddArgument(presetIndexArgument);
            presetSwapCommand.AddArgument(presetIndexArgument);
            presetSwapCommand.SetHandler(PresetCommands.PresetSwap, presetIndexArgument, presetIndexArgument);
            presetCommand.AddCommand(presetSwapCommand);

            var presetShiftCommand = new Command("shift", "Shift presets");
            presetShiftCommand.AddArgument(presetIndexArgument);
            presetShiftCommand.AddArgument(presetIndexArgument);
            presetShiftCommand.SetHandler(PresetCommands.PresetSwap, presetIndexArgument, presetIndexArgument);
            presetCommand.AddCommand(presetShiftCommand);

            var presetClearCommand = new Command("clear", "Clear preset");
            presetClearCommand.AddArgument(presetIndexArgument);

            var qaCommand = new Command("qa", "Footswitch");
            rootCommand.AddCommand(qaCommand);

            var qaSetCommand = new Command("set", "Set footswitch presets");
            qaSetCommand.AddArgument(presetIndexArgument);
            qaSetCommand.AddArgument(presetIndexArgument);
            qaCommand.AddCommand(qaSetCommand);


            //var gainCommand = new Command("gain", "USB Gain");
            //rootCommand.Add(presetIndexArgument);
            //rootCommand.SetHandler(Preset_Get, presetIndexArgument);
            var wait = new AutoResetEvent(false);
            amp = new LtAmpDevice(new MockHidDevice(mockDeviceState));
            amp.DeviceConnected += (sender, eventArgs) => {
                wait.Set();
            };
            amp.Open();
            wait.WaitOne(TimeSpan.FromSeconds(5));
            rootCommand.InvokeAsync(args);
            
        }
    }
}