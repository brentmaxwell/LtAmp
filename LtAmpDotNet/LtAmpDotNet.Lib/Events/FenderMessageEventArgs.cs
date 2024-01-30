using LtAmpDotNet.Lib.Models.Protobuf;
using static LtAmpDotNet.Lib.Models.Protobuf.FenderMessageLT;

namespace LtAmpDotNet.Lib.Events
{
    public class FenderMessageEventArgs : EventArgs
    {
        public TypeOneofCase MessageType => Message.TypeCase;
        public FenderMessageLT Message { get; set; }

        public FenderMessageEventArgs(){}
        
        public FenderMessageEventArgs(FenderMessageLT message) => Message = message;
    }
}