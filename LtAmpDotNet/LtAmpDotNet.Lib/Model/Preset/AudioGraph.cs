using Newtonsoft.Json;

namespace LtAmpDotNet.Lib.Model.Preset
{
    /// <summary>Represents the audio setup of the preset</summary>
    public class AudioGraph
    {
        /// <summary>The connections among the parts of the preset</summary>
        [JsonProperty("connections")]
        public ICollection<Connection> Connections { get; set; }

        /// <summary>The DSP units and amp for the preset</summary>
        [JsonProperty("nodes")]
        public ICollection<Node> Nodes { get; set; }
    }
}
