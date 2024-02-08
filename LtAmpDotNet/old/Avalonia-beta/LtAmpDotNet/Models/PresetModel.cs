using LtAmpDotNet.Base;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Models.Enums;
using LtAmpDotNet.Models.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LtAmpDotNet.Models
{
    public class PresetModel : ObservableModel, IDspUnitParameterChangedEvent
    {
        public PresetModel()
        {
            _dspUnits = [];
            _dspUnits.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
        }

        private int _slotIndex;
        public int SlotIndex
        {
            get => _slotIndex;
            set => SetProperty(ref _slotIndex, value);
        }


        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;

        event DspUnitParameterValueChangedEventHandler? IDspUnitParameterChangedEvent.DspUnitParameterValueChanged
        {
            add => DspUnitParameterValueChanged += value;
            remove => DspUnitParameterValueChanged -= value;
        }

        private DspUnitModelCollection _dspUnits;
        public DspUnitModelCollection DspUnits
        {
            get => _dspUnits;
            set
            {
                if (_dspUnits == null)
                {
                    if (SetProperty(ref _dspUnits, value))
                    {
                        _dspUnits.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
                    }
                }
            }
        }
        public string SelectedAmpFenderId
        {
            get => AmpUnit.FenderId;
            set => AmpUnit = DspUnitLists.Amps.GetByFenderId(value);
        }
        public DspUnitModel AmpUnit
        {
            get => _dspUnits[DspUnitType.amp];
            set => SetProperty(DspUnits[DspUnitType.amp], value, DspUnits, (x, y) => x[DspUnitType.amp] = y);
        }

        public string SelectedStompFenderId
        {
            get => StompUnit.FenderId;
            set
            {
                StompUnit = DspUnitLists.Stomp.GetByFenderId(value);
            }
        }
        public DspUnitModel StompUnit
        {
            get => _dspUnits[DspUnitType.stomp];
            set => SetProperty(DspUnits[DspUnitType.stomp], value, DspUnits, (x, y) => x[DspUnitType.stomp] = y);
        }

        public string SelectedModFenderId
        {
            get => ModUnit.FenderId;
            set
            {
                ModUnit = DspUnitLists.Mod.GetByFenderId(value);
            }
        }
        public DspUnitModel ModUnit
        {
            get => _dspUnits[DspUnitType.mod];
            set => SetProperty(DspUnits[DspUnitType.mod], value, DspUnits, (x, y) => x[DspUnitType.mod] = y);
        }

        public string SelectedDelayFenderId
        {
            get => DelayUnit.FenderId;
            set
            {
                DelayUnit = DspUnitLists.Delay.GetByFenderId(value);
            }
        }
        public DspUnitModel DelayUnit
        {
            get => _dspUnits[DspUnitType.delay];
            set => SetProperty(DspUnits[DspUnitType.delay], value, DspUnits, (x, y) => x[DspUnitType.delay] = y);
        }

        public string SelectedReverbFenderId
        {
            get => ReverbUnit.FenderId;
            set
            {
                ReverbUnit = DspUnitLists.Reverb.GetByFenderId(value);
            }
        }
        public DspUnitModel ReverbUnit
        {
            get => _dspUnits[DspUnitType.reverb];
            set => SetProperty(DspUnits[DspUnitType.reverb], value, DspUnits, (x, y) => x[DspUnitType.reverb] = y);
        }

        private void OnDspUnitParameterValueChanged(object? sender, DspUnitParameterValueChangedEventArgs e)
        {
            e.PresetIndex = SlotIndex;
            if (sender.GetType() == typeof(DspUnitModel))
            {
                var model = (DspUnitModel)sender;
                if (DspUnits[e.DspUnitType].FenderId == model.FenderId)
                {
                    DspUnitParameterValueChanged?.Invoke(this, e);
                }
            }
        }
    }

    public class PresetModelCollection : ObservableCollection<PresetModel>, IDspUnitParameterChangedEvent
    {
        public event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;

        event DspUnitParameterValueChangedEventHandler? IDspUnitParameterChangedEvent.DspUnitParameterValueChanged
        {
            add => DspUnitParameterValueChanged += value;
            remove => DspUnitParameterValueChanged -= value;
        }

        public PresetModelCollection()
        {
            for (int i = 0; i <= LtAmplifier.NUM_OF_PRESETS; i++)
            {
                this.Add(null);
            }
            CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this[e.NewStartingIndex].DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
        }

        private void OnDspUnitParameterValueChanged(object? sender, DspUnitParameterValueChangedEventArgs e)
        {
            DspUnitParameterValueChanged?.Invoke(sender, e);
        }
    }
}
