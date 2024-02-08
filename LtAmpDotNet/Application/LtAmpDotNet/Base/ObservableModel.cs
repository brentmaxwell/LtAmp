using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Services.Messages;
using net.thebrent.dotnet.helpers.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LtAmpDotNet.Base
{
    public abstract class ObservableModel : ObservableRecipient
    {
        protected MessageChannelEnum? MessageChannel { get; set; }

        protected List<string> IsReceiving = [];

        public ObservableModel(MessageChannelEnum messageChannel) : this()
        {
            MessageChannel = messageChannel;
        }

        public ObservableModel() : base(StrongReferenceMessenger.Default)
        {
            MessageReceiverChannelAttribute<MessageChannelEnum>? channelAttribute = GetType().GetCustomAttribute<MessageReceiverChannelAttribute<MessageChannelEnum>>();
            if (channelAttribute != null)
            {
                MessageChannel = channelAttribute.Channel;
            }
        }

        protected override void OnActivated()
        {
            if (MessageChannel.HasValue)
            {
                Messenger.RegisterAll(this, MessageChannel.Value.ValueOf());
            }
            else
            {
                Messenger.RegisterAll(this);
            }
            base.OnActivated();
        }

        protected TMessage Send<TMessage>(TMessage message) where TMessage : class
        {
            return Messenger.Send(message);
        }

        protected TMessage Send<TMessage, TToken>(TMessage message, TToken token)
            where TMessage : class
            where TToken : Enum
        {
            return Messenger.Send(message, token.ValueOf());
        }

        protected bool SetPropertyAnd<T>([NotNullIfNotNull(nameof(newValue))] ref T field, T newValue, Action<T> callback, [CallerMemberName] string? propertyName = null)
        {
            if (SetProperty(ref field, newValue, propertyName))
            {
                callback(field);
                return true;
            }
            return false;
        }

        protected bool SetPropertyAnd<TModel, T>(T oldValue, T newValue, TModel model, Action<TModel, T> setCallback, Action<TModel, T> callback, [CallerMemberName] string? propertyName = null)
            where TModel : class
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(callback);
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                return false;
            }
            OnPropertyChanging(propertyName);
            setCallback(model, newValue);
            callback(model, newValue);
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}