using System.ComponentModel;

namespace LtAmpDotNet.Base
{
    public interface IViewModel : INotifyPropertyChanged
    {
        delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);
        event ValueChangedEventHandler ValueChanged;
    }
}
