using net.thebrent.dotnet.helpers.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LtAmpDotNet.Models
{
    public class MidiCommandDefinitionsModel : Dictionary<MidiCommandType, int?>
    {
        public MidiCommandDefinitionsModel()
        {
            Enum.GetValues(typeof(MidiCommandType)).ForEach<MidiCommandType>(x => Add(x, null));
        }

        public Dictionary<int, MidiCommandType> SwapKeysValues()
        {
            return this.Where(x => x.Value.HasValue).ToDictionary(x => x.Value.Value, x => x.Key);
        }
    }

    public enum MidiCommandType
    {
        BypassStomp = 1,
        BypassMod = 2,
        BypassDelay = 3,
        BypassReverb = 4,
    }
}