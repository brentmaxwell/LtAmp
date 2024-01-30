using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class UsbGainCommandDefinition
    {
        internal Command CommandDefinition { get; set; }

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
            var wait = new AutoResetEvent(false);
            Program.amp.UsbGainStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.UsbGainStatus.ValueDB}");
                wait.Set();
            };
            Program.amp.GetUsbGain();
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }

        internal void UsbGainSet(float gainValue)
        {
            var wait = new AutoResetEvent(false);
            Program.amp.UsbGainStatusMessageReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.Message.UsbGainStatus.ValueDB}");
                wait.Set();
            };
            Program.amp.SetUsbGain(gainValue);
            wait.WaitOne(TimeSpan.FromSeconds(5));
        }
    }
}