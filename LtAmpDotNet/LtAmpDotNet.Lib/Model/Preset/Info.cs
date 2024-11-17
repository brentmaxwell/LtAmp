using Newtonsoft.Json;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Info
    {
        [JsonProperty("author")]
        public string? Author { get; set; }

        [JsonProperty("bpm")]
        public int? BPM { get; set; }

        [JsonProperty("created_at")]
        public int? CreatedAt { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayNameRaw { get; set; }

        [JsonIgnore]
        public string[] DisplayName
        {
            get => Enumerable.Range(0, DisplayNameRaw!.Length / 8).Select(i => DisplayNameRaw.Substring(i * 8, 8).Trim()).ToArray();
            set
            {
                if (value.Length == 2)
                {
                    DisplayNameRaw = value[0].PadRight(8) + value[1].PadRight(8);
                }
            }
        }

        [JsonIgnore]
        public string FormattedDisplayName => string.Join(" ", DisplayName);

        [JsonProperty("is_factory_default")]
        public bool? IsFactoryDefault { get; set; }

        [JsonProperty("preset_id")]
        public Guid? PresetId { get; set; }

        [JsonProperty("product_id")]
        public string? ProductId { get; set; }

        [JsonProperty("source_id")]
        public string? SourceId { get; set; }

        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }
    }
}
