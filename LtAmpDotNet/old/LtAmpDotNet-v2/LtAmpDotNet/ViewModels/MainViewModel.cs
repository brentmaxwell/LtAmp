using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LtAmpDotNet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
	private AmplifierViewModel amplifierViewModel = new ();
	public AmplifierViewModel AmplifierViewModel
	{
		get => amplifierViewModel;
		set => SetProperty(ref amplifierViewModel, value);
	}
}
