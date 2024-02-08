using LtAmpDotNet.Lib.Extensions.JsonConverters;
using Newtonsoft.Json;

namespace LtAmpDotNet.Lib.Model.Preset
{
    [JsonConverter(typeof(DspUnitParameterConverter))]
    public class DspUnitParameter
    {
        [JsonIgnore]
        public DspUnitParameterDataType ParameterType { get; set; }

        [JsonIgnore]
        public string? Name { get; set; }

        [JsonIgnore]
        public dynamic Value
        {
            get => ParameterType switch
            {
                DspUnitParameterDataType.Boolean => boolValue.GetValueOrDefault(),
                DspUnitParameterDataType.String => stringValue!,
                DspUnitParameterDataType.Float => floatValue.GetValueOrDefault(),
                DspUnitParameterDataType.Integer => intValue.GetValueOrDefault(),
                DspUnitParameterDataType.None =>
                    boolValue.HasValue ? boolValue.Value :
                    stringValue?.Length > 0 ? stringValue :
                    floatValue.HasValue ? floatValue.Value :
                    intValue.HasValue ? intValue.Value : null,
                _ => null!,
            };
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
                            ParameterType = DspUnitParameterDataType.Boolean;
                        }
                        break;

                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        float _singleValue;
                        if (float.TryParse(string.Format("{0}", temp), out _singleValue))
                        {
                            floatValue = _singleValue;
                            ParameterType = DspUnitParameterDataType.Float;
                        }
                        break;

                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        int _intValue;
                        if (int.TryParse(string.Format("{0}", temp), out _intValue))
                        {
                            intValue = _intValue;
                            ParameterType = DspUnitParameterDataType.Integer;
                        }
                        break;

                    case TypeCode.String:
                        stringValue = temp;
                        ParameterType = DspUnitParameterDataType.String;
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

    public enum DspUnitParameterDataType
    {
        None = 0b0010,  // 0  0 0 0 0
        Boolean = 0b0001,  // 1  0 0 0 1
        Integer = 0b1010,  //10  1 0 1 0
        Float = 0b1011,  //11  1 0 1 1
        String = 0x0100,  //4   0 1 0 0
        Numeric = 0b1000,
    }
}