using Newtonsoft.Json.Linq;
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
    [JsonConverter(typeof(DspUnitParameterCollectionConverter))]
    public class DspUnitParameterCollectionConverter : JsonConverter<List<DspUnitParameter>>
    {
        public override void WriteJson(JsonWriter writer, List<DspUnitParameter>? value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value!);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                o.WriteTo(writer);
            }
        }

        public override List<DspUnitParameter>? ReadJson(JsonReader reader, Type objectType, List<DspUnitParameter>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            List<DspUnitParameter> parameters = new List<DspUnitParameter>();
            foreach (var prop in jObject)
            {
                parameters.Add(new DspUnitParameter() { Name = prop.Key, Value = prop.Value! });
            }

            if (jObject != null && jObject.Count > 1)
            {
                return parameters;
            }

            return null;
        }
    }
}
