using LtAmpDotNet.Base;

namespace LtAmpDotNet.Services.Messages
{
    public record CurrentPresetChangedMessage(int PresetIndex) : IMessage;
}