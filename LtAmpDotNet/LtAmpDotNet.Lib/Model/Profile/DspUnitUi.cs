using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Profile
{
    public class DspUnitUi
    {
        [JsonProperty("hasBypass")]
        public bool HasBypass { get; set; }

        [JsonProperty("inputs")]
        public IEnumerable<DspUnitInputOutput>? Inputs { get; set; }

        [JsonProperty("outputs")]
        public IEnumerable<DspUnitInputOutput>? Outputs { get; set; }

        [JsonProperty("uiParameters")]
        public IEnumerable<DspUnitUiParameter>? UiParameters { get; set; }
    }
}
