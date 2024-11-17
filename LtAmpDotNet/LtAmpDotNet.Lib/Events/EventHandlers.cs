using LtAmpDotNet.Lib.Models.Protobuf;
using static LtAmpDotNet.Lib.Models.Protobuf.FenderMessageLT;

namespace LtAmpDotNet.Lib.Events
{
    /// <summary>EventArgs for message events from the Amp</summary>
    public class FenderMessageEventArgs : EventArgs
    {
        /// <summary>The type of message contained</summary>
        public TypeOneofCase? MessageType => Message?.TypeCase;

        /// <summary>The message</summary>
        public FenderMessageLT? Message { get; set; }

        /// <summary>Creates a new instance of the class</summary>
        public FenderMessageEventArgs() { }

        /// <summary>Creates a new instance of the class with the message as the body</summary>
        /// <param name="message">The message to include as the body of the event args</param>
        public FenderMessageEventArgs(FenderMessageLT message)
        {
            Message = message;
        }
    }
}
