using LtAmpDotNet.Base;
using LtAmpDotNet.Lib.Models.Protobuf;

namespace LtAmpDotNet.Services.Messages
{
    public record FenderLtMessage(MessageDirection Direction, FenderMessageLT Message) : IMessage;

    public enum MessageDirection
    {
        Unknown = 0,
        Input = 1,
        Output = 2,
    }
}