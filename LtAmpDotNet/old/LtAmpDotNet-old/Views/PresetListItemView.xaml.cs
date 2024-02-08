namespace LtAmpDotNet.Views;
using LtAmpDotNet.Models;

public partial class PresetList : ContentView
{
    public static readonly BindableProperty ListItemsProperty = BindableProperty.Create(
        nameof(ListItems), typeof(List<Preset?>), typeof(PresetList), string.Empty
    );

    public List<Preset?> ListItems
    {
        get { return (List<Preset?>)GetValue(PresetList.ListItemsProperty); }
        set { SetValue(PresetList.ListItemsProperty, value); }
    }
    public PresetList()
	{
		InitializeComponent();
	}
}