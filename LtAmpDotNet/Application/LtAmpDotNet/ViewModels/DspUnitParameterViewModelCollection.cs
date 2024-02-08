using LtAmpDotNet.Lib.Model.Preset;
using net.thebrent.dotnet.helpers.Collections;
using System;
using System.ComponentModel;

namespace LtAmpDotNet.ViewModels
{
    public class DspUnitParameterViewModelCollection : ObservableKeyedCollection<string, DspUnitParameterViewModel>
    {
        public new event EventHandler<PropertyChangedEventArgs>? PropertyChanged;

        public NodeIdType DspUnitType { get; set; }

        public DspUnitParameterViewModelCollection(NodeIdType dspUnitType) : this()
        {
            DspUnitType = dspUnitType;
        }

        public DspUnitParameterViewModelCollection() : base((item) => item.Model.ControlId)
        {
            CollectionChanged += DspUnitParameterViewModelCollection_CollectionChanged;
        }

        private void DspUnitParameterViewModelCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            e.NewItems.ForEach<DspUnitParameterViewModel>(x => x.PropertyChanged += Item_PropertyChanged);
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}