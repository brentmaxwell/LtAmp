using LtAmpDotNet.Base;
using LtAmpDotNet.Lib;
using LtAmpDotNet.ViewModels;
using System.Windows.Input;

namespace LtAmpDotNet
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            ((MainPageViewModel)BindingContext).LoadPreset(picker.SelectedIndex);
        }
    }
}
