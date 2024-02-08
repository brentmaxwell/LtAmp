using net.thebrent.dotnet.helpers.Collections;

namespace LtAmpDotNet.Models
{
    public class DspUnitModelCollection : ObservableKeyedCollection<string, DspUnitModel>
    {
        public DspUnitModelCollection() : base(x => x.FenderId)
        {
        }
    }
}