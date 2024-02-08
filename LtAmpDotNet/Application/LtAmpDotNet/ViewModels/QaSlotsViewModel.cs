using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Base.Attributes;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services.Messages;

namespace LtAmpDotNet.ViewModels
{
    [MessageReceiverChannel<MessageChannelEnum>(MessageChannelEnum.FromAmplifier)]
    public class QaSlotsViewModel : ViewModelBase, IRecipient<QaSlotsChangedMessage>
    {
        public QaSlotsViewModel(PresetModelCollection presets) : base()
        {
            Presets = presets;
        }

        public PresetModelCollection Presets { get; set; }

        private int _slot1;

        public int Slot1
        {
            get => _slot1;
            set => SetProperty(ref _slot1, value);
        }

        private int _slot2;

        public int Slot2
        {
            get => _slot2;
            set => SetProperty(ref _slot2, value);
        }

        public void Receive(QaSlotsChangedMessage message)
        {
            Slot1 = message.SlotA;
            Slot2 = message.SlotB;
        }
    }
}