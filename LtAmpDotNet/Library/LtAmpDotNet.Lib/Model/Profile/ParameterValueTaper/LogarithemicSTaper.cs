namespace LtAmpDotNet.Lib.Model.Profile.ParameterValueTaper
{
    public class LogarithemicSTaper : LogarithemicTaper
    {
        public LogarithemicSTaper(float coefficient) : base(coefficient)
        {
        }

        public override float Encode(float value)
        {
            return ((value >= 0.5f ? base.Encode((value - 0.5f) * 2.0f) : base.Encode(1.0f - (value * 2.0f))) + 1.0f) * 0.5f;
        }

        public override float Decode(float value)
        {
            return value >= 0.5f ? (base.Decode((value - 0.5f) * 2.0f) + 1.0f) * 0.5f : (1.0f - base.Decode(1.0f - (value * 2.0f))) * 0.5f;
        }
    }
}