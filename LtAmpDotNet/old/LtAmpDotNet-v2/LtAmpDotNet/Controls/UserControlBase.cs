using Avalonia.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Controls
{
    public class UserControlBase<T> : UserControl
    {
        public T ViewModel
        {
            get => (T)DataContext;
            set => DataContext = value;
        }
    }
}
