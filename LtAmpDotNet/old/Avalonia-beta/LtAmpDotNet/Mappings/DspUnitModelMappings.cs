using AutoMapper;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models;
using LtAmpDotNet.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LtAmpDotNet.Mappings
{
    public class DspUnitModelMappings : Profile
    {
        public DspUnitModelMappings()
        {
            CreateMap<DspUnitDefinition, DspUnitModel>()
                .ForMember(dest => dest.DspUnitType, opt => opt.MapFrom(src => TryParseNodeIdType(src.Info.SubCategory)))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.FenderId, opt => opt.MapFrom(src => src.FenderId))
                .ForMember(dest => dest.HasBypass, opt => opt.MapFrom(src => src.Ui.HasBypass))
                .ForMember(dest => dest.BypassState, opt => opt.MapFrom(src => src.Ui.HasBypass ? src.DefaultDspUnitParameters.SingleOrDefault(x => x.Name == "bypass").Value : false))
                .ForMember(dest => dest.Parameters, opt => opt.MapFrom(src => src));

            CreateMap<DspUnitDefinition, DspUnitParameterModelCollection>()
                .ConstructUsing((src, context) =>
                {
                    var parameterModels = context.Mapper.Map<DspUnitParameterModelCollection>(src.Ui.UiParameters);
                    if (parameterModels != null && src.DefaultDspUnitParameters != null)
                    {
                        foreach (var p in parameterModels)
                        {
                            p.Value.Value = src.DefaultDspUnitParameters.SingleOrDefault(x => x.Name == p.Key).Value;
                        }
                    }
                    return parameterModels;
                });


            CreateMap<Node, DspUnitModel>()
                .ConstructUsing(src => GetDefinition((DspUnitType)src.NodeId, src.FenderId))
                .ForMember(dest => dest.Parameters, opt => opt.MapFrom(src => GetParameters(GetDefinition((DspUnitType)src.NodeId, src.FenderId), src.DspUnitParameters)));


        }

        public static DspUnitType TryParseNodeIdType(string value)
        {
            return Enum.TryParse(value, out DspUnitType type) ? type : DspUnitType.none;
        }

        public static DspUnitModel GetDefinition(DspUnitType type, string fenderId)
        {
            return DspUnitLists.AllUnits[type].SingleOrDefault(x => x.FenderId == fenderId);
        }

        //public DspUnitParameterModelCollection GetParameters(DspUnitDefinition model)
        //{

        //}

        public static DspUnitParameterModelCollection GetParameters(DspUnitModel model, List<DspUnitParameter> parameters)
        {
            DspUnitParameterModelCollection parameterModels = [];
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    var def = model.Parameters.TryGetValue(p.Name, out DspUnitParameterModel? value) ? value : null;
                    if (def != null)
                    {
                        var paramModel = def;
                        paramModel.Value = p.Value;
                        parameterModels.Add(paramModel);
                    }
                };
            }
            return parameterModels;
        }
    }
}
