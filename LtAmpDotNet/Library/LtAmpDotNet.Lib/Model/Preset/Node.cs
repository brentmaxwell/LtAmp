using LtAmpDotNet.Lib.Extensions.JsonConverters;
using LtAmpDotNet.Lib.Model.Profile;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Node
    {
        /// <summary>Creates a new node for the specified node type</summary>
        /// <param name="nodeId"></param>
        /// <returns>A new node object for the specified type</returns>
        public static Node Create(NodeIdType nodeId)
        {
            return nodeId == NodeIdType.amp
                ? Node.FromString("{\"dspUnitParameters\":{\"cabsimType\":\"none\",\"treb\":\".5\",\"mid\": 0.5,\"volume\": 0,\"gateDetectorPosition\":\"jack\",\"gain\":0.5,\"gatePreset\":\"off\",\"bass\":0.5},\"FenderId\":\"DUBS_LinearGain\",\"nodeType\":\"dspUnit\",\"nodeId\":\"amp\"}")
                : Node.FromString($"{{\"nodeId\":\"{nodeId}\",\"nodeType\":\"dspUnit\",\"FenderId\":\"DUBS_Passthru\",\"dspUnitParameters\":{{\"bypass\": false,\"bypassType\":\"Post\"}}}}");
        }

        public Node(DspUnitDefinition definition, NodeIdType nodeId)
        {
            FenderId = definition.FenderId;
            NodeId = nodeId;
            DspUnitParameters = definition?.DefaultDspUnitParameters;
        }

        public Node()
        {
        }

        public Node(string fenderId, NodeIdType node) : this(LtAmplifier.DspUnitDefinitions?.FirstOrDefault(x => x.FenderId == fenderId)!, node)
        {
        }

        [JsonProperty("FenderId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1507:Use nameof to express symbol names", Justification = "Json property is not specificaly tied to the variable name; it refers to the name in the JSON file.")]
        public string? FenderId { get; set; }

        [JsonProperty("nodeId")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeIdType NodeId { get; set; }

        [JsonProperty("nodeType")]
        public string? NodeType => "dspUnit";

        [JsonProperty("dspUnitParameters")]
        [JsonConverter(typeof(DspUnitParameterCollectionConverter))]
        public List<DspUnitParameter>? DspUnitParameters { get; set; }

        [JsonIgnore]
        public DspUnitDefinition Definition => LtAmplifier.DspUnitDefinitions?.FirstOrDefault(x => x.FenderId == FenderId)!;

        public static Node? FromString(string json)
        {
            return JsonConvert.DeserializeObject<Node>(json);
        }

        public string ToString(Formatting jsonFormatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(this, jsonFormatting);
        }
    }

    /// <summary>Represents the type of node (amp, stomp, mod, delay, or reverb)</summary>
    public enum NodeIdType
    {
        none = 0,
        amp = 1,
        stomp = 2,
        mod = 3,
        delay = 4,
        reverb = 5
    }
}