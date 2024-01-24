using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Preset
    {
        [JsonProperty("data")]
        public string? Raw { get; set; }

        [JsonProperty("audioGraph")]
        public AudioGraph? AudioGraph { get; set; }

        [JsonProperty("info")]
        public Info? Info { get; set; }

        [JsonProperty("nodeId")]
        public string? NodeId { get; set; }

        [JsonProperty("nodeType")]
        public string? NodeType { get; set; }

        [JsonProperty("numInputs")]
        public int? NumOfInputs { get; set; }

        [JsonProperty("numOutputs")]
        public int? NumOfOutputs { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonIgnore]
        public string[] DisplayName => Info?.DisplayName!;

        [JsonIgnore]
        public string? FormattedDisplayName => Info?.FormattedDisplayName!;

        [JsonIgnore]
        public string? TwoLineDisplayName
        {
            get
            {
                return Info?.DisplayName[0].Trim() + "\n" + Info?.DisplayName[1].Trim();
            }
        }

        public static Preset? FromString(string json)
        {
            return JsonConvert.DeserializeObject<Preset>(json);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
