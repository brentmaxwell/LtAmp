using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.DataModels
{
    public class PresetViewModel
    {
        public Guid PresetId { get; set; }
        public string DisplayName { get; set; }
        public int BeatsPerMinute { get; set; }
        public Dictionary<string, NodeViewModel> Nodes { get; set; }
    }
}
