using Avalonia.Controls;
using Avalonia.Threading;
using System;

namespace LtAmpDotNet.Base
{
    public abstract class ViewBase : UserControl
    {
        public ViewBase() : base()
        {
        }

        public ViewBase(object dataContext) : this()
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

    public abstract class ViewBase<TModel> : ViewBase
    {
        public ViewBase() : base()
        {
        }

        public ViewBase(TModel model) : this()
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