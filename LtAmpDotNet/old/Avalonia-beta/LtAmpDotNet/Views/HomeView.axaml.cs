using Avalonia.Controls;
using Avalonia.Interactivity;

namespace LtAmpDotNet.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void PresetNameEdit_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            PresetNameDisplay.IsVisible = true;
            PresetNameEdit.IsVisible = false;
        }

        private void RenameMenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            PresetNameDisplay.IsVisible = false;
            PresetNameEdit.IsVisible = true;
        }

        private void ListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox ctl)
            {
                var flyout = ctl.GetInteractiveParent();
            }
        }
    }
}
