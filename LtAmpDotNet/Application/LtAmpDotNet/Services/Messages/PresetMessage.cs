using LtAmpDotNet.Base;
using LtAmpDotNet.Models;

namespace LtAmpDotNet.Services.Messages
{
    public record PresetMessage(PresetModel Preset) : IMessage;
}