using LtAmpDotNet.Base;

namespace LtAmpDotNet.Services.Messages
{
    public record QaSlotsChangedMessage(int SlotA, int SlotB) : IMessage;
}