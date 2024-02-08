using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using LtAmpDotNet.Base;
using LtAmpDotNet.Models;
using LtAmpDotNet.Services;
using LtAmpDotNet.ViewModels;
namespace LtAmpDotNet.Views
{
    public partial class MainView2 : ViewBase<MainViewModel>
    {
        public MainView2(MainViewModel model) : this()
        {
            ViewModel = model;
        }

        public MainView2() : base()
        {
            InitializeComponent();
            AmplifierService ampService = App.Current.ResolveService<AmplifierService>();
            AmpStateModel ampStateModel = App.Current.ResolveService<AmpStateModel>();
            ampService.IsActive = true;
            Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ViewModel.Connect();
        }

        //public void FootswitchFlyout_PointerPressed(object sender, PointerPressedEventArgs args)
        //{
        //    if (sender is Control ctl)
        //    {
        //        FlyoutBase.ShowAttachedFlyout(ctl);
        //    }
        //}

        //private void Flyout_Closed(object? sender, System.EventArgs e)
        //{
        //}

        //private void MenuItem_PointerExited(object? sender, Avalonia.Input.PointerEventArgs e)
        //{
        //    //Menu2.UnselectAll();
        //}

        //private void SplitView_PaneClosing(object? sender, Avalonia.Interactivity.CancelRoutedEventArgs e)
        //{
        //    ViewModel.IsMenuOpen = false;
        //}
    }
}
