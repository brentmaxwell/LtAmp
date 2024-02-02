namespace LtAmpDotNet.Extensions
{
    public static class ValueExtensions
    {
        public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            float fromAbs = from - fromMin;
            float fromMaxAbs = fromMax - fromMin;
            float normal = fromAbs / fromMaxAbs;
            float toMaxAbs = toMax - toMin;
            float toAbs = toMaxAbs * normal;
            float to = toAbs + toMin;
            return to;
        }
    }
}
