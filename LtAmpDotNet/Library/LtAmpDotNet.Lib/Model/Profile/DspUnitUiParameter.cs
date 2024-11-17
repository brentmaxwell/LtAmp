using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LtAmpDotNet.Lib.Model.Profile
{
    public class DspUnitUiParameter
    {
        [JsonProperty("controlType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ControlType? ControlType { get; set; }

        [JsonProperty("paramGuiObjectNameMinimized")]
        public string? ParamGuiObjectNameMinimized { get; set; }

        [JsonProperty("paramGuiObjectNameMaximized")]
        public string? ParamGuiObjectNameMaximized { get; set; }

        [JsonProperty("controlId")]
        public string? ControlId { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("numTicks")]
        public int NumTicks { get; set; }

        [JsonProperty("displayType")]
        public string? DisplayType { get; set; }

        [JsonProperty("min")]
        public float? Min { get; set; }

        [JsonProperty("max")]
        public float? Max { get; set; }

        [JsonProperty("taper")]
        public string? Taper { get; set; }

        [JsonProperty("listItems")]
        public IEnumerable<string>? ListItems { get; set; }

        [JsonProperty("remap")]
        public DspUnitUiParametersRemap? Remap { get; set; }
    }

    /// <summary>The type of data used by the parameter</summary>
    public enum ControlType
    {
        /// <summary>A control with a continuous range (a number)</summary>
        continuous = 1,

        /// <summary>A control with distinct values</summary>
        list = 2,

        /// <summary>An on/off control</summary>
        listBool = 3,
    }
}