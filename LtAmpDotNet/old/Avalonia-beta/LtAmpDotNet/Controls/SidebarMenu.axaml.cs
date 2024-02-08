using Avalonia;
using Avalonia.Controls;
using System.Collections;

namespace LtAmpDotNet.Controls
{
    public partial class SidebarMenu : UserControl
    {
        public static readonly StyledProperty<bool> IsPaneOpenProperty = SplitView.IsPaneOpenProperty.AddOwner<SidebarMenu>();
        public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty = ListBox.ItemsSourceProperty.AddOwner<SidebarMenu>();

        public bool IsPaneOpen
        {
            get => GetValue(IsPaneOpenProperty);
            set => SetValue(IsPaneOpenProperty, value);
        }

        public IEnumerable? ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public SidebarMenu()
        {
            InitializeComponent();
        }
    }
}
