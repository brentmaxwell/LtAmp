using LtAmpDotNet.Lib.Events;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class UsbGainCommandDefinition : BaseCommandDefinition
    {
        internal override Command CommandDefinition { get; set; }

        internal UsbGainCommandDefinition()
        {
            var usbGainArgument = new Argument<float>("value", "USB Gain value (dB)");
            var usbGainCommand = new Command("gain", "USB Gain");

            var usabGainGetCommand = new Command("get", "Get footswitch preset indexes");
            usabGainGetCommand.SetHandler(UsbGainGet);
            usbGainCommand.AddCommand(usabGainGetCommand);

            var usbGainSetCommand = new Command("set", "Set footswitch preset indexes");
            usbGainSetCommand.AddArgument(usbGainArgument);
            usbGainSetCommand.SetHandler(UsbGainSet, usbGainArgument);
            usbGainCommand.AddCommand(usbGainSetCommand);

            CommandDefinition = usbGainCommand;
        }

        internal void UsbGainGet()
        {
            Open();
            if(Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(Amp.GetUsbGain, handler => Amp.UsbGainStatusMessageReceived += handler, 5);
                Console.WriteLine($"{eventArgs?.Message?.UsbGainStatus.ValueDB}");
            }
        }

        internal void UsbGainSet(float gainValue)
        {
            Open();
            if (Amp != null)
            {
                FenderMessageEventArgs? eventArgs = WaitForEvent<FenderMessageEventArgs>(() => Amp.SetUsbGain(gainValue), handler => Amp.UsbGainStatusMessageReceived += handler, 5);
                Console.WriteLine($"{eventArgs?.Message?.UsbGainStatus.ValueDB}");
            }
        }
    }
}