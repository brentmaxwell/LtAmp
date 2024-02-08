using Avalonia.Controls;

namespace LtAmpDotNet.Base
{
    public class UserControl<T> : UserControl
    {
        public T ViewModel
        {
            get => (T)DataContext;
            set => DataContext = value;
        }
    }

    //public class SelectingUserControl<T> : UserControl
    //{

    //    /// <summary>
    //    /// Defines the <see cref="SelectedIndex"/> property.
    //    /// </summary>
    //    public static readonly DirectProperty<SelectingItemsControl, int> SelectedIndexProperty =
    //        AvaloniaProperty.RegisterDirect<SelectingItemsControl, int>(
    //            nameof(SelectedIndex),
    //            o => o.SelectedIndex,
    //            (o, v) => o.SelectedIndex = v,
    //            unsetValue: -1,
    //            defaultBindingMode: BindingMode.TwoWay);

    //    public static readonly DirectProperty<SelectingItemsControl, object?> SelectedItemProperty =
    //        AvaloniaProperty.RegisterDirect<SelectingItemsControl, object?>(
    //            nameof(SelectedItem),
    //            o => o.SelectedItem,
    //            (o, v) => o.SelectedItem = v,
    //            defaultBindingMode: BindingMode.TwoWay, enableDataValidation: true);

    //    /// <summary>
    //    /// Defines the <see cref="SelectedValue"/> property
    //    /// </summary>
    //    public static readonly StyledProperty<object?> SelectedValueProperty =
    //        AvaloniaProperty.Register<SelectingItemsControl, object?>(nameof(SelectedValue),
    //            defaultBindingMode: BindingMode.TwoWay);

    //    /// <summary>
    //    /// Defines the <see cref="SelectedValueBinding"/> property
    //    /// </summary>
    //    public static readonly StyledProperty<IBinding?> SelectedValueBindingProperty =
    //        AvaloniaProperty.Register<SelectingItemsControl, IBinding?>(nameof(SelectedValueBinding));

    //    /// <summary>
    //    /// Event that should be raised by containers when their selection state changes to notify
    //    /// the parent <see cref="SelectingItemsControl"/> that their selection state has changed.
    //    /// </summary>
    //    public static readonly RoutedEvent<RoutedEventArgs> IsSelectedChangedEvent =
    //        RoutedEvent.Register<SelectingItemsControl, RoutedEventArgs>(
    //            "IsSelectedChanged",
    //            RoutingStrategies.Bubble);

    //    /// <summary>
    //    /// Defines the <see cref="SelectionChanged"/> event.
    //    /// </summary>
    //    public static readonly RoutedEvent<SelectionChangedEventArgs> SelectionChangedEvent =
    //        RoutedEvent.Register<SelectingItemsControl, SelectionChangedEventArgs>(
    //            nameof(SelectionChanged),
    //            RoutingStrategies.Bubble);

    //    static SelectingItemsControl()
    //    {
    //        IsSelectedChangedEvent.AddClassHandler<SelectingItemsControl>((x, e) => x.ContainerSelectionChanged(e));
    //    }

    //    /// <summary>
    //    /// Occurs when the control's selection changes.
    //    /// </summary>
    //    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged
    //    {
    //        add => AddHandler(SelectionChangedEvent, value);
    //        remove => RemoveHandler(SelectionChangedEvent, value);
    //    }

    //    public int SelectedIndex
    //    {
    //        get
    //        {
    //            // When a Begin/EndInit/DataContext update is in place we return the value to be
    //            // updated here, even though it's not yet active and the property changed notification
    //            // has not yet been raised. If we don't do this then the old value will be written back
    //            // to the source when two-way bound, and the update value will be lost.
    //            if (_updateState is not null)
    //            {
    //                return _updateState.SelectedIndex.HasValue ?
    //                    _updateState.SelectedIndex.Value :
    //                    TryGetExistingSelection()?.SelectedIndex ?? -1;
    //            }

    //            return Selection.SelectedIndex;
    //        }
    //        set
    //        {
    //            if (_updateState is object)
    //            {
    //                _updateState.SelectedIndex = value;
    //            }
    //            else
    //            {
    //                Selection.SelectedIndex = value;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the selected item.
    //    /// </summary>
    //    public object? SelectedItem
    //    {
    //        get
    //        {
    //            // See SelectedIndex getter for more information.
    //            if (_updateState is not null)
    //            {
    //                return _updateState.SelectedItem.HasValue ?
    //                    _updateState.SelectedItem.Value :
    //                    TryGetExistingSelection()?.SelectedItem;
    //            }

    //            return Selection.SelectedItem;
    //        }
    //        set
    //        {
    //            if (_updateState is object)
    //            {
    //                _updateState.SelectedItem = value;
    //            }
    //            else
    //            {
    //                Selection.SelectedItem = value;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the <see cref="IBinding"/> instance used to obtain the 
    //    /// <see cref="SelectedValue"/> property
    //    /// </summary>
    //    [AssignBinding]
    //    [InheritDataTypeFromItems(nameof(ItemsSource))]
    //    public IBinding? SelectedValueBinding
    //    {
    //        get => GetValue(SelectedValueBindingProperty);
    //        set => SetValue(SelectedValueBindingProperty, value);
    //    }

    //    /// <summary>
    //    /// Gets or sets the value of the selected item, obtained using 
    //    /// <see cref="SelectedValueBinding"/>
    //    /// </summary>
    //    public object? SelectedValue
    //    {
    //        get => GetValue(SelectedValueProperty);
    //        set => SetValue(SelectedValueProperty, value);
    //    }
    //}
}
