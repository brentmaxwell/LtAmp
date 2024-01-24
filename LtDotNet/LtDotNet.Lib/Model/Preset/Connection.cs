using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtDotNet.Lib.Model.Preset
{
    public class Connection
    {
        [JsonProperty("input")]
        public InputOutput Input { get; set; }

        [JsonProperty("output")]
        public InputOutput Output { get; set; }
    }

    public class InputOutput
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
    }

}
