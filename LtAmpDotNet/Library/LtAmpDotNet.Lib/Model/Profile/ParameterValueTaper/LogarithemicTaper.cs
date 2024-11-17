namespace LtAmpDotNet.Lib.Model.Profile.ParameterValueTaper
{
    public class LogarithemicTaper : Taper
    {
        public LogarithemicTaper(float coefficient) : base(coefficient)
        {
        }

        public override float Encode(float f)
        {
            return ForwardLogarithemicMap(f);
        }

        public override float Decode(float f)
        {
            return ForwardExponentialMap(f);
        }
    }
}