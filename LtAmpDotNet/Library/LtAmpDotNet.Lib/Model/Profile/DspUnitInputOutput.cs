using Newtonsoft.Json;

namespace LtAmpDotNet.Lib.Model.Profile
{
    public class DspUnitInputOutput
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
    }
}