using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using net.thebrent.dotnet.helpers.Collections;
using System.Collections.Generic;

namespace LtAmpDotNet.Models
{
    public class PresetModel : ObservableModel
    {
        private int _presetIndex;

        public int PresetIndex
        {
            get => _presetIndex;
            set => SetProperty(ref _presetIndex, value);
        }

        private string? _displayName;

        public string? DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private Dictionary<NodeIdType, DspUnitModel> _dspUnits;

        public Dictionary<NodeIdType, DspUnitModel> DspUnits
        {
            get => _dspUnits;
            set => SetProperty(ref _dspUnits, value);
        }

        private bool _isPresetEdited;

        public bool IsPresetEdited
        {
            get => _isPresetEdited;
            set => SetProperty(ref _isPresetEdited, value);
        }

        public DspUnitModel AmpUnit => DspUnits[NodeIdType.amp];
        public DspUnitModel StompUnit => DspUnits[NodeIdType.stomp];
        public DspUnitModel ModUnit => DspUnits[NodeIdType.mod];
        public DspUnitModel DelayUnit => DspUnits[NodeIdType.delay];
        public DspUnitModel ReverbUnit => DspUnits[NodeIdType.reverb];

        public PresetModel()
        {
            _dspUnits = new()
            {
                { NodeIdType.amp, new DspUnitModel(NodeIdType.amp) },
                { NodeIdType.stomp, new DspUnitModel(NodeIdType.stomp) },
                { NodeIdType.mod, new DspUnitModel(NodeIdType.mod) },
                { NodeIdType.delay, new DspUnitModel(NodeIdType.delay) },
                { NodeIdType.reverb, new DspUnitModel(NodeIdType.reverb) }
            };
        }

        public PresetModel(Preset model)
        {
            _displayName = model.FormattedDisplayName;
            _dspUnits = [];
            model.AudioGraph.Nodes.ForEach(x => DspUnits.Add(x.NodeId, new DspUnitModel(x)));
        }

        public PresetModel Clone()
        {
            PresetModel newPreset = new()
            {
                DisplayName = DisplayName,
                PresetIndex = PresetIndex,
            };
            DspUnits.ForEach((k, v) => newPreset.DspUnits[k] = v.Clone());
            return newPreset;
        }
    }
}