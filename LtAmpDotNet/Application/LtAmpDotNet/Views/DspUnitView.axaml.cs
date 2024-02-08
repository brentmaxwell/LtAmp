using Avalonia.Controls;
using Avalonia.Interactivity;
using LtAmpDotNet.Base;
using LtAmpDotNet.ViewModels;
using System;

namespace LtAmpDotNet.Views
{
    public partial class DspUnitView : ViewBase<DspUnitViewModel>
    {
        public static readonly RoutedEvent<RoutedEventArgs> ParameterValueChangedEvent =
            RoutedEvent.Register<DspUnitView, RoutedEventArgs>(nameof(ParameterValueChanged), RoutingStrategies.Bubble);

        public event EventHandler<RoutedEventArgs> ParameterValueChanged
        {
            add => AddHandler(ParameterValueChangedEvent, value);
            remove => RemoveHandler(ParameterValueChangedEvent, value);
        }

        public DspUnitView()
        {
            InitializeComponent();
            SizeChanged += DspUnitView_SizeChanged;
            //DspUnitParameterView.ParameterValueChangedEvent.AddClassHandler<DspUnitView>((x, e) => x.OnParameterValueChanged(e));
        }

        private void OnParameterValueChanged(object? sender, RoutedEventArgs e)
        {
        }

        private void DspUnitView_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
        }
    }
}