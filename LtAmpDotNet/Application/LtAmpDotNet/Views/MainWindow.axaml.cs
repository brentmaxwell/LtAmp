using LtAmpDotNet.Base;

namespace LtAmpDotNet.Views
{
    public partial class MainWindow : ViewWindowBase
    {
        public MainWindow(object dataContext) : this()
        {
            DataContext = dataContext;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}