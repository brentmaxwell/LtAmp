using LtAmpDotNet.Models;
using System.Collections.Generic;

namespace LtAmpDotNet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        AmpState = new AmpStateModel();
    }

    public AmpStateModel AmpState { get; set; } 
}
