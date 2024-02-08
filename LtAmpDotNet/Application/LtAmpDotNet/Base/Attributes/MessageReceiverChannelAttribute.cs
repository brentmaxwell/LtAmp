using System;

namespace LtAmpDotNet.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageReceiverChannelAttribute<T> : Attribute
    {
        public T Channel { get; set; }

        public MessageReceiverChannelAttribute(T channel)
        {
            Channel = channel;
        }
    }
}