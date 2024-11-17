using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;

namespace LtAmpDotNet.ViewModels
{
    public class CurrentPresetPanelViewModel : ViewModelBase
    {
        private string _presetName;
        private int? _bpm;
        private Preset _preset;
        private DspUnitControlViewModel _ampViewModel;
        private DspUnitControlViewModel _stompViewModel;
        private DspUnitControlViewModel _modViewModel;
        private DspUnitControlViewModel _delayViewModel;
        private DspUnitControlViewModel _reverbViewModel;

        public string PresetName
        {
            get => _presetName;
            set
            {
                Preset.Info.DisplayNameRaw = value;
                SetProperty(ref _presetName, value);
            }
        }

        public int? BPM
        {
            get => _bpm;
            set
            {
                Preset.Info.BPM = value;
                SetProperty(ref _bpm, value);
            }
        }

        public Preset Preset
        {
            get => _preset;
            set
            {
                if (value != null && value.Info != null && value.AudioGraph?.Nodes != null)
                {
                    _presetName = value.FormattedDisplayName;
                    _bpm = value.Info.BPM;
                    AmpViewModel = new DspUnitControlViewModel(value.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.amp));
                    StompViewModel = new DspUnitControlViewModel(value.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.stomp));
                    ModViewModel = new DspUnitControlViewModel(value.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.mod));
                    DelayViewModel = new DspUnitControlViewModel(value.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.delay));
                    ReverbViewModel = new DspUnitControlViewModel(value.AudioGraph.Nodes.SingleOrDefault(x => x.NodeId == NodeIdType.reverb));

                }
                SetProperty(ref _preset, value);
            }
        }

        public DspUnitControlViewModel AmpViewModel
        {
            get => _ampViewModel;
            set
            {
                SetProperty(ref _ampViewModel, value);
                ObserveChildProperty(_ampViewModel);
            }
        }

        public DspUnitControlViewModel StompViewModel
        {
            get => _stompViewModel;
            set
            {
                SetProperty(ref _stompViewModel, value);
                ObserveChildProperty(_ampViewModel);
            }
        }

        public DspUnitControlViewModel ModViewModel
        {
            get => _modViewModel;
            set
            {
                SetProperty(ref _modViewModel, value);
                ObserveChildProperty(_modViewModel);
            }
        }

        public DspUnitControlViewModel DelayViewModel
        {
            get => _delayViewModel;
            set
            {
                SetProperty(ref _delayViewModel, value);
                ObserveChildProperty(_delayViewModel);
            }
        }

        public DspUnitControlViewModel ReverbViewModel
        {
            get => _reverbViewModel;
            set
            {
                SetProperty(ref _reverbViewModel, value);
                ObserveChildProperty(_reverbViewModel);
            }
        }

        public CurrentPresetPanelViewModel(Preset preset)
        {
            Preset = preset;
        }
    }
}
