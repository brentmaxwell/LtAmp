using LtAmpDotNet.Lib.Model.Preset;
using net.thebrent.dotnet.helpers.Collections;
using System;

namespace LtAmpDotNet.Models
{
    public class DspUnitModelDefinitions : ObservableDictionary<NodeIdType, DspUnitModelCollection>
    {
        public DspUnitModelDefinitions()
        {
            Enum.GetValues(typeof(NodeIdType))
                        .ForEach<NodeIdType>(x => Add(x, []));
        }
    }
}