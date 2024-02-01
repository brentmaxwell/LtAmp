using LtAmpDotNet.Lib;
using LtAmpDotNet.Tests.Mock;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Cli.Commands
{
    internal abstract class BaseCommandDefinition
    {
        internal virtual Command CommandDefinition {  get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        internal LtAmplifier? Amp;

        internal virtual void Open()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Write($"Connecting to device {0}...");
            }
            Amp = new LtAmplifier(new MockHidDevice(), false);
            WaitForEvent(() => { Amp.Open(); }, handler => Amp.AmplifierConnected += handler, 5);
        }

        /// <summary>Executes the command, and waits for the event to respond before continuing.</summary>
        /// <param name="action">the action to run</param>
        /// <param name="eventHandler">the event to wait for</param>
        /// <param name="waitTime">timeout (in seconds)</param>
        internal static EventArgs? WaitForEvent(Action action, Action<EventHandler> eventHandler, int waitTime = 5)
        {
            EventArgs? returnVal = null;
            var wait = new AutoResetEvent(false);
            eventHandler((sender, eventArgs) => {
                returnVal = eventArgs;
                wait.Set();
            });
            action.Invoke();
            wait.WaitOne(TimeSpan.FromSeconds(waitTime));
            return returnVal;
        }

        /// <summary>Executes the command, and waits for the event to respond before continuing.</summary>
        /// <param name="action">the action to run</param>
        /// <param name="eventHandler">the event to wait for</param>
        /// <param name="waitTime">timeout (in seconds)</param>
        internal static T? WaitForEvent<T>(Action action, Action<EventHandler<T>> eventHandler, int waitTime = 5)
        {
            T? returnVal = default;
            var wait = new AutoResetEvent(false);
            eventHandler((sender, eventArgs) => {
                returnVal = eventArgs;
                wait.Set();
            });
            action.Invoke();
            wait.WaitOne(TimeSpan.FromSeconds(waitTime));
            return returnVal;
        }
    }
}

