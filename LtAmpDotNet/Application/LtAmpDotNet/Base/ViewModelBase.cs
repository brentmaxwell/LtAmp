using LtAmpDotNet.Services.Messages;
using System;

namespace LtAmpDotNet.Base;

public abstract class ViewModelBase : ObservableModel
{
    public ViewModelBase(MessageChannelEnum messageChannel) : base(messageChannel)
    {
    }

    public ViewModelBase() : base()
    {
    }
}