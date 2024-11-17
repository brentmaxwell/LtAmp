using Google.Protobuf;
using LtAmpDotNet.Lib.Models.Protobuf;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal class TerminalCommandDefinition : BaseCommandDefinition
    {
        internal TerminalCommandDefinition() : base("term", "Terminal")
        {
            Command terminalCommand = new("term", "Terminal");
            terminalCommand.SetHandler(Terminal);
        }

        internal async Task Terminal()
        {
            await OpenAmp();
            if (Amp != null)
            {
                IMessage definition = (IMessage)Activator.CreateInstance(typeof(FenderMessageLT))!;
                string? input;
                Amp.MessageReceived += Amp_MessageReceived;
                while ((input = Console.ReadLine()) != null)
                {
                    FenderMessageLT message = (FenderMessageLT)JsonParser.Default.Parse(input, definition?.Descriptor);
                    Amp.SendMessage(message);
                }
            }
        }

        internal void Amp_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
