using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using LtAmpDotNet.Panels.DspUnitControlViews;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitControlViewModel : ViewModelBase
    {
        private readonly IDspUnitControlView viewControl;
        private NodeIdType _nodeType;
        private string _fenderId;
        private DspUnitDefinition _dspUnitDefinition;
        private Node _node;
        private readonly List<DspUnitParameterViewModel> _parameters;

        public NodeIdType NodeId
        {
            get => _nodeType;
            set => SetProperty(ref _nodeType, value);
        }

        public string Name => _node?.Definition.DisplayName;

        public string FenderId
        {
            get => _node?.FenderId;
            set
            {
                _node.FenderId = value;
                SetProperty(ref _fenderId, value);
            }
        }

        public bool? Bypass
        {
            get => _dspUnitDefinition.Ui.HasBypass ? _node?.DspUnitParameters?.SingleOrDefault(x => x.Name == "bypass")?.Value : false;
            set
            {
                if (_dspUnitDefinition.Ui.HasBypass)
                {
                    bool? oldValue = Bypass;
                    _node.DspUnitParameters.SingleOrDefault(x => x.Name == "bypass").Value = value;
                    OnPropertyChanged("Node.DspUnitParameters");
                    OnValueChanged("Node.DspUnitParameters", oldValue, value);
                }
            }
        }

        public List<DspUnitParameterViewModel> Parameters => _node?.DspUnitParameters.Select(x =>
                                                                      new DspUnitParameterViewModel
                                                                      (
                                                                          _node.Definition!.Ui!.UiParameters.SingleOrDefault(y => y.ControlId == x.Name),
                                                                          x
                                                                      )).ToList();

        public DspUnitDefinition DspUnitDefinition
        {
            get => _dspUnitDefinition;
            set
            {
                _node = new Node(value, NodeId);
                SetProperty(ref _dspUnitDefinition, value);
            }
        }

        public Node Node
        {
            get => _node;
            set
            {
                SetProperty(ref _node, value);
                _dspUnitDefinition = value.Definition;
            }
        }

        public DspUnitControlViewModel() { }

        public DspUnitControlViewModel(NodeIdType type) : this(Node.Create(type)) { }

        public DspUnitControlViewModel(Node node)
        {
            Node = node;
            _nodeType = node.NodeId;
        }
    }
}
