using AutoMapper;
using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models.Enums;
using LtAmpDotNet.Models.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LtAmpDotNet.Models
{
    public class DspUnitModel : ObservableModel, IDspUnitParameterChangedEvent
    {
        public event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;
        event DspUnitParameterValueChangedEventHandler? IDspUnitParameterChangedEvent.DspUnitParameterValueChanged
        {
            add => DspUnitParameterValueChanged += value;
            remove => DspUnitParameterValueChanged -= value;
        }

        private string? _displayName;
        public string? DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private string? _fenderId;
        public string? FenderId
        {
            get => _fenderId;
            set => SetProperty(ref _fenderId, value);
        }

        private DspUnitType _dspUnitType;
        public DspUnitType DspUnitType
        {
            get => _dspUnitType;
            set => SetProperty(ref _dspUnitType, value);
        }

        private bool _hasBypass;

        public bool HasBypass
        {
            get => _hasBypass;
            set => SetProperty(ref _hasBypass, value);
        }

        private bool _bypassState;

        public bool BypassState
        {
            get => _bypassState;
            set => SetProperty(ref _bypassState, value);
        }


        private DspUnitParameterModelCollection _parameters;
        public DspUnitParameterModelCollection Parameters
        {
            get => _parameters;
            set
            {
                if (SetProperty(ref _parameters, value))
                {
                    _parameters.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
                }
            }
        }

        private void OnDspUnitParameterValueChanged(object? sender, DspUnitParameterValueChangedEventArgs e)
        {
            e.DspUnitType = DspUnitType;
            DspUnitParameterValueChanged?.Invoke(this, e);
        }
    }

    public class DspUnitDefinitionModelCollection : ObservableCollection<DspUnitModel>
    {
        public DspUnitDefinitionModelCollection()
        {
        }

        public DspUnitDefinitionModelCollection(IEnumerable<DspUnitModel> models) : base(models)
        { }

        public DspUnitDefinitionModelCollection(IMapper mapper, List<DspUnitDefinition> definitions) : this(mapper.Map<DspUnitDefinitionModelCollection>(definitions))
        {
        }

        public void Add(IEnumerable<DspUnitModel> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public DspUnitModel? GetByFenderId(string fenderId)
        {
            return this.SingleOrDefault(x => x.FenderId == fenderId);
        }

        public static implicit operator DspUnitDefinitionModelCollection(List<DspUnitModel> models)
        {
            return new DspUnitDefinitionModelCollection(models);
        }
    }

    public class DspUnitModelCollection : ObservableDictionary<DspUnitType, DspUnitModel>, IDspUnitParameterChangedEvent
    {
        public event DspUnitParameterValueChangedEventHandler DspUnitParameterValueChanged;

        event DspUnitParameterValueChangedEventHandler? IDspUnitParameterChangedEvent.DspUnitParameterValueChanged
        {
            add => DspUnitParameterValueChanged += value;
            remove => DspUnitParameterValueChanged -= value;
        }
        public DspUnitModelCollection() : this(null, null, null, null, null) { }
        public DspUnitModelCollection(DspUnitModel ampUnit, DspUnitModel stompUnit, DspUnitModel modUnit, DspUnitModel delayUnit, DspUnitModel reverbUnit)
        {
            CollectionChanged += OnCollectionChanged;
            Items.Add(DspUnitType.amp, ampUnit);
            Items.Add(DspUnitType.stomp, stompUnit);
            Items.Add(DspUnitType.mod, modUnit);
            Items.Add(DspUnitType.delay, delayUnit);
            Items.Add(DspUnitType.reverb, reverbUnit);
        }

        private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (KeyValuePair<DspUnitType, DspUnitModel> item in e.NewItems)
            {
                this[item.Key].DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            }
        }

        private void OnDspUnitParameterValueChanged(object? sender, DspUnitParameterValueChangedEventArgs e)
        {
            DspUnitParameterValueChanged?.Invoke(sender, e);
        }
    }

    public static class DspUnitLists
    {
        public static DspUnitDefinitionModelCollection Amps;
        public static DspUnitDefinitionModelCollection Stomp;
        public static DspUnitDefinitionModelCollection Mod;
        public static DspUnitDefinitionModelCollection Delay;
        public static DspUnitDefinitionModelCollection Reverb;

        public static Dictionary<DspUnitType,DspUnitDefinitionModelCollection> AllUnits;

        public static void Populate(IMapper mapper, List<DspUnitDefinition> definitions)
        {
            var allUnits = new DspUnitDefinitionModelCollection(mapper, definitions);
            Amps = mapper.Map<List<DspUnitModel>>(allUnits.Where(x => x.DspUnitType == DspUnitType.amp || x.FenderId == "DUBS_Passthru"));
            Stomp = mapper.Map<List<DspUnitModel>>(allUnits.Where(x => x.DspUnitType == DspUnitType.stomp || x.FenderId == "DUBS_Passthru"));
            Mod = mapper.Map<List<DspUnitModel>>(allUnits.Where(x => x.DspUnitType == DspUnitType.mod || x.FenderId == "DUBS_Passthru"));
            Delay = mapper.Map<List<DspUnitModel>>(allUnits.Where(x => x.DspUnitType == DspUnitType.delay || x.FenderId == "DUBS_Passthru"));
            Reverb = mapper.Map<List<DspUnitModel>>(allUnits.Where(x => x.DspUnitType == DspUnitType.reverb || x.FenderId == "DUBS_Passthru"));
            AllUnits = new Dictionary<DspUnitType, DspUnitDefinitionModelCollection>()
            {
                { DspUnitType.amp, Amps },
                { DspUnitType.stomp, Stomp },
                { DspUnitType.mod, Mod },
                { DspUnitType.delay, Delay },
                { DspUnitType.reverb, Reverb },
            };
        }
    }
}
