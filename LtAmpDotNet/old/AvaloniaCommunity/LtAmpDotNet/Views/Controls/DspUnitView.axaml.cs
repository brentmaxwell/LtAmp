using Avalonia.Controls;
using LtAmpDotNet.Models;

namespace LtAmpDotNet.Views.Controls
{
    public partial class DspUnitView : UserControl
    {
        public DspUnitModel model{ get; set; }
        public DspUnitView()
        {
            InitializeComponent();
        }
    }
}
