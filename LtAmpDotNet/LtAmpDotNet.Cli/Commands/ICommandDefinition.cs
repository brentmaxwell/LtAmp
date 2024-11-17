using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal interface ICommandDefinition
    {
        Command CommandDefinition { get; set; }
    }
}