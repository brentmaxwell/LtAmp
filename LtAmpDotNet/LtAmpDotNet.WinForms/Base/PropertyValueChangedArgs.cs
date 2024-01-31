using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Base
{
    public class ValueChangedEventArgs
    {
        public ValueChangedEventArgs(string propertyName, object previousValue, object newValue)
        {
            PropertyName = propertyName;
            PreviousValue = previousValue;
            NewValue = newValue;
        }

        public string PropertyName { get; }
        
        public object PreviousValue { get; }
        
        public object NewValue { get; }
    }
}
