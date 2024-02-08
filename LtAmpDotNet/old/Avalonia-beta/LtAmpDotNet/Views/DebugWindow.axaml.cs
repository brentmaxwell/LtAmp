using Avalonia.Controls;
using Avalonia.Threading;
using LtAmpDotNet.Models;
using LtAmpDotNet.ViewModels;
using System;

namespace LtAmpDotNet.Views
{
    public partial class DebugWindow : Window
    {
        public AmpStateModel Model { get; set; }
        public DebugWindow(AmpStateModel model)
        {
            InitializeComponent();
            Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
            Model._amplifier.MessageReceived += _amplifier_MessageReceived;
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var value = sender.GetType().GetProperty(e.PropertyName).GetValue(sender);
            var message = $"{e.PropertyName} = {value.ToString()}";
            Dispatcher.UIThread.Invoke<bool>(callback: () => AddToTextBox(message));
        }

        private void _amplifier_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            Dispatcher.UIThread.Invoke<bool>(callback: () => AddToTextBox(e.Message.ToString()));
        }

        private bool AddToTextBox(string text)
        {
            DebugTextBox.Text += text + "\n";
            return true;
        }
    }
}
