using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Models;
using System.Linq;

namespace LtAmpDotNet.Mappings
{
    public class PresetModelMappings : AutoMapper.Profile
    {
        public PresetModelMappings()
        {
            CreateMap<Preset, PresetModel>()
                //.ConstructUsing(src => new PresetModel())
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FormattedDisplayName))
                .ForMember(dest => dest.AmpUnit, opt => opt.MapFrom(src => src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.amp)))
                .ForMember(dest => dest.StompUnit, opt => opt.MapFrom(src => src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.stomp)))
                .ForMember(dest => dest.ModUnit, opt => opt.MapFrom(src => src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.mod)))
                .ForMember(dest => dest.DelayUnit, opt => opt.MapFrom(src => src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.delay)))
                .ForMember(dest => dest.ReverbUnit, opt => opt.MapFrom(src => src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.reverb)))
                .ForMember(dest => dest.DspUnits, opt => opt.Ignore());
        }
    }
}
