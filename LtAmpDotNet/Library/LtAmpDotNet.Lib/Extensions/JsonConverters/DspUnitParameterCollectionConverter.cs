using LtAmpDotNet.Lib.Model.Preset;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LtAmpDotNet.Lib.Extensions.JsonConverters
{
    [JsonConverter(typeof(DspUnitParameterCollectionConverter))]
    public class DspUnitParameterCollectionConverter : JsonConverter<List<DspUnitParameter>>
    {
        public override void WriteJson(JsonWriter writer, List<DspUnitParameter>? value, JsonSerializer serializer)
        {
            Dictionary<string, dynamic> dValue = value!.ToDictionary(x => x.Name!, x => x.Value);
            JToken t = JToken.FromObject(dValue!);
            t.WriteTo(writer);
        }

        public override List<DspUnitParameter>? ReadJson(JsonReader reader, Type objectType, List<DspUnitParameter>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            List<DspUnitParameter> parameters = [];
            foreach (KeyValuePair<string, JToken?> prop in jObject)
            {
                parameters.Add(new DspUnitParameter() { Name = prop.Key, Value = prop.Value! });
            }

            return jObject != null && jObject.Count > 1 ? parameters : null;
        }
    }
}