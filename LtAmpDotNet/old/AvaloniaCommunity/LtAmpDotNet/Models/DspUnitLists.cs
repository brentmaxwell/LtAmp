using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class DspUnitLists
    {
        public List<DspUnitModel> AmpUnits { get; set; }
        public List<DspUnitModel> StompUnits { get; set; }
        public List<DspUnitModel> ModUnits { get; set; }
        public List<DspUnitModel> DelayUnits { get; set; }
        public List<DspUnitModel> ReverbUnits { get; set; }

        public DspUnitLists(IMapper mapper)
        {
            AmpUnits = new List<DspUnitModel>();
            StompUnits = new List<DspUnitModel>();
            ModUnits = new List<DspUnitModel>();
            DelayUnits = new List<DspUnitModel>();
            ReverbUnits = new List<DspUnitModel>();
            foreach (var dspUnit in LtAmplifier.DspUnitDefinitions)
            {
                switch (dspUnit.Info.SubCategory)
                {
                    case "utility":
                        var model = mapper.Map<DspUnitModel>(dspUnit);
                        model.NodeId = NodeIdType.amp;
                        AmpUnits.Add(model);
                        model = mapper.Map<DspUnitModel>(dspUnit);
                        model.NodeId = NodeIdType.stomp;
                        StompUnits.Add(model);
                        model = mapper.Map<DspUnitModel>(dspUnit);
                        model.NodeId = NodeIdType.mod;
                        ModUnits.Add(model);
                        model = mapper.Map<DspUnitModel>(dspUnit);
                        model.NodeId = NodeIdType.delay;
                        DelayUnits.Add(model);
                        model = mapper.Map<DspUnitModel>(dspUnit);
                        model.NodeId = NodeIdType.reverb;
                        ReverbUnits.Add(model);
                        break;
                    case "amp":
                        var ampModel = mapper.Map<DspUnitModel>(dspUnit);
                        AmpUnits.Add(ampModel);
                        break;
                    case "stomp":
                        var stompModel = mapper.Map<DspUnitModel>(dspUnit);
                        StompUnits.Add(stompModel);
                        break;
                    case "mod":
                        var modModel = mapper.Map<DspUnitModel>(dspUnit);
                        ModUnits.Add(modModel);
                        break;
                    case "delay":
                        var delayModel = mapper.Map<DspUnitModel>(dspUnit);
                        DelayUnits.Add(delayModel);
                        break;
                    case "reverb":
                        var reverbModel = mapper.Map<DspUnitModel>(dspUnit);
                        ReverbUnits.Add(reverbModel);
                        break;
                }
            }
        }
    }
}
