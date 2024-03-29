﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class AudioGraph
    {
        [JsonProperty("Connections")]
        public ICollection<Connection>? Connections { get; set; }

        [JsonProperty("nodes")]
        public ICollection<Node>? Nodes { get; set; }
    }
}
