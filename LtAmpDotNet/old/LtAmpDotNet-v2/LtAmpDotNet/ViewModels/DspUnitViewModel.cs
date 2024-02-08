using Avalonia.Collections;
using LtAmpDotNet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.ViewModels
{
	public class DspUnitViewModel : ViewModelBase
	{
        public DspUnitViewModel()
        {
        }

        private Dictionary<string, string> _dspUnits;
		public Dictionary<string,string> DspUnits
		{
			get => _dspUnits;
			set => SetProperty(ref _dspUnits, value);
		}

		private string _selectedFenderId;
		public string SelectedFenderId
		{
			get => _selectedFenderId;
			set => SetProperty(ref _selectedFenderId, value);
		}

		private DspUnitModel _model;

		public DspUnitModel Model
		{
			get => _model;
			set => SetProperty(ref _model, value);
		}
	}
}
