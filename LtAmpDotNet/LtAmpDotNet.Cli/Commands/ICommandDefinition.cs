using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli.Commands
{
    internal interface ICommandDefinition
    {
        Command CommandDefinition { get; set; }
    }
}
