using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;

namespace LtAmpDotNet.Services.Messages
{
    public record ParameterChangedMessage(NodeIdType DspUnitType, string ControlId, dynamic ParameterValue) : IMessage;
}