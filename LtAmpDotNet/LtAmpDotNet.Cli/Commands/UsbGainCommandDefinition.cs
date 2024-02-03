using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class UsbGainCommandDefinition : BaseCommandDefinition
    {
        internal UsbGainCommandDefinition() : base("gain", "USB Gain")
        {
            Argument<float> usbGainArgument = new("value", "USB Gain value (dB)");

            Command usabGainGetCommand = new("get", "Get footswitch preset indexes");
            usabGainGetCommand.SetHandler(UsbGainGet);
            AddCommand(usabGainGetCommand);

            Command usbGainSetCommand = new("set", "Set footswitch preset indexes");
            usbGainSetCommand.AddArgument(usbGainArgument);
            usbGainSetCommand.SetHandler(UsbGainSet, usbGainArgument);
            AddCommand(usbGainSetCommand);
        }

        internal async Task UsbGainGet()
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.UsbGainStatus result = await Amp.GetUsbGainAsync();
                Console.WriteLine($"{result.ValueDB}");
            }
        }

        internal async void UsbGainSet(float gainValue)
        {
            await Open();
            if (Amp != null)
            {
                Lib.Models.Protobuf.UsbGainStatus result = await Amp.SetUsbGainAsync(gainValue);
                Console.WriteLine($"{result.ValueDB}");
            }
        }
    }
}