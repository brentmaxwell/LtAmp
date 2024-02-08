using LtAmpDotNet.Base;
using LtAmpDotNet.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class Preset
    {
        public Guid? PresetId { get; set; }
        public int? SlotIndex { get; set; }
        public string DisplayName { get; set; }
        public DspUnit Amp { get; set; }
        public DspUnit Stomp { get; set; }
        public DspUnit Mod { get; set; }
        public DspUnit Delay {  get; set; }
        public DspUnit Reverb { get; set; }

        public static Preset New(LtAmpDotNet.Lib.Model.Preset.Preset preset, int index = 0)
        {
            return new Preset()
            {
                DisplayName = preset.FormattedDisplayName,
                SlotIndex = index,
                PresetId = preset.Info.PresetId,
                Amp = DspUnit.New(preset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == Lib.Model.Preset.NodeIdType.amp)),
                Stomp = DspUnit.New(preset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == Lib.Model.Preset.NodeIdType.stomp)),
                Mod = DspUnit.New(preset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == Lib.Model.Preset.NodeIdType.mod)),
                Delay = DspUnit.New(preset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == Lib.Model.Preset.NodeIdType.delay)),
                Reverb = DspUnit.New(preset.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == Lib.Model.Preset.NodeIdType.reverb))
            };
        }
    }
}
