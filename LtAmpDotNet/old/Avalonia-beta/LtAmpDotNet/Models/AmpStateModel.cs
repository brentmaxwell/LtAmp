using AutoMapper;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Models.Enums;
using System;
using System.Linq;

namespace LtAmpDotNet.Models
{
    public class AmpStateModel : ModelBase
    {
        private readonly IMapper _mapper;
        public readonly ILtAmplifier _amplifier;

        private bool _isAmplifierConnected;

        public bool IsAmplifierConnected
        {
            get => _isAmplifierConnected;
            set => SetProperty(ref _isAmplifierConnected, value);
        }

        private PresetModelCollection _presets = [];
        public PresetModelCollection Presets
        {
            get => _presets;
            set => SetPropertyAnd(ref _presets, value, (x) => x.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged);
        }

        private int _currentPresetIndex;
        public int CurrentPresetIndex
        {
            get => _currentPresetIndex;
            set => SetPropertyAnd(ref _currentPresetIndex, value, (x) => OnPropertyChanged(nameof(CurrentPreset)));
        }

        public PresetModel CurrentPreset => Presets[CurrentPresetIndex];

        private uint[] _qaSlots;
        public uint[] QaSlots
        {
            get => _qaSlots;
            set => SetProperty(ref _qaSlots, value);
        }

        public AmpStateModel(ILtAmplifier amplifier, IMapper mapper)
        {
            _amplifier = amplifier;
            _mapper = mapper;
            _presets = [];
            DspUnitLists.Populate(_mapper, DspUnitDefinition.Load());
            Connect();
        }

        public async void Connect()
        {
            _amplifier.AmplifierConnected += Amplifier_AmplifierConnected;
            _amplifier.AmplifierDisconnected += Amplifier_AmplifierDisconnected;
            _amplifier.CurrentDisplayedPresetIndexStatusMessageReceived += Amplifier_CurrentDisplayedPresetIndexStatusMessageReceived;
            _amplifier.CurrentLoadedPresetIndexStatusMessageReceived += Amplifier_CurrentLoadedPresetIndexStatusMessageReceived;
            _amplifier.CurrentPresetStatusMessageReceived += Amplifier_CurrentPresetStatusMessageReceived;
            _amplifier.PresetJSONMessageReceived += Amplifier_PresetJSONMessageReceived;
            _amplifier.DspUnitParameterStatusMessageReceived += Amplifier_DspUnitParameterStatusMessageReceived;
            _presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            PropertyChanged += OnPropertyChanged_SetAmpState;
            await _amplifier.OpenAsync(true);
        }

        public void Disconnect()
        {
            _amplifier.Close();
        }

        public async void OnPropertyChanged_SetAmpState(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PropertyChanged -= OnPropertyChanged_SetAmpState;
            switch (e.PropertyName)
            {
                case nameof(CurrentPresetIndex):
                    await _amplifier.LoadPresetAsync(CurrentPresetIndex);
                    break;
                case nameof(QaSlots):
                    await _amplifier.SetQASlotsAsync(QaSlots);
                    break;
            }
            PropertyChanged += OnPropertyChanged_SetAmpState;
        }

        private async void OnDspUnitParameterValueChanged(object? sender, Events.DspUnitParameterValueChangedEventArgs e)
        {
            var model = (PresetModel)sender;
            //_presets.DspUnitParameterValueChanged -= OnDspUnitParameterValueChanged;
            var parameter = model.DspUnits[e.DspUnitType].Parameters[e.ControlId];
            parameter.Value = e.NewValue;
            await _amplifier.SetDspUnitParameterAsync((NodeIdType)e.DspUnitType, new DspUnitParameter()
            {
                Name = parameter.ControlId,
                ParameterType = parameter.DataType,
                Value = parameter.Value
            });
            //_presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
        }

        private void Amplifier_AmplifierConnected(object? sender, EventArgs e)
        {
            IsAmplifierConnected = true;
            _amplifier.GetAllPresets();
            _amplifier.GetCurrentPreset();
        }

        private async void Amplifier_AmplifierDisconnected(object? sender, EventArgs e)
        {
            IsAmplifierConnected = false;
            _amplifier.Dispose();
        }

        private void Amplifier_PresetJSONMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            PropertyChanged -= OnPropertyChanged_SetAmpState;
            _presets.DspUnitParameterValueChanged -= OnDspUnitParameterValueChanged;
            var slotIndex = e.Message.PresetJSONMessage.SlotIndex;
            var preset = Preset.FromString(e.Message.PresetJSONMessage.Data);
            if (preset != null)
            {
                Presets[slotIndex] = _mapper.Map<PresetModel>(preset);
                OnPropertyChanged(nameof(Presets));

            }
            _presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            PropertyChanged += OnPropertyChanged_SetAmpState;
        }

        private void Amplifier_CurrentPresetStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            PropertyChanged -= OnPropertyChanged_SetAmpState;
            _presets.DspUnitParameterValueChanged -= OnDspUnitParameterValueChanged;
            var slotIndex = e.Message.CurrentPresetStatus.CurrentSlotIndex;
            CurrentPresetIndex = slotIndex;
            _presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            PropertyChanged += OnPropertyChanged_SetAmpState;
        }

        private void Amplifier_CurrentLoadedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            PropertyChanged -= OnPropertyChanged_SetAmpState;
            _presets.DspUnitParameterValueChanged -= OnDspUnitParameterValueChanged;
            CurrentPresetIndex = e.Message.CurrentLoadedPresetIndexStatus.CurrentLoadedPresetIndex;
            _presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            PropertyChanged += OnPropertyChanged_SetAmpState;
        }

        private void Amplifier_CurrentDisplayedPresetIndexStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            PropertyChanged -= OnPropertyChanged_SetAmpState;
            _presets.DspUnitParameterValueChanged -= OnDspUnitParameterValueChanged;
            CurrentPresetIndex = e.Message.CurrentDisplayedPresetIndexStatus.CurrentDisplayedPresetIndex;
            _presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
            PropertyChanged += OnPropertyChanged_SetAmpState;
        }

        private void Amplifier_DspUnitParameterStatusMessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            _presets.DspUnitParameterValueChanged -= OnDspUnitParameterValueChanged;
            var message = e.Message.DspUnitParameterStatus;
            var nodeId = Enum.Parse<DspUnitType>(message.NodeId);
            var parameterId = message.ParameterId;
            var parameterObject = CurrentPreset.DspUnits[nodeId].Parameters[parameterId];
            switch (message.TypeCase)
            {
                case Lib.Models.Protobuf.DspUnitParameterStatus.TypeOneofCase.BoolParameter:
                    parameterObject.Value = message.BoolParameter;
                    break;
                case Lib.Models.Protobuf.DspUnitParameterStatus.TypeOneofCase.Sint32Parameter:
                    parameterObject.Value = message.Sint32Parameter;
                    break;
                case Lib.Models.Protobuf.DspUnitParameterStatus.TypeOneofCase.FloatParameter:
                    parameterObject.Value = message.FloatParameter;
                    break;
                case Lib.Models.Protobuf.DspUnitParameterStatus.TypeOneofCase.StringParameter:
                    parameterObject.Value = message.StringParameter;
                    break;
            }
            _presets.DspUnitParameterValueChanged += OnDspUnitParameterValueChanged;
        }
    }
}
