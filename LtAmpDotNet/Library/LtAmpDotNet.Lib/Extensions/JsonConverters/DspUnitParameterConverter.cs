using LtAmpDotNet.Lib.Model.Preset;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LtAmpDotNet.Lib.Extensions.JsonConverters
{
    [JsonConverter(typeof(DspUnitParameterConverter))]
    public class DspUnitParameterConverter : JsonConverter<DspUnitParameter>
    {
        public override void WriteJson(JsonWriter writer, DspUnitParameter? value, JsonSerializer serializer)
        {
            writer.WriteRaw(value?.ParameterType switch
            {
                DspUnitParameterDataType.String => $"\"{value.Name}\": \"{value.Value}\"",
                DspUnitParameterDataType.Boolean => $"\"{value?.Name}\": {value?.Value.ToString().ToLower()}",
                _ => $"\"{value?.Name}\": {value?.Value.ToString()}",
            });
        }
        //{
        //    switch (value?.ParameterType)
        //    {
        //        case DspUnitParameterDataType.String:
        //            writer.WriteRaw($"\"{value.Name}\": \"{value.Value}\"");
        //            break;

        //        case DspUnitParameterDataType.Boolean:
        //            writer.WriteRaw($"\"{value?.Name}\": {value?.Value.ToString().ToLower()}");
        //            break;

        //        default:
        //            writer.WriteRaw($"\"{value?.Name}\": {value?.Value.ToString()}");
        //            break;
        //    }
        //}

        public override DspUnitParameter? ReadJson(JsonReader reader, Type objectType, DspUnitParameter? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            DspUnitParameter parameters = new();
            foreach (KeyValuePair<string, JToken?> prop in jObject)
            {
                parameters = new DspUnitParameter() { Name = prop.Key, Value = prop.Value! };
            }
            return parameters;
        }
    }
}