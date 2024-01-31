using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Base
{
    public interface IViewModel : INotifyPropertyChanged
    {
        delegate void ValueChangedEventHandler (object sender, ValueChangedEventArgs e);
        event ValueChangedEventHandler ValueChanged;
    }
}
