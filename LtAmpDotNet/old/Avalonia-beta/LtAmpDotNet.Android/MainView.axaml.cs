using Avalonia.Controls;

namespace LtAmpDotNet.Android;

public partial class MainView : UserControl
{
    public MainView()
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
}
