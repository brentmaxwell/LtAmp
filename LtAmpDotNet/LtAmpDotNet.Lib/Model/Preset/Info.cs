using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Lib.Model.Preset
{
    public class Info : ObservableAmpData
    {
        private string _author;

        [JsonProperty("author")]
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        private int _bpm;

        [JsonProperty("bpm")]
        public int BPM
        {
            get => _bpm;
            set => SetProperty(ref _bpm, value);
        }

        private int _createdAt;

        [JsonProperty("created_at")]
        public int CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        private string _displayName;

        [JsonProperty("displayName")]
        public string DisplayNameRaw
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        [JsonIgnore]
        public string[] DisplayName
        {
            get => Enumerable.Range(0, _displayName.Length / 8).Select(i => _displayName.Substring(i * 8, 8).Trim()).ToArray();
            set
            {
                if (value.Length == 2)
                {
                    _displayName = value[0].PadRight(8) + value[1].PadRight(8);
                }
            }
        }

        public string FormattedDisplayName
        {
            get
            {
                return string.Join(" ", DisplayName);
            }
        }

        private bool _isFactoryDefault;

        [JsonProperty("is_factory_default")]
        public bool IsFactoryDefault
        {
            get => _isFactoryDefault;
            set => SetProperty(ref _isFactoryDefault, value);
        }

        private Guid _presetId;

        [JsonProperty("preset_id")]
        public Guid PresetId
        {
            get => _presetId;
            set => SetProperty(ref _presetId, value);
        }

        private string _productId;

        [JsonProperty("product_id")]
        public string ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }

        private string _sourceId;

        [JsonProperty("source_id")]
        public string SourceId
        {
            get => _sourceId;
            set => SetProperty(ref _sourceId, value);
        }

        private long _timestamp;

        [JsonProperty("timestamp")]
        public long Timestamp
        {
            get => _timestamp;
            set => SetProperty(ref _timestamp, value);
        }
    }
}
