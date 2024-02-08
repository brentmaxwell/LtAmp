using LtAmpDotNet.Lib.Model.Preset;
using net.thebrent.dotnet.helpers.Collections;

namespace LtAmpDotNet.Models
{
    public class DspUnitParameterModelCollection : ObservableKeyedCollection<string, DspUnitParameterModel>
    {
        public DspUnitParameterModelCollection() : base(x => x.ControlId!)
        {
        }

        public DspUnitParameterModelCollection(NodeIdType dspUnitType)
            : this()
        {
            DspUnitType = dspUnitType;
        }

        public NodeIdType DspUnitType { get; set; }

        public DspUnitParameterModelCollection Clone()
        {
            DspUnitParameterModelCollection clone = new(DspUnitType);
            this.ForEach((item) => clone.Add(item.Clone()));
            return clone;
        }
    }
}