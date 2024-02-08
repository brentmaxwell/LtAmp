using Avalonia.Controls;
using Avalonia.Platform;
using LtAmpDotNet.Models;
using LtAmpDotNet.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LtAmpDotNet.Views;

public partial class MainView : UserControl
{
    private MainViewModel viewModel => (MainViewModel)DataContext;
    public MainView()
    {
        InitializeComponent();
        formPreset.SelectionChanged += FormPreset_SelectionChanged;
        //ViewModel.AmpState.PropertyChanged += AmpState_PropertyChanged;
    }

    private void FormPreset_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        viewModel.AmpState.CurrentPresetIndex = formPreset.SelectedIndex;
    }
}
