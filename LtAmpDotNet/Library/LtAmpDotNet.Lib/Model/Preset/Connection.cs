using Newtonsoft.Json;

namespace LtAmpDotNet.Lib.Model.Preset
{
    /// <summary>A connection among the parts of the preset</summary>
    public class Connection
    {
        /// <summary>The input part of the connection</summary>
        [JsonProperty("input")]
        public InputOutput? Input { get; set; }

        /// <summary>The output part of the connection</summary>
        [JsonProperty("output")]
        public InputOutput? Output { get; set; }
    }

    /// <summary>An object in a connection</summary>
    public class InputOutput
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("nodeId")]
        public string? NodeId { get; set; }
    }
}