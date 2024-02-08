using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class DspUnitModel
    {
        public NodeIdType NodeId { get; set; }
        public List<string> DspUnits { get; set; }
        public string DisplayName { get; set; }
        public string FenderId { get; set; }

        public static DspUnitModel FromNode(Node node)
        {
            IMapper mapper = (IMapper)App.Current.Services.GetService(typeof(IMapper));
            return mapper.Map<DspUnitModel>(node);
        }

        public static DspUnitModel FromDefition(DspUnitDefinition definition)
        {
            IMapper mapper = (IMapper)App.Current.Services.GetService(typeof(IMapper));
            return mapper.Map<DspUnitModel>(definition);
        }
    }
}
