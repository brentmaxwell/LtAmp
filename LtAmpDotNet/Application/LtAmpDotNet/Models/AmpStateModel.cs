using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Services.Messages;
using System;
using System.Linq;

namespace LtAmpDotNet.Models
{
    [MessageReceiverChannel<MessageChannelEnum>(MessageChannelEnum.FromAmplifier)]
    public class AmpStateModel : ObservableModel, IRecipient<PresetMessage>
    {
        public AmpStateModel() : base()
        {
            _presets = [];
            _definitions = [];
            _qaSlots = new int[2];
            IsActive = true;
            LoadDefinitions();
        }

        private DspUnitModelDefinitions _definitions;

        public DspUnitModelDefinitions Definitions
        {
            get => _definitions;
            set => SetProperty(ref _definitions, value);
        }

        private PresetModelCollection _presets;

        public PresetModelCollection Presets
        {
            get => _presets;
            set => SetProperty(ref _presets, value);
        }

        private int[] _qaSlots;

        public int[] QaSlots
        {
            get => _qaSlots;
            set => SetProperty(ref _qaSlots, value);
        }

        public void LoadDefinitions()
        {
            DspUnitModelDefinitions defs = [];
            foreach (DspUnitDefinition def in LtAmplifier.DspUnitDefinitions?.Where(x => x.FenderId != "Default")!)
            {
                NodeIdType dspUnitType = Enum.TryParse(def.Info?.SubCategory, out NodeIdType type) ? type : NodeIdType.none;
                if (dspUnitType == NodeIdType.none)
                {
                    defs[NodeIdType.stomp].Add(new DspUnitModel(def) { DspUnitType = NodeIdType.stomp });
                    defs[NodeIdType.mod].Add(new DspUnitModel(def) { DspUnitType = NodeIdType.mod });
                    defs[NodeIdType.delay].Add(new DspUnitModel(def) { DspUnitType = NodeIdType.delay });
                    defs[NodeIdType.reverb].Add(new DspUnitModel(def) { DspUnitType = NodeIdType.reverb });
                }
                else
                {
                    defs[dspUnitType].Add(new DspUnitModel(def) { DspUnitType = dspUnitType });
                }
            }
            Definitions = defs;
        }

        void IRecipient<PresetMessage>.Receive(PresetMessage message)
        {
            Presets.Add(message.Preset);
        }
    }
}