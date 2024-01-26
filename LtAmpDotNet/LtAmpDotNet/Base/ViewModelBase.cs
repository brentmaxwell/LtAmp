using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static LtAmpDotNet.Base.IViewModel;

namespace LtAmpDotNet.Base
{
    public abstract class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ValueChangedEventHandler? ValueChanged;

        public ViewModelBase()
        {
        }

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            var previousValue = backingStore;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            OnValueChanged(propertyName, previousValue, value);
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
            OnValueChanged(propertyName);
            return true;
        }

        protected virtual void ObserveChildProperty(INotifyPropertyChanged backingStore)
        {
            if (backingStore != null)
            {
                backingStore.PropertyChanged += OnChildPropertyChanged;
            }
        }

        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{this.GetType().Name}.{propertyName}"));
        }

        public void OnValueChanged([CallerMemberName] String propertyName = "", object? previousValue = null, object? newValue = null)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs($"{GetType().Name}.{propertyName}", previousValue, newValue));
        }


        private void OnChildPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void OnChildPropertyChanged(object? sender, ValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
