using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services.Messages;
using net.thebrent.dotnet.helpers.Collections;

namespace LtAmpDotNet.ViewModels
{
    [MessageReceiverChannel<MessageChannelEnum>(MessageChannelEnum.FromAmplifier)]
    public class DspUnitViewModel : ViewModelBase, IRecipient<ParameterChangedMessage>
    {
        #region Constructors

        public DspUnitViewModel(NodeIdType dspUnitType, DspUnitModelCollection definitions) : base()
        {
            DspUnitType = dspUnitType;
            Definitions = definitions;
            _parameters = [];
            FenderId = dspUnitType == NodeIdType.amp ? "DUBS_LinearGain" : "DUBS_Passthru";
            IsActive = true;
        }

        #endregion Constructors

        #region Properties and fields

        public NodeIdType DspUnitType { get; private set; }
        public DspUnitModelCollection Definitions { get; private set; }

        private DspUnitModel _model = new();

        public DspUnitModel? Model
        {
            get => _model;
            set => SetPropertyAnd(ref _model!, value, (val) =>
            {
                if (val != null)
                {
                    DspUnitParameterViewModelCollection parameters = new(DspUnitType);
                    val.Parameters.ForEach(v => parameters.Add(new DspUnitParameterViewModel(v)));
                    Parameters = parameters;
                    Send(new DspUnitChangedMessage(DspUnitType, FenderId!), MessageChannelEnum.ToAmplifier);
                }
                OnPropertyChanged(nameof(FenderId));
                OnPropertyChanged(nameof(IsVisible));
            });
        }

        public bool IsVisible => FenderId != "DUBS_Passthru";

        public string? FenderId
        {
            get => Model?.FenderId;
            set => Model = value != null && Definitions.TryGetValue(value, out var model) ? model : null;
        }

        public string? DisplayName => Model?.DisplayName;

        public bool HasBypass => Model?.HasBypass ?? false;

        public bool IsBypassing
        {
            get => Model?.IsBypassing ?? false;
            set => SetPropertyAnd(Model!.IsBypassing, value, Model,
                (model, val) => Model.IsBypassing = val,
                (model, val) => Send(new ParameterChangedMessage(DspUnitType, "bypass", val), MessageChannelEnum.ToAmplifier), nameof(IsBypassing));
        }

        private DspUnitParameterViewModelCollection _parameters;

        public DspUnitParameterViewModelCollection Parameters
        {
            get => _parameters;
            set => SetPropertyAnd(ref _parameters, value, (x) => x.PropertyChanged += Parameters_PropertyChanged);
        }

        #endregion Properties and fields

        #region event handlers

        private void Parameters_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DspUnitParameterViewModel.Model.Value) && !IsReceiving.Contains(nameof(Parameters)))
            {
                DspUnitParameterViewModel? parameter = (DspUnitParameterViewModel?)sender;
                if (parameter != null)
                {
                    Send(new ParameterChangedMessage(DspUnitType, parameter.Model.ControlId, parameter.Model.Value), MessageChannelEnum.ToAmplifier);
                }
            }
        }

        #endregion event handlers

        #region Message receivers

        void IRecipient<ParameterChangedMessage>.Receive(ParameterChangedMessage message)
        {
            if (message.DspUnitType == DspUnitType)
            {
                if (!IsReceiving.Contains(nameof(Parameters)))
                {
                    IsReceiving.Add(nameof(Parameters));
                }
                Parameters[message.ControlId].Model.Value = message.ParameterValue;
                IsReceiving.Remove(nameof(Parameters));
            }
        }

        #endregion Message receivers
    }
}