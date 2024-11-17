namespace LtAmpDotNet.Extensions
{
    public static class ControlExtensions
    {
        public static object TryInvoke(this Control control, Delegate method)
        {
            return control.InvokeRequired ? control.Invoke(method) : method.DynamicInvoke();
        }
    }
}
