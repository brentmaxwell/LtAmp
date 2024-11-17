namespace LtAmpDotNet.Lib.Model.Profile.ParameterValueTaper
{
    public class ExponentialTaper : Taper
    {
        public ExponentialTaper(float coefficient) : base(coefficient)
        {
        }

        public override float Encode(float value)
        {
            return ForwardExponentialMap(value);
        }

        public override float Decode(float value)
        {
            return ForwardLogarithemicMap(value);
        }
    }
}