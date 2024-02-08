namespace LtAmpDotNet.Lib.Model.Profile.ParameterValueTaper
{
    public class Taper
    {
        protected readonly float _coefficient;
        protected float _logScale => (float)(1.0d / Math.Log(_coefficient));
        protected float _expScale => (float)(1.0d / (_coefficient - 1.0d));

        public Taper(float coefficient)
        {
            _coefficient = coefficient;
        }

        public virtual float Encode(float value)
        {
            return value;
        }

        public virtual float Decode(float value)
        {
            return value;
        }

        public float ForwardLogarithemicMap(float f)
        {
            return (float)(((double)this._logScale) * Math.Log((((double)f) * (_coefficient - 1.0d)) + 1.0d));
        }

        public float ForwardExponentialMap(float f)
        {
            return (float)(((double)this._expScale) * (Math.Pow(_coefficient, (double)f) - 1.0d));
        }

        public static Dictionary<string, Taper> TaperTypes = new()
        {
            { "t10", new ExponentialTaper(81f) },
            { "t10i", new LogarithemicTaper(81f) },
            { "t10r", new LogarithemicTaper(1013.99f) },
            { "t10ri", new ExponentialTaper(1013.99f) },
            { "t10rs", new LogarithemicSTaper(1013.99f) },
            { "t10rsi", new ExponentialSTaper(1013.99f) },
            { "t10s", new ExponentialSTaper(81f) },
            { "t10si", new LogarithemicSTaper(81f) },
            { "t15", new ExponentialTaper(32.111f) },
            { "t15i", new LogarithemicTaper(32.111f) },
            { "t15r", new LogarithemicTaper(94.725f) },
            { "t15ri", new ExponentialTaper(94.725f) },
            { "t15rs", new LogarithemicSTaper(94.725f) },
            { "t15rsi", new ExponentialSTaper(94.725f) },
            { "t15s", new ExponentialSTaper(32.111f) },
            { "t15si", new LogarithemicSTaper(32.111f) },
            { "t20", new ExponentialTaper(16f) },
            { "t20i", new LogarithemicTaper(16f) },
            { "t20r", new LogarithemicTaper(26.61f) },
            { "t20ri", new ExponentialTaper(26.61f) },
            { "t20rs", new LogarithemicSTaper(26.61f) },
            { "t20rsi", new ExponentialSTaper(26.61f) },
            { "t20s", new ExponentialSTaper(16f) },
            { "t20si", new LogarithemicSTaper(16f) },
            { "t25", new ExponentialTaper(9f) },
            { "t25i", new LogarithemicTaper(9f) },
            { "t25r", new LogarithemicTaper(11.4445f) },
            { "t25ri", new ExponentialTaper(11.4445f) },
            { "t25rs", new LogarithemicSTaper(11.4445f) },
            { "t25rsi", new ExponentialSTaper(11.4445f) },
            { "t25s", new ExponentialSTaper(9f) },
            { "t25si", new LogarithemicSTaper(9f) },
            { "t30", new ExponentialTaper(5.44445f) },
            { "t30i", new LogarithemicTaper(5.44445f) },
            { "t30r", new LogarithemicTaper(6.05615f) },
            { "t30ri", new ExponentialTaper(6.05615f) },
            { "t30rs", new LogarithemicSTaper(6.05615f) },
            { "t30rsi", new ExponentialSTaper(6.05615f) },
            { "t30s", new ExponentialSTaper(5.44445f) },
            { "t30si", new LogarithemicSTaper(5.44445f) },
            { "t35", new ExponentialTaper(3.44899f) },
            { "t35i", new LogarithemicTaper(3.44899f) },
            { "t35r", new LogarithemicTaper(3.5918f) },
            { "t35ri", new ExponentialTaper(3.5918f) },
            { "t35rs", new LogarithemicSTaper(3.5918f) },
            { "t35rsi", new ExponentialSTaper(3.5918f) },
            { "t35s", new ExponentialSTaper(3.44899f) },
            { "t35si", new LogarithemicSTaper(3.44899f) },
            { "t40", new ExponentialTaper(2.25f) },
            { "t40i", new LogarithemicTaper(2.25f) },
            { "t40r", new LogarithemicTaper(2.27541f) },
            { "t40ri", new ExponentialTaper(2.27541f) },
            { "t40rs", new LogarithemicSTaper(2.27541f) },
            { "t40rsi", new ExponentialSTaper(2.27541f) },
            { "t40s", new ExponentialSTaper(2.25f) },
            { "t40si", new LogarithemicSTaper(2.25f) },
            { "t45", new ExponentialTaper(1.49383f) },
            { "t45i", new LogarithemicTaper(1.49383f) },
            { "t45r", new LogarithemicTaper(2.49585f) },
            { "t45ri", new ExponentialTaper(2.49585f) },
            { "t45rs", new LogarithemicSTaper(2.49585f) },
            { "t45rsi", new ExponentialSTaper(2.49585f) },
            { "t45s", new ExponentialSTaper(1.49383f) },
            { "t45si", new LogarithemicSTaper(1.49383f) },
            { "t50", new Taper(0f) },
        };
    }
}