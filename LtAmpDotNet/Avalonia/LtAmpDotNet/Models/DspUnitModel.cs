using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class DspUnitModel : ObservableObject
    {
		private string _displayName;
		public string DisplayName
		{
			get => _displayName;
			set => SetProperty(ref _displayName, value);
		}

        private string _fenderId;
        public string FenderId
        {
            get => _fenderId;
            set => SetProperty(ref _fenderId, value);
        }
    }
}
