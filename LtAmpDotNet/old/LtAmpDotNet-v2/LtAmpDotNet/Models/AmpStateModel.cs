using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class AmpStateModel : ObservableModel
    {
		private DspUnitDefinitionCollection _dspUnitTypes;
		public DspUnitDefinitionCollection DspUnitTypes
        {
			get => _dspUnitTypes;
			set => SetProperty(ref _dspUnitTypes, value);
		}

		private PresetModelCollection _presetList;
		public PresetModelCollection PresetList
		{
			get => _presetList;
            set => SetProperty(ref _presetList, value);
		}

	}
}
