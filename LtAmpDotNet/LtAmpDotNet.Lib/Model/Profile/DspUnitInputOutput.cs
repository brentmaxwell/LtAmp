using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
