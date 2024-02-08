using Avalonia.Controls;
using Avalonia.Data;
using LtAmpDotNet.Controls;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Views
{
    public partial class PresetView : UserControlBase<PresetViewModel>
    {
        public PresetView()
        {
            InitializeComponent();
            SizeChanged += PresetView_SizeChanged;
        }

        private void PresetView_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            if(e.WidthChanged)
            {
                this.FindControl<FixedWrapPanel>("DspUnitsPanel").ItemsPerLine = e.NewSize.Width > 800 ? 2 : 1;
            }
        }
    }
}
