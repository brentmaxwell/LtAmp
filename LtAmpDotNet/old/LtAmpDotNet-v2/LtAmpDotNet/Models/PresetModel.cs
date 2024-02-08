using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class PresetModel : ObservableModel
    {
		private string _displayName;
		public string DisplayName
		{
			get => _displayName;
			set => SetProperty(ref _displayName, value);
		}

        private ObservableDictionary<NodeIdType, DspUnitModel> _dspUnits = new();
        public ObservableDictionary<NodeIdType, DspUnitModel> DspUnits
        {
            get => _dspUnits;
            set => SetProperty(ref _dspUnits, value);
        }
    }

	public class PresetModelCollection : ObservableDictionary<int, PresetModel> { }
}
