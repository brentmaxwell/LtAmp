using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using LtAmpDotNet.Base;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Controls
{
    public partial class DspUnitView : UserControl<DspUnitViewModel>
    {
        public DspUnitView()
        {
            InitializeComponent();
            //EffectiveViewportChanged += DspUnitView_EffectiveViewportChanged;
        }

        //private void DspUnitView_EffectiveViewportChanged(object? sender, Avalonia.Layout.EffectiveViewportChangedEventArgs e)
        //{
        //    if (itemControl.ItemsPanelRoot != null)
        //    {
        //        var stackPanelControl = (StackPanel)itemControl.ItemsPanelRoot;
        //        var width = this.Bounds.Right - stackPanelControl.Bounds.Left;
        //        var numOfControls = ViewModel.Parameters.Count();
        //        stackPanelControl.Spacing = width / (numOfControls + 1);
        //    }
        //}



        private void TextBlock_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            var ctl = sender as Control;
            if (ctl != null)
            {
                FlyoutBase.ShowAttachedFlyout(ctl);
            }
        }
    }
}
