using LtAmpDotNet.Lib.Extensions.JsonConverters;
using LtAmpDotNet.Lib.Model.Profile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Node
    {

        public Node() { }
        public Node(DspUnitDefinition definition, string nodeId = null)
        {
            FenderId = definition.FenderId;
            NodeId = nodeId ?? definition.Info.SubCategory;
            DspUnitParameters = definition.DefaultDspUnitParameters;
        }

        public Node(string fenderId, string node = null) : this(LtAmpDevice.DspUnitDefinitions.FirstOrDefault(x => x.FenderId == fenderId), node) { }

        [JsonProperty("FenderId")]
        public string FenderId { get; set; }

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("nodeType")]
        public string NodeType => "dspUnit";

        [JsonProperty("dspUnitParameters")]
        [JsonConverter(typeof(DspUnitParameterCollectionConverter))]
        public List<DspUnitParameter> DspUnitParameters { get; set; }

        [JsonIgnore]
        public DspUnitDefinition Definition => LtAmpDevice.DspUnitDefinitions.FirstOrDefault(x => x.FenderId == FenderId);
    }

    public static class NodeType
    {
        public const string NONE = "";
        public const string PRESET = "preset";
        public const string DSP_UNIT = "dspUnit";
    }

    public static class NodeId
    {
        public const string NONE = "";
        public const string PRESET = "preset";
        public const string AMP = "amp";
        public const string STOMP = "stomp";
        public const string MOD = "mod";
        public const string DELAY = "delay";
        public const string REVERB = "reverb";
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
