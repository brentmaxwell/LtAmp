using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Lib.Model.Profile;
using net.thebrent.dotnet.helpers.Collections;
using System.Linq;

namespace LtAmpDotNet.Models
{
    public class DspUnitModel : ObservableModel
    {
        #region Properties and fields

        private NodeIdType _dspUnitType;

        public NodeIdType DspUnitType
        {
            get => _dspUnitType;
            set => SetProperty(ref _dspUnitType, value);
        }

        private string? _displayName;

        public string? DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private string? _fenderId;

        public string? FenderId
        {
            get => _fenderId;
            set => SetProperty(ref _fenderId, value);
        }

        private bool _hasBybass;

        public bool HasBypass
        {
            get => _hasBybass;
            set => SetProperty(ref _hasBybass, value);
        }

        private bool _isBypassing;

        public bool IsBypassing
        {
            get => _isBypassing;
            set => SetProperty(ref _isBypassing, value);
        }

        private DspUnitParameterModelCollection? _parameters;

        public DspUnitParameterModelCollection? Parameters
        {
            get => _parameters;
            set => SetProperty(ref _parameters, value);
        }

        #endregion Properties and fields

        #region Constructors

        public DspUnitModel()
        {
            Parameters = [];
        }

        public DspUnitModel(NodeIdType nodeIdType)
        {
            DspUnitType = nodeIdType;
        }

        public DspUnitModel(DspUnitDefinition def) : this()
        {
            if (def != null)
            {
                DisplayName = def.DisplayName;
                FenderId = def.FenderId;
                HasBypass = def.Ui?.HasBypass ?? false;
                Parameters = new DspUnitParameterModelCollection(DspUnitType);
                def.Ui?.UiParameters?.ForEach((value)
                    => Parameters.Add(
                        new DspUnitParameterModel(
                            DspUnitType,
                            value,
                            def.DefaultDspUnitParameters?.SingleOrDefault(x => x.Name == value.ControlId)?.Value)
                        )
                    );
            }
        }

        public DspUnitModel(Node model) : this(model.Definition)
        {
            Parameters?.ForEach((value)
                => value.Value = model.DspUnitParameters.SingleOrDefault(x => x.Name == value.ControlId).Value
            );
        }

        #endregion Constructors

        #region Methods

        public DspUnitModel Clone()
        {
            return new()
            {
                DspUnitType = DspUnitType,
                DisplayName = DisplayName,
                FenderId = FenderId,
                HasBypass = HasBypass,
                IsBypassing = IsBypassing,
                Parameters = Parameters?.Clone(),
            };
        }

        #endregion Methods
    }
}