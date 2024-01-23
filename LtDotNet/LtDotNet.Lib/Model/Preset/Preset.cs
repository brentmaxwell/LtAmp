using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LtDotNet.Lib.Model.Preset
{
    public class Preset : ObservableAmpData, INotifyPropertyChanged
    {
        [JsonProperty("data")]
        public string Raw { get; set; }

        private AudioGraph _audioGraph;

        [JsonProperty("audioGraph")]
        public AudioGraph AudioGraph
        {
            get => _audioGraph;
            set
            {
                SetProperty(ref _audioGraph, value);
                ObserveChildProperty(_audioGraph);
            }
        }

        private Info _info;

        [JsonProperty("info")]
        public Info Info
        {
            get => _info;
            set
            {
                SetProperty(ref _info, value);
                ObserveChildProperty(_info);
            }
        }

        private string _nodeId;

        [JsonProperty("nodeId")]
        public string NodeId
        {
            get => _nodeId;
            set => SetProperty(ref _nodeId, value);
        }

        private string _nodeType;

        [JsonProperty("nodeType")]
        public string NodeType
        {
            get => _nodeType;
            set => SetProperty(ref _nodeType, value);
        }

        private int _numOfInputs;

        [JsonProperty("numInputs")]
        public int NumOfInputs
        {
            get => _numOfInputs;
            set => SetProperty(ref _numOfInputs, value);
        }

        private int _numOfOutputs;

        [JsonProperty("numOutputs")]
        public int NumOfOutputs
        {
            get => _numOfOutputs;
            set => SetProperty(ref _numOfOutputs, value);
        }

        private string _version;

        [JsonProperty("version")]
        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        [JsonIgnore]
        public string[] DisplayName => Info?.DisplayName;

        [JsonIgnore]
        public string FormattedDisplayName => Info?.FormattedDisplayName;

        [JsonIgnore]
        public string TwoLineDisplayName
        {
            get
            {
                return Info?.DisplayName[0].Trim() + "\n" + Info.DisplayName[1].Trim();
            }
        }

        public static Preset FromString(string json)
        {
            return JsonConvert.DeserializeObject<Preset>(json);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
