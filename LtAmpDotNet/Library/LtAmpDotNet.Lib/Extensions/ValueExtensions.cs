namespace LtAmpDotNet.Lib.Extensions
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

        public static bool IsNumber(this object obj)
        {
            if (Equals(obj, null))
            {
                return false;
            }

            Type objType = obj.GetType();
            objType = Nullable.GetUnderlyingType(objType) ?? objType;

            return objType.IsPrimitive
                ? objType != typeof(bool) &&
                    objType != typeof(char) &&
                    objType != typeof(nint) &&
                    objType != typeof(nuint)
                : objType == typeof(decimal);
        }
    }
}