using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Mappings
{
    public class DspUnitModelMappings : Profile
    {
        public DspUnitModelMappings()
        {
            CreateMap<Node, DspUnitModel>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Definition.DisplayName))
                .ForMember(dest => dest.NodeId, opt => opt.MapFrom(src => src.NodeId))
                .ForMember(dest => dest.FenderId, opt => opt.MapFrom(src => src.FenderId));

            CreateMap<DspUnitDefinition, DspUnitModel>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.NodeId, opt => opt.MapFrom(src => TryParseNodeIdType(src.Info.SubCategory)))
                .ForMember(dest => dest.FenderId, opt => opt.MapFrom(src => src.FenderId));
        }

        public NodeIdType TryParseNodeIdType(string value)
        {
            NodeIdType type;
            return Enum.TryParse(value, out type) ? type : NodeIdType.none;
        }
    }
}
