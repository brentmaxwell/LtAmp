using LtAmpDotNet.Lib.Extensions.JsonConverters;
using LtAmpDotNet.Lib.Model.Profile;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Node
    {
        public static Node Create(NodeIdType nodeId) {
            if (nodeId == NodeIdType.amp)
            {
                return Node.FromString("{\"dspUnitParameters\":{\"cabsimType\":\"none\",\"treb\":\".5\",\"mid\": 0.5,\"volume\": 0,\"gateDetectorPosition\":\"jack\",\"gain\":0.5,\"gatePreset\":\"off\",\"bass\":0.5},\"FenderId\":\"DUBS_LinearGain\",\"nodeType\":\"dspUnit\",\"nodeId\":\"amp\"}");
            }
            else
            {
                return Node.FromString("{\"nodeId\":\"mod\",\"nodeType\":\"dspUnit\",\"FenderId\":\"DUBS_Passthru\",\"dspUnitParameters\":{\"bypass\": false,\"bypassType\":\"Post\"}}");
            }
            
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

        public Node(string fenderId, NodeIdType node) : this(LtAmplifier.DspUnitDefinitions?.FirstOrDefault(x => x.FenderId == fenderId)!, node) { }

        [JsonProperty("FenderId")]
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

        public override string ToString()
        {
            return ToString(Formatting.None);
        }

        public string ToString(Formatting jsonFormatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(this, jsonFormatting);
        }
    }

    public static class NodeType
    {
        public const string NONE = "";
        public const string PRESET = "preset";
        public const string DSP_UNIT = "dspUnit";
    }

    //public static class NodeIds
    //{
    //    public const string NONE = "";
    //    public const string PRESET = "preset";
    //    public const string AMP = "amp";
    //    public const string STOMP = "stomp";
    //    public const string MOD = "mod";
    //    public const string DELAY = "delay";
    //    public const string REVERB = "reverb";
    //}

    public enum NodeIdType
    {
        none = 0,
        amp = 1,
        stomp = 2,
        mod = 3,
        delay = 4,
        reverb = 5
    }

    public enum FenderId
    {
        // amp
        DBUS_LinearGain,  //SUPER CLEAN
        DUBS_Champ57,     //CHAMP
        DUBS_Deluxe57,    //DELUXE DIRT
        DUBS_Twin57,	  //50S TWIN
        DUBS_Bassman59,   //BASSMAN
        DUBS_Princeton65, //PRINCETON
        DUBS_Deluxe65,    //DELUXE CLN
        DUBS_Twin65,      //TWIN CLEAN
        DUBS_Excelsior,   //EXCELSIOR
        DUBS_Silvertone,  //SMALLTONE
        DUBS_DR103,       //70S UK CLN
        DUBS_Ac30Tb,      //60S UK CLN
        DUBS_Plexi87,     //70S ROCK
        DUBS_Jcm800,      //80S ROCK
        DUBS_Or120,       //DOOM METAL
        DUBS_SuperSonic,  //BURN
        DUBS_Rect2,       //90S ROCK
        DUBS_MetalRect2,  //ALT METAL
        DUBS_Evh3,        //METAL 2000
        DUBS_MetalEvh3,   //SUPER HEAVY

        // stomp

        // mod

        // delay

        // reverb
    }
}
