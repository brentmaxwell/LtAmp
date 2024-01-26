using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Panels.DspUnitControlViews
{
    public interface IDspUnitControlView
    {
        Node Node { get; set; }
        event EventHandler ChangesApplied;
    }
}
