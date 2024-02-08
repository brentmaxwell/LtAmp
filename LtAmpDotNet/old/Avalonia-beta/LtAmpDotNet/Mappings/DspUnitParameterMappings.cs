using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models;
using System.Collections.Generic;

namespace LtAmpDotNet.Mappings
{
    public class DspUnitParameterMappings : Profile
    {
        public DspUnitParameterMappings()
        {
            CreateMap<DspUnitUiParameter, DspUnitParameterModel>()
                .ForMember(dest => dest.ControlId, opt => opt.MapFrom(src => src.ControlId))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.ParameterType, opt => opt.MapFrom(src => (Models.Enums.DspUnitParameterType)src.ControlType))
                .ForMember(dest => dest.Min, opt => opt.MapFrom(src => src.Min))
                .ForMember(dest => dest.Max, opt => opt.MapFrom(src => src.Max))
                .ForMember(dest => dest.NumTicks, opt => opt.MapFrom(src => src.NumTicks))
                .ForMember(dest => dest.DisplayMin, opt => opt.MapFrom(src => src.Remap.Min))
                .ForMember(dest => dest.DisplayMax, opt => opt.MapFrom(src => src.Remap.Max))
                .ForMember(dest => dest.ListItems, opt => opt.MapFrom(src => src.ListItems));

            CreateMap<DspUnitParameter, DspUnitParameterModel>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<IEnumerable<DspUnitUiParameter>, DspUnitParameterModelCollection>()
                .ConstructUsing((src, context) => new DspUnitParameterModelCollection(
                    context.Mapper.Map<IEnumerable<DspUnitUiParameter>, IEnumerable<DspUnitParameterModel>>(src)
                    )
                );

            CreateMap<IEnumerable<DspUnitParameter>, DspUnitParameterModelCollection>()
                .ConstructUsing((src, context) => new DspUnitParameterModelCollection(
                    context.Mapper.Map<IEnumerable<DspUnitParameter>, IEnumerable<DspUnitParameterModel>>(src)
                    )
                );
        }
    }
}
