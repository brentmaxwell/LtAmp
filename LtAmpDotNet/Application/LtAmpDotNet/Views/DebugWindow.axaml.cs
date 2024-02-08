using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using LtAmpDotNet.Base;
using LtAmpDotNet.Services.Messages;
using LtAmpDotNet.Services.Midi;

namespace LtAmpDotNet.Views
{
    public partial class DebugWindow : ViewWindowBase,
        IRecipient<FenderLtMessage>
    {
        public IMidiService MidiService { get; set; }

        public DebugWindow()
        {
            InitializeComponent();
            MidiService = App.Current.ResolveService<IMidiService>();
            Opened += DebugWindow_Opened;
            Closing += DebugWindow_Closing;
        }

        private void DebugWindow_Opened(object? sender, System.EventArgs e)
        {
            StrongReferenceMessenger.Default.RegisterAll(this);
        }

        private void DebugWindow_Closing(object? sender, Avalonia.Controls.WindowClosingEventArgs e)
        {
            StrongReferenceMessenger.Default.UnregisterAll(this);
        }

        public void AddToTextBox(string text)
        {
            Dispatcher.UIThread.Invoke<bool>(callback: () =>
            {
                if (this.IsVisible)
                {
                    DebugTextBox.Text += text + "\n";
                    return true;
                }
                return false;
            });
        }

        public void Receive(FenderLtMessage message)
        {
            Do(()
                    =>
                    {
                        if (this.IsVisible)
                        {
                            DebugTextBox.Text += (message.Direction == MessageDirection.Input ? "<<" : ">>") + message.Message + "\n";
                        }
                    });
        }
    }
}