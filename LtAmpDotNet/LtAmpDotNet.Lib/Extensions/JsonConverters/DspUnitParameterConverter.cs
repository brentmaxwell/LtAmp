﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidSharp.Utility;
using LtAmpDotNet.Lib.Model.Preset;

namespace LtAmpDotNet.Lib.Extensions.JsonConverters
{
    [JsonConverter(typeof(DspUnitParameterConverter))]
    public class DspUnitParameterConverter : JsonConverter<DspUnitParameter>
    {
        public override void WriteJson(JsonWriter writer, DspUnitParameter? value, JsonSerializer serializer)
        {
            switch(value?.ParameterType)
            {
                case DspUnitParameterType.String:
                    writer.WriteRaw($"\"{value.Name}\": \"{value.Value}\"");
                    break;
                case DspUnitParameterType.Boolean:
                    writer.WriteRaw($"\"{value?.Name}\": {value?.Value.ToString().ToLower()}");
                    break;
                default:
                    writer.WriteRaw($"\"{value?.Name}\": {value?.Value.ToString()}");
                    break;
            }
        }

        public override DspUnitParameter? ReadJson(JsonReader reader, Type objectType, DspUnitParameter? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            DspUnitParameter parameters = new DspUnitParameter();
            foreach(var prop in jObject) { 
                parameters = new DspUnitParameter() { Name = prop.Key, Value = prop.Value! };
            }
            return parameters;
        }
    }
}