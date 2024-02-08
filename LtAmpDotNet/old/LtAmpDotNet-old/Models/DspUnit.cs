using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class DspUnit
    {
        public string DisplayName { get; set; }

        public static DspUnit New(LtAmpDotNet.Lib.Model.Preset.Node unit)
        {
            return new DspUnit()
            {
                DisplayName = unit.Definition.DisplayName,
            };
        }
    }
}
