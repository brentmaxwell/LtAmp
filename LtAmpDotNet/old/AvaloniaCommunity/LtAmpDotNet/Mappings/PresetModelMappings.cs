using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models;
using LtAmpDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Mappings
{
    public class PresetModelMappings : AutoMapper.Profile
    {
        public PresetModelMappings()
        {
            CreateMap<Preset, PresetModel>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FormattedDisplayName))
                .ForMember(dest => dest.AmpUnit, opt => opt.MapFrom(src => GetModel(src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.amp))))
                .ForMember(dest => dest.StompUnit, opt => opt.MapFrom(src => GetModel(src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.stomp))))
                .ForMember(dest => dest.ModUnit, opt => opt.MapFrom(src => GetModel(src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.mod))))
                .ForMember(dest => dest.DelayUnit, opt => opt.MapFrom(src => GetModel(src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.delay))))
                .ForMember(dest => dest.ReverbUnit, opt => opt.MapFrom(src => GetModel(src.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.reverb))));

        }

        public DspUnitModel GetModel(Node node)
        {
            var lists = (DspUnitLists)App.Current.Services.GetService(typeof(DspUnitLists));
            switch (node.NodeId)
            {
                case NodeIdType.amp:
                    return lists.AmpUnits.SingleOrDefault(x => x.FenderId == node.FenderId);
                    break;
                case NodeIdType.stomp:
                    return lists.StompUnits.SingleOrDefault(x => x.FenderId == node.FenderId);
                    break;
                case NodeIdType.mod:
                    return lists.ModUnits.SingleOrDefault(x => x.FenderId == node.FenderId);
                    break;
                case NodeIdType.delay:
                    return lists.DelayUnits.SingleOrDefault(x => x.FenderId == node.FenderId);
                    break;
                case NodeIdType.reverb:
                    return lists.ReverbUnits.SingleOrDefault(x => x.FenderId == node.FenderId);
                    break;
            }
            return null;
        } 
    }
}
