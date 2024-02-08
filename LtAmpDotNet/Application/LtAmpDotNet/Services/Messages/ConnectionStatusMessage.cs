using LtAmpDotNet.Base;

namespace LtAmpDotNet.Services.Messages
{
    public record ConnectionStatusMessage(ConnectionStatus Connected) : IMessage;

    public enum ConnectionStatus
    {
        Disconnected = 0,
        Connecting = 1,
        Connected = 2
    }
}