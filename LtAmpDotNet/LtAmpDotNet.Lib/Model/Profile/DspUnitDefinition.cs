using Google.Protobuf.WellKnownTypes;
using LtAmpDotNet.Lib.Extensions.JsonConverters;
using LtAmpDotNet.Lib.Model.Preset;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Profile
{
    public class DspUnitDefinition
    {
        [JsonIgnore]
        public string? DisplayName { get => Info?.DisplayName; }

        [JsonProperty("nodeType")]
        public string? NodeType { get; set; }

        [JsonProperty("FenderId")]
        public string? FenderId { get; set; }

        [JsonProperty("defaultDspUnitParameters")]
        [JsonConverter(typeof(DspUnitParameterCollectionConverter))]
        public List<DspUnitParameter>? DefaultDspUnitParameters { get; set; }

        [JsonProperty("info")]
        public DspUnitInfo? Info { get; set; }

        [JsonProperty("ui")]
        public DspUnitUi? Ui { get; set; }

        public Node ToNode()
        {
            return new Node(Info?.SubCategory)
            {
                NodeId = Info?.SubCategory,
                FenderId = FenderId,
                DspUnitParameters = DefaultDspUnitParameters,
            };
        }
    }

    
    

    

    //public class TaperValue
    //{
    //    // si = Logarithmic S Taper
    //    // rs = Logarithmic S Taper
    //    // ri = Exponential Taper
    //    // s = Exponential S Taper
    //    // r = Logarithmic Taper
    //    // i = Logarithmic Taper
    //    // blank = Exponential Taper
    //    // rsi = Exponential S Taper
    //    private float _base = 1.0f;
    //    private float _expscale = 1.0f;
    //    private float _logscale = 1.0f;
    //    public TaperValue(float f)
    //    {
    //        _base = f;
    //        double d = (double)f;
    //        _logscale = (float)(1.0d / Math.Log(d));
    //        _expscale = (float)(1.0d / (d - 1.0d));
    //    }

    //    public float forwardLogarithemicMap(float f)
    //    {
    //        return (float)(((double)_logscale) * Math.Log((((double)f) * (((double)_base) - 1.0d)) + 1.0d));
    //    }

    //    public float forwardExponentialMap(float f)
    //    {
    //        return (float)(((double)_expscale) * (Math.Pow((double)_base, (double)f) - 1.0d));
    //    }

    //    public static Dictionary<String, float> coefficients = new Dictionary<string, float>()
    //    {
    //        {"t10r",1013.99f},
    //        {"t10rs",1013.99f},
    //        {"t10ri",1013.99f},
    //        {"t10rsi",1013.99f},
    //        {"t15r",94.725f},
    //        {"t15rs",94.725f},
    //        {"t15ri",94.725f},
    //        {"t15rsi",94.725f},
    //        {"t10",81.0f},
    //        {"t10s",81.0f},
    //        {"t10i",81.0f},
    //        {"t10si",81.0f},
    //        {"t15",32.111f},
    //        {"t15s",32.111f},
    //        {"t15i",32.111f},
    //        {"t15si",32.111f},
    //        {"t20r",26.61f},
    //        {"t20rs",26.61f},
    //        {"t20ri",26.61f},
    //        {"t20rsi",26.61f},
    //        {"t20",16.0f},
    //        {"t20s",16.0f},
    //        {"t20i",16.0f},
    //        {"t20si",16.0f},
    //        {"t25r",11.4445f},
    //        {"t25rs",11.4445f},
    //        {"t25ri",11.4445f},
    //        {"t25rsi",11.4445f},
    //        {"t25",9.0f},
    //        {"t25s",9.0f},
    //        {"t25i",9.0f},
    //        {"t25si",9.0f},
    //        {"t30r",6.05615f},
    //        {"t30rs",6.05615f},
    //        {"t30ri",6.05615f},
    //        {"t30rsi",6.05615f},
    //        {"t30",5.44445f},
    //        {"t30s",5.44445f},
    //        {"t30i",5.44445f},
    //        {"t30si",5.44445f},
    //        {"t35r",3.5918f},
    //        {"t35rs",3.5918f},
    //        {"t35ri",3.5918f},
    //        {"t35rsi",3.5918f},
    //        {"t35",3.44899f},
    //        {"t35s",3.44899f},
    //        {"t35i",3.44899f},
    //        {"t35si",3.44899f},
    //        {"t45r",2.49585f},
    //        {"t45rs",2.49585f},
    //        {"t45ri",2.49585f},
    //        {"t45rsi",2.49585f},
    //        {"t40r",2.27541f},
    //        {"t40rs",2.27541f},
    //        {"t40ri",2.27541f},
    //        {"t40rsi",2.27541f},
    //        {"t40",2.25f},
    //        {"t40s",2.25f},
    //        {"t40i",2.25f},
    //        {"t40si",2.25f},
    //        {"t45",1.49383f},
    //        {"t45s",1.49383f},
    //        {"t45i",1.49383f},
    //        {"t45si",1.49383f},
    //        {"t50",0.0f}
    //    };
    //}

    

    public static class DspUnitTypes
    {
        public const string AMP = "amp";
        public const string STOMP = "stomp";
        public const string MOD = "mod";
        public const string DELAY = "delay";
        public const string REVERB = "reverb";
        public const string UTILITY = "utility";
    }
}
