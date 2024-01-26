using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitControlViewModel : ViewModelBase
    {
        private string _nodeType;
        private string _fenderId;
        private DspUnitDefinition _dspUnitDefinition;
        private Node _node;
        
        public string NodeType
        {
            get => _nodeType;
            set => SetProperty(ref _nodeType, value);
        }

        public string Name
        {
            get => _node.Definition.DisplayName!;
        }

        public string FenderId
        {
            get => _node.FenderId;
            set
            {
                _node.FenderId = value;
                SetProperty(ref _fenderId, value);
            }
        }

        public DspUnitDefinition DspUnitDefinition
        {
            get => _dspUnitDefinition;
            set
            {
                SetProperty(ref _dspUnitDefinition, value);
                _node = new Node(value);
            }
        }

        public Node Node
        {
            get { return _node; }
            set
            {
                SetProperty(ref _node, value);
                _dspUnitDefinition = value.Definition;
            }
        }

        public DspUnitControlViewModel()
        {

        }

        public DspUnitControlViewModel(Node node)
        {
            Node = node;
        }
    }
}
