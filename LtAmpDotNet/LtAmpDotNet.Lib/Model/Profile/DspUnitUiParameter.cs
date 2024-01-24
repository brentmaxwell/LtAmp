using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Profile
{
    public class DspUnitUiParameter
    {
        [JsonProperty("controlType")]
        public string ControlType { get; set; }

        [JsonProperty("paramGuiObjectNameMinimized")]
        public string ParamGuiObjectNameMinimized { get; set; }

        [JsonProperty("paramGuiObjectNameMaximized")]
        public string ParamGuiObjectNameMaximized { get; set; }

        [JsonProperty("controlId")]
        public string ControlId { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("numTicks")]
        public int NumTicks { get; set; }

        [JsonProperty("displayType")]
        public string DisplayType { get; set; }

        [JsonProperty("min")]
        public float Min { get; set; }

        [JsonProperty("max")]
        public float Max { get; set; }

        [JsonProperty("taper")]
        public string Taper { get; set; }

        [JsonProperty("listItems")]
        public IEnumerable<string> ListItems { get; set; }

        [JsonProperty("remap")]
        public DspUnitUiParametersRemap Remap { get; set; }
    }
    public static class ControlType
    {
        public const string CONTINUOUS = "continuous";
        public const string LIST = "list";
        public const string LIST_BOOL = "listBool";
    }
}
