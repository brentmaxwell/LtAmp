using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class PresetModel
    {
        public string DisplayName { get; set; }
        public DspUnitModel AmpUnit { get; set; }
        public DspUnitModel StompUnit { get; set; }
        public DspUnitModel ModUnit { get; set; }
        public DspUnitModel DelayUnit { get; set; }
        public DspUnitModel ReverbUnit { get; set; }

        public PresetModel()
        {
        }

        public static PresetModel FromPreset(Preset preset)
        {
            IMapper mapper = (IMapper)App.Current.Services.GetService(typeof(IMapper));
            return mapper.Map<PresetModel>(preset);
        }

    }
}
