namespace net.thebrent.dotnet.helpers
{
    public class ExecutableDictionary<TKey> : Dictionary<TKey, Action> where TKey : notnull
    {
        public void Eval(TKey key)
        {
            if (ContainsKey(key))
            {
                this[key].Invoke();
            }
            else
            {
                this[default].Invoke();
            }
        }
    }

    public class ExecutableDictionary<TKey, T1> : Dictionary<TKey, Action<T1>> where TKey : notnull
    {
        public void Eval(TKey key, T1 p1)
        {
            if (ContainsKey(key))
            {
                this[key].Invoke(p1);
            }
            else
            {
                this[default].Invoke(p1);
            }
        }
    }
}