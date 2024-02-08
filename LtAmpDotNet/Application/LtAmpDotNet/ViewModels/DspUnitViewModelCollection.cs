using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Models;
using net.thebrent.dotnet.helpers.Collections;
using System;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitViewModelCollection : ObservableDictionary<NodeIdType, DspUnitViewModel>
    {
        public DspUnitViewModelCollection(DspUnitModelDefinitions definitions)
        {
            Enum.GetValues(typeof(NodeIdType))
                        .ForEach<NodeIdType>(x => Add(x, new DspUnitViewModel(x, definitions[x])));
        }

        public DspUnitViewModel AmpUnit => this[NodeIdType.amp];
        public DspUnitViewModel StompUnit => this[NodeIdType.stomp];
        public DspUnitViewModel ModUnit => this[NodeIdType.mod];
        public DspUnitViewModel DelayUnit => this[NodeIdType.delay];
        public DspUnitViewModel ReverbUnit => this[NodeIdType.reverb];
    }
}