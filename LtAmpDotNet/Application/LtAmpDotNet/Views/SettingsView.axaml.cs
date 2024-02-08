using LtAmpDotNet.Base;
using LtAmpDotNet.ViewModels;

namespace LtAmpDotNet.Views
{
    public partial class SettingsView : ViewBase<SettingsViewModel>
    {
        public SettingsView()
        {
            ViewModel = App.Current.ResolveService<SettingsViewModel>();
            InitializeComponent();
            Loaded += SettingsView_Loaded;
            Unloaded += SettingsView_Unloaded;
        }

        private async void SettingsView_Unloaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await ViewModel.SaveAsync();
        }

        private async void SettingsView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await ViewModel.LoadAsync();
        }
    }
}