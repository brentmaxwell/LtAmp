using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Device;
using System.CommandLine;

namespace LtAmpDotNet.Cli.Commands
{
    internal abstract class BaseCommandDefinition : Command, IDisposable
    {
        internal ILtAmplifier? Amp;

        protected BaseCommandDefinition(string name, string? description = null) : base(name, description)
        {
        }

        internal virtual async Task OpenAmp()
        {
            Amp = new LtAmplifier(new UsbAmpDevice());
            await Amp.OpenAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}