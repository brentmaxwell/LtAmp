using LtAmpDotNet.Lib.Extensions.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    [JsonConverter(typeof(DspUnitParameterConverter))]
    public class DspUnitParameter
    {
        [JsonIgnore]
        public DspUnitParameterType ParameterType { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public dynamic Value
        {
            get
            {
                switch (ParameterType)
                {
                    case DspUnitParameterType.Boolean:
                        return boolValue;
                    case DspUnitParameterType.String:
                        return stringValue;
                    case DspUnitParameterType.Float:
                        return floatValue;
                    case DspUnitParameterType.Integer:
                        return intValue;
                    case DspUnitParameterType.None:
                        if (boolValue.HasValue)
                            return boolValue.Value;
                        if (stringValue.Length > 0)
                            return stringValue;
                        if (floatValue.HasValue)
                            return floatValue.Value;
                        if (intValue.HasValue)
                            return intValue.Value;
                        break;
                }
                return null;
            }
            set
            {
                dynamic temp = value;
                if (Type.GetTypeCode(value.GetType()) == TypeCode.Object)
                {
                    temp = value.Value;
                }
                switch (Type.GetTypeCode(temp.GetType()))
                {
                    case TypeCode.Boolean:
                        bool _boolValue;
                        if (bool.TryParse(string.Format("{0}", temp), out _boolValue))
                        {
                            boolValue = _boolValue;
                            ParameterType = DspUnitParameterType.Boolean;
                        }
                        break;
                    case TypeCode.Single:
                    case TypeCode.Double:
                        float _singleValue;
                        if (float.TryParse(string.Format("{0}", temp), out _singleValue))
                        {
                            floatValue = _singleValue;
                            ParameterType = DspUnitParameterType.Float;
                        }
                        break;
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        int _intValue;
                        if (int.TryParse(string.Format("{0}", temp), out _intValue))
                        {
                            intValue = _intValue;
                            ParameterType = DspUnitParameterType.Integer;
                        }
                        break;
                    case TypeCode.String:
                        stringValue = temp;
                        ParameterType = DspUnitParameterType.String;
                        break;
                    default:
                        throw new Exception("Invalid DSP parameter type");
                }
            }
        }
        [JsonIgnore]
        private float? floatValue;
        [JsonIgnore]
        private string? stringValue;
        [JsonIgnore]
        private bool? boolValue;
        [JsonIgnore]
        private int? intValue;
    }

    public enum DspUnitParameterType
    {
        None = 0,
        Boolean = 6,
        Integer = 9,
        Float = 13,
        String = 18,

        //None = 0,
        //Float = 3,
        //String = 4,
        //Integer = 5,
        //Boolean = 6,
    }
}
