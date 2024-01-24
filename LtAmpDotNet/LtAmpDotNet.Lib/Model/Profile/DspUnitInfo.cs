﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Profile
{
    public class DspUnitInfo
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("audioGuiObjectNameMinimized")]
        public string AudioGuiObjectNameMinimized { get; set; }

        [JsonProperty("audioGuiObjectNameMaximized")]
        public string AudioGuiObjectNameMaximized { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("subcategory")]
        public string SubCategory { get; set; }
    }
}
