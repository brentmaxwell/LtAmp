using System.Collections.ObjectModel;

namespace LtAmpDotNet.ViewModels
{
    public class AmplifierViewModel : ViewModelBase
    {
		private PresetViewModel presetViewModel = new();

		public PresetViewModel PresetViewModel
		{
			get => presetViewModel;
			set => SetProperty(ref presetViewModel, value);
		}

	}
}
