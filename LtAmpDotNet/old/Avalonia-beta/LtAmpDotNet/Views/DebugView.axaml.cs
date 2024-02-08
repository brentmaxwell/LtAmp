using Avalonia.Controls;
using LtAmpDotNet.Lib;
using LtAmpDotNet.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LtAmpDotNet.Views
{
    public partial class DebugView : UserControl
    {
        //public ILtAmplifier Model { get; set; }
        public MainViewModel Model { get; set; }
        public DebugView(MainViewModel model)
        {
            InitializeComponent();
            Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var value = sender.GetType().GetProperty(e.PropertyName).GetValue(sender);
            var message = $"{e.PropertyName} = {value.ToString()}";
            DebugTextBox.Text += message;
        }

        private void _amplifier_MessageReceived(object? sender, Lib.Events.FenderMessageEventArgs e)
        {
            DebugTextBox.Text += e.Message.ToString();
        }
    }
}
