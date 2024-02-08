using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Models
{
    public class DspUnitModel : ObservableModel
    {
		private NodeIdType _nodeIdType;

		public NodeIdType NodeIdType
		{
			get => _nodeIdType;
			set => SetProperty(ref _nodeIdType, value);
		}

		private string _fenderId;
		public string FenderId
		{
			get => _fenderId;
			set => SetProperty(ref _fenderId, value);
		}

		private string _displayName;

		public string DisplayName
		{
			get => _displayName;
			set => SetProperty(ref _displayName, value);
		}
	}

    public class DspUnitModelCollection : KeyedCollection<string, DspUnitModel>
    {
		protected override string GetKeyForItem(DspUnitModel item) => item.FenderId;

        public DspUnitModelCollection() { }

        public DspUnitModelCollection(IEnumerable<DspUnitModel> model)
        {
            foreach (var item in model)
            {
                this.Add(item);
            }
        }
    }

    public class DspUnitDefinitionCollection : ObservableDictionary<NodeIdType, DspUnitModelCollection>
    {
        public DspUnitDefinitionCollection()
        {
            foreach (NodeIdType nodeType in Enum.GetValues(typeof(NodeIdType)))
            {
                Add(nodeType, new DspUnitModelCollection());
            }
        }

        public void Add(DspUnitModel value)
        {
            this[value.NodeIdType].Add(value);
        }
    }
}
