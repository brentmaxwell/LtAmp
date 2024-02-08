using net.thebrent.dotnet.helpers.Collections;
using System.Collections.Generic;

namespace LtAmpDotNet.Models
{
    public class PresetModelCollection : ObservableKeyedCollection<int, PresetModel>
    {
        public PresetModelCollection() : base(x => x.PresetIndex)
        {
        }

        public PresetModelCollection(IEnumerable<PresetModel> enumerable) : this()
        {
            enumerable.ForEach(Add);
        }

        public static implicit operator PresetModelCollection(List<PresetModel> enumerable)
        {
            return new(enumerable);
        }
    }
}