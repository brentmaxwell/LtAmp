using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtDotNet.Lib.Model.Preset
{
    public class AudioGraph : ObservableAmpData, INotifyPropertyChanged
    {
        [JsonProperty("Connections")]
        public ICollection<Connection> Connections { get; set; }

        [JsonProperty("nodes")]
        public ICollection<Node> Nodes { get; set; }
    }
}
