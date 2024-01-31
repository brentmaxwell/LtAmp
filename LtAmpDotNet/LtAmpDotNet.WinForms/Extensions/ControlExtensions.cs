using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Extensions
{
    public static class ControlExtensions
    {
        public static object TryInvoke(this Control control, Delegate method)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke(method);
            }
            else
            {
                return method.DynamicInvoke();
            }
        }
    }
}
