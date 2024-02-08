using Avalonia.Controls;
using Avalonia.Threading;
using System;

namespace LtAmpDotNet.Base
{
    public abstract class ViewWindowBase : Window
    {
        public ViewWindowBase() : base()
        {
        }

        public ViewWindowBase(object dataContext) : this()
        {
            DataContext = dataContext;
        }

        public void Do(Action action)
        {
            Dispatcher.UIThread.Invoke(action);
        }

        public TResult Do<TResult>(Func<TResult> action)
        {
            return Dispatcher.UIThread.Invoke(action);
        }
    }

    public abstract class ViewWindowBase<TModel> : ViewWindowBase where TModel : ViewModelBase
    {
        public ViewWindowBase() : base()
        {
        }

        public ViewWindowBase(TModel model) : this()
        {
            ViewModel = model;
        }

        public TModel ViewModel
        {
            get => (TModel)DataContext!;
            set => DataContext = value;
        }
    }
}