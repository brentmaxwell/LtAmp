using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Preset
    {
        [JsonProperty("audioGraph")]
        public AudioGraph? AudioGraph { get; set; }

        [JsonProperty("info")]
        public Info? Info { get; set; }

        [JsonProperty("nodeId")]
        public string? NodeId { get; set; } = "preset";

        [JsonProperty("nodeType")]
        public string? NodeType { get; set; } = "preset";

        [JsonProperty("numInputs")]
        public int? NumOfInputs { get; set; } = 2;

        [JsonProperty("numOutputs")]
        public int? NumOfOutputs { get; set; } = 2;

        [JsonProperty("version")]
        public string Version { get; set; } = "1.1";

        [JsonIgnore]
        public string[] DisplayName {
            get => Info?.DisplayName!;
            set => Info.DisplayName = value;
        }

        [JsonIgnore]
        public string FormattedDisplayName => Info?.FormattedDisplayName!;

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

        public static Preset Create(string json = "{\"info\":{\"displayName\":\"CLEAN\",\"product_id\":\"mustang-lt\",\"author\":\"\",\"timestamp\":1510855005,\"created_at\":0,\"bpm\":0,\"preset_id\":\"82701e3e-caf7-11e7-b721-171e6c7d3090\",\"source_id\":\"\",\"is_factory_default\":true},\"nodeType\":\"preset\",\"audioGraph\":{\"connections\":[{\"input\":{\"index\":0,\"nodeId\":\"preset\"},\"output\":{\"index\":0,\"nodeId\":\"stomp\"}},{\"input\":{\"index\":1,\"nodeId\":\"preset\"},\"output\":{\"index\":1,\"nodeId\":\"stomp\"}},{\"input\":{\"index\":0,\"nodeId\":\"stomp\"},\"output\":{\"index\":0,\"nodeId\":\"mod\"}},{\"input\":{\"index\":1,\"nodeId\":\"stomp\"},\"output\":{\"index\":1,\"nodeId\":\"mod\"}},{\"input\":{\"index\":0,\"nodeId\":\"mod\"},\"output\":{\"index\":0,\"nodeId\":\"amp\"}},{\"input\":{\"index\":1,\"nodeId\":\"mod\"},\"output\":{\"index\":1,\"nodeId\":\"amp\"}},{\"input\":{\"index\":0,\"nodeId\":\"amp\"},\"output\":{\"index\":0,\"nodeId\":\"delay\"}},{\"input\":{\"index\":1,\"nodeId\":\"amp\"},\"output\":{\"index\":1,\"nodeId\":\"delay\"}},{\"input\":{\"index\":0,\"nodeId\":\"delay\"},\"output\":{\"index\":0,\"nodeId\":\"reverb\"}},{\"input\":{\"index\":1,\"nodeId\":\"delay\"},\"output\":{\"index\":1,\"nodeId\":\"reverb\"}},{\"input\":{\"index\":0,\"nodeId\":\"reverb\"},\"output\":{\"index\":0,\"nodeId\":\"preset\"}},{\"input\":{\"index\":1,\"nodeId\":\"reverb\"},\"output\":{\"index\":1,\"nodeId\":\"preset\"}}],\"nodes\":[{\"dspUnitParameters\":{\"cabsimType\":\"none\",\"treb\":.5,\"mid\":0.5,\"volume\":0,\"gateDetectorPosition\":\"jack\",\"gain\":0.5,\"gatePreset\":\"off\",\"bass\":0.5},\"FenderId\":\"DUBS_LinearGain\",\"nodeType\":\"dspUnit\",\"nodeId\":\"amp\"},{\"dspUnitParameters\":{\"bypassType\":\"Post\",\"bypass\":false},\"FenderId\":\"DUBS_Passthru\",\"nodeType\":\"dspUnit\",\"nodeId\":\"stomp\"},{\"dspUnitParameters\":{\"bypassType\":\"Post\",\"bypass\":false},\"FenderId\":\"DUBS_Passthru\",\"nodeType\":\"dspUnit\",\"nodeId\":\"mod\"},{\"dspUnitParameters\":{\"bypassType\":\"Post\",\"bypass\":false},\"FenderId\":\"DUBS_Passthru\",\"nodeType\":\"dspUnit\",\"nodeId\":\"delay\"},{\"dspUnitParameters\":{\"bypassType\":\"Post\",\"bypass\":false},\"FenderId\":\"DUBS_Passthru\",\"nodeType\":\"dspUnit\",\"nodeId\":\"reverb\"}]},\"nodeId\":\"preset\",\"version\":\"1.1\",\"numOutputs\":2,\"numInputs\":2}") => FromString(json)!;

        public override string ToString()
        {
            return ToString(Formatting.None);
        }

        public string ToString(Formatting jsonFormatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(this, jsonFormatting);
        }

        public Preset()
        {

        }
    }
}
