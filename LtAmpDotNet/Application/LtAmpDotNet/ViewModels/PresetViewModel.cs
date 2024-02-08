using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services.Messages;

namespace LtAmpDotNet.ViewModels
{
    [MessageReceiverChannel<MessageChannelEnum>(MessageChannelEnum.FromAmplifier)]
    public class PresetViewModel : ViewModelBase,
        IRecipient<CurrentPresetChangedMessage>,
        IRecipient<DspUnitChangedMessage>
    {
        #region Constructors

        public PresetViewModel(AmpStateModel ampState) : base()
        {
            AmpState = ampState;
            AmpState.PropertyChanged += AmpState_PropertyChanged;
            DspUnits = new(AmpState.Definitions);
            IsActive = true;
        }

        #endregion Constructors

        #region Fields and properties

        public AmpStateModel AmpState { get; }

        public bool IsPresetEdited
        {
            get => CurrentPreset.IsPresetEdited;
            set => SetProperty(CurrentPreset.IsPresetEdited, value, CurrentPreset, (model, val) => model.IsPresetEdited = val);
        }

        private int _currentPresetIndex;

        public int CurrentPresetIndex
        {
            get => _currentPresetIndex;
            set => SetPropertyAnd(ref _currentPresetIndex, value, (val) =>
            {
                if (AmpState.Presets.Contains(val))
                {
                    CurrentPreset = AmpState.Presets[val].Clone();
                    Send(new CurrentPresetChangedMessage(val), MessageChannelEnum.ToAmplifier);
                }
            });
        }

        private PresetModel _currentPreset = new();

        public PresetModel CurrentPreset
        {
            get => _currentPreset;
            set => SetPropertyAnd(ref _currentPreset, value, (val) =>
            {
                DspUnits[NodeIdType.amp].Model = _currentPreset.AmpUnit;
                DspUnits[NodeIdType.stomp].Model = _currentPreset.StompUnit;
                DspUnits[NodeIdType.mod].Model = _currentPreset.ModUnit;
                DspUnits[NodeIdType.delay].Model = _currentPreset.DelayUnit;
                DspUnits[NodeIdType.reverb].Model = _currentPreset.ReverbUnit;
            });
        }

        public DspUnitViewModelCollection DspUnits { get; set; }

        #endregion Fields and properties

        #region Message receivers

        void IRecipient<CurrentPresetChangedMessage>.Receive(CurrentPresetChangedMessage message)
        {
            if (!IsReceiving.Contains(nameof(CurrentPreset)))
            {
                IsReceiving.Add(nameof(CurrentPreset));
            }
            CurrentPresetIndex = message.PresetIndex;
            IsReceiving.Remove(nameof(CurrentPreset));
        }

        void IRecipient<DspUnitChangedMessage>.Receive(DspUnitChangedMessage message)
        {
            if (!IsReceiving.Contains(nameof(DspUnits)))
            {
                IsReceiving.Add(nameof(DspUnits));
            }
            DspUnits[message.DspUnitType].Model = AmpState.Definitions[message.DspUnitType][message.FenderId];
            IsReceiving.Remove(nameof(DspUnits));
        }

        #endregion Message receivers

        #region Event handlers

        private void AmpState_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        #endregion Event handlers
    }
}