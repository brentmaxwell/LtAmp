using LtAmpDotNet.Base;

namespace LtAmpDotNet.Services.Messages
{
    public record MidiMessage(byte[] Message) : IMessage;
}