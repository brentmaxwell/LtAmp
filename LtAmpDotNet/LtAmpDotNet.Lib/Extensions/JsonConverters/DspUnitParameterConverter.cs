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
            switch (value?.ParameterType)
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
            foreach (KeyValuePair<string, JToken?> prop in jObject)
            {
                parameters = new DspUnitParameter() { Name = prop.Key, Value = prop.Value! };
            }
            return parameters;
        }
    }
}