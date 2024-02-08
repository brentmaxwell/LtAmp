namespace LtAmpDotNet.Views;

public partial class HeaderView : ContentView
{

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Text), typeof(string), typeof(HeaderView), string.Empty
	);

	public string Text
	{
		get { return (string)GetValue(HeaderView.TextProperty); }
		set { SetValue(HeaderView.TextProperty, value); }
	}

	public HeaderView()
	{
		InitializeComponent();
	}
}