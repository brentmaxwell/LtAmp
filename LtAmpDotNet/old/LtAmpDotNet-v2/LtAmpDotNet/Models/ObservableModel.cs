using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public abstract class ObservableModel : ObservableObject
    {
        protected bool SetPropertyAnd<T>([NotNullIfNotNull(nameof(newValue))] ref T field, T newValue, Action<T> callback, [CallerMemberName] string? propertyName = null)
        {
            if (SetProperty<T>(ref field, newValue, propertyName))
            {
                callback(field);
                return true;
            }
            return false;
        }

        protected bool SetPropertyAnd<TModel, T>(T oldValue, T newValue, TModel model, Action<TModel, T> setCallback, Action<TModel, T> callback, [CallerMemberName] string? propertyName = null)
            where TModel : class
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(callback);
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                return false;
            }
            OnPropertyChanging(propertyName);
            setCallback(model, newValue);
            callback(model, newValue);
            OnPropertyChanged(propertyName);
            return true;
        }
    }
    {
    }
}
