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
    public abstract class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangedEventHandler? BeforePropertyChanged;

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual bool SetProperty<T>(T originalValue, T value, Action onChanged, [CallerMemberName] string propertyName = "", Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(originalValue, value))
                return false;

            if (validateValue != null && !validateValue(originalValue, value))
                return false;

            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void ObserveChildProperty(INotifyPropertyChanged backingStore)
        {
            if (backingStore != null)
            {
                backingStore.PropertyChanged += OnChildPropertyChanged;
            }
        }

        public void OnBeforePropertyChanged([CallerMemberName] String propertyName = "")
        {
            BeforePropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{this.GetType().Name}.{propertyName}"));
        }

        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{this.GetType().Name}.{propertyName}"));
        }

        private void OnChildPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
