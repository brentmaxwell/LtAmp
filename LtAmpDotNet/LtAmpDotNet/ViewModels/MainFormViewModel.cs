﻿using Accessibility;
using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.ViewModels
{
    public class MainFormViewModel : ViewModelBase
    {
		private const int NUM_OF_PRESETS = 60;

		private bool _connectionStatus;

		public bool ConnectionStatus
		{
			get => _connectionStatus;
			set => SetProperty(ref _connectionStatus, value);
		}


		private List<Preset> _presets = new List<Preset>();
		public List<Preset> Presets
		{
			get => _presets;
			set => SetProperty(ref _presets, value);
		}

		private int _currentPresetIndex;
		public int CurrentPresetIndex
		{
			get => _currentPresetIndex;
			set
			{
				OnBeforePropertyChanged();
                CurrentPresetViewModel = new CurrentPresetPanelViewModel(_presets[value]);
                SetProperty(ref _currentPresetIndex, value);
			}
		}

		public Preset CurrentPreset
		{
			get => _presets[_currentPresetIndex];
			set
			{
				_presets[_currentPresetIndex] = value;
                CurrentPresetViewModel = new CurrentPresetPanelViewModel(value);
                OnPropertyChanged("CurrentPreset");
            }
		}


		private CurrentPresetPanelViewModel? _currentPresetViewModel;
		public CurrentPresetPanelViewModel? CurrentPresetViewModel
        {
			get => _currentPresetViewModel;
			set
			{
				SetProperty(ref _currentPresetViewModel, value);
				ObserveChildProperty(_currentPresetViewModel);
			}
		}

		private bool _isPresetEdited;
		public bool IsPresetEdited
		{
            get => _isPresetEdited;
            set => SetProperty(ref _isPresetEdited, value);
        }

		private uint[] _footswitchPresets = new uint[2];

		public uint[] FootswitchPresets
		{
			get => _footswitchPresets;
			set => SetProperty(ref _footswitchPresets, value);
		}


		public MainFormViewModel()
		{
			for (var i = 0; i <= NUM_OF_PRESETS; i++)
			{
				_presets.Add(Preset.New);
			}
        }

	}
}
