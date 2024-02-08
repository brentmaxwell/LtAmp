using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Model.Preset;

namespace LtAmpDotNet.Services.Messages
{
    public record DspUnitChangedMessage(NodeIdType DspUnitType, string FenderId) : IMessage;
}