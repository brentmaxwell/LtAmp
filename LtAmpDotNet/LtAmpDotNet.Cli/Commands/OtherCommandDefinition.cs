using Google.Protobuf;
using LtAmpDotNet.Lib.Models.Protobuf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli.Commands
{
    internal class OtherCommandDefinition : BaseCommandDefinition
    {
        internal override Command CommandDefinition { get; set; }
        internal OtherCommandDefinition()
        {
            var terminalCommand = new Command("term", "Terminal");
            terminalCommand.SetHandler(Terminal);
            CommandDefinition = terminalCommand;
        }

        internal void Terminal()
        {
            Open();
            if(Amp != null)
            { 
                IMessage definition = (IMessage)Activator.CreateInstance(typeof(FenderMessageLT))!;
                string? input;
                Amp.MessageReceived += Amp_MessageReceived;
                while ((input = Console.ReadLine()) != null)
                {
                    var message = (FenderMessageLT)JsonParser.Default.Parse(input, definition?.Descriptor);
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
