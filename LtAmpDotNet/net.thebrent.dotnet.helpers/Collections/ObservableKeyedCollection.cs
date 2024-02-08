using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace net.thebrent.dotnet.helpers.Collections
{
    public class ObservableKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>, INotifyCollectionChanged, INotifyPropertyChanged where TKey : notnull
    {
        #region Fields

        private readonly Func<TItem, TKey>? _getKeyForItemDelegate;
        private readonly string? keyPropertyName;
        private bool _deferNotifyCollectionChanged = false;

        #endregion Fields

        #region Event handlers

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion INotifyCollectionChanged Members

        private readonly PropertyChangingEventHandler? ChildPropertyChanging;
        private readonly PropertyChangedEventHandler? ChildPropertyChanged;

        #endregion Event handlers

        #region Constructors

        public ObservableKeyedCollection(Func<TItem, TKey> getKeyForItemDelegate) : this(getKeyForItemDelegate, null)
        {
        }

        public ObservableKeyedCollection(string keyPropName) : this(null, keyPropName)
        {
        }

        public ObservableKeyedCollection(Func<TItem, TKey>? getKeyForItemDelegate = null, string? keyPropName = null)
        {
            if (getKeyForItemDelegate == null && keyPropName == null)
                throw new ArgumentException(@"getKeyForItemDelegate and KeyPropertyName cannot both be null.");

            keyPropertyName = keyPropName;
            _getKeyForItemDelegate = getKeyForItemDelegate;

            if (keyPropertyName != null &&
                typeof(TItem).GetInterface(nameof(INotifyPropertyChanged)) != null &&
                typeof(TItem).GetInterface(nameof(INotifyPropertyChanging)) != null)
            {
                ChildPropertyChanging = (o, e) =>
                {
                    if (e.PropertyName != keyPropertyName) return;
                    TItem item = (TItem)o!;
                    Dictionary?.Remove(GetKeyForItem(item));
                };
                ChildPropertyChanged = (o, e) =>
                {
                    if (e.PropertyName != keyPropertyName) return;
                    TItem item = (TItem)o!;
                    Dictionary?.Add(GetKeyForItem(item), item);
                };
            }
        }

        #endregion Constructors

        #region Methods

        #region Public methods

        public int IndexOfKey(TKey key)
        {
            return Contains(key) ? IndexOf(this[key]) : -1;
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            _deferNotifyCollectionChanged = true;
            foreach (TItem? item in items) Add(item);//Add will call Insert internally.
            _deferNotifyCollectionChanged = false;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new IEnumerable<TItem> Items => base.Items;

        #endregion Public methods

        #region Protected methods

        protected override TKey GetKeyForItem(TItem item)
        {
            return _getKeyForItemDelegate != null
                ? _getKeyForItemDelegate(item)
                : keyPropertyName != null
                    ? (TKey)item!.GetType().GetProperty(keyPropertyName)!.GetValue(item)!
                    : throw new ArgumentException(@"getKeyForItemDelegate and KeyPropertyName cannot both be null.");
        }

        protected override void SetItem(int index, TItem newitem)
        {
            TItem olditem = base[index];
            UpdatePropChangeHandlers(olditem, false);
            UpdatePropChangeHandlers(newitem, true);
            base.SetItem(index, newitem);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newitem, olditem, index));
        }

        protected override void InsertItem(int index, TItem item)
        {
            UpdatePropChangeHandlers(item, true);
            base.InsertItem(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        protected override void ClearItems()
        {
            UpdatePropChangeHandlers(Items, null);
            base.ClearItems();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void RemoveItem(int index)
        {
            TItem item = this[index];
            UpdatePropChangeHandlers(item, false);
            base.RemoveItem(index);
            //using the overload without index causes binding problem when used in CompositeCollection.
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_deferNotifyCollectionChanged) return;
            NotifyChanges(e);
        }

        #endregion Protected methods

        #region Private methods

        private void UpdatePropChangeHandlers(TItem item, bool addOrRemove)
        {
            if (EqualityComparer<TItem>.Default.Equals(item, default) || keyPropertyName == null || ChildPropertyChanging == null || ChildPropertyChanged == null) return;

            if (addOrRemove)
            {
                ((INotifyPropertyChanging)item).PropertyChanging += ChildPropertyChanging;
                ((INotifyPropertyChanged)item).PropertyChanged += ChildPropertyChanged;
            }
            else
            {
                ((INotifyPropertyChanging)item).PropertyChanging -= ChildPropertyChanging;
                ((INotifyPropertyChanged)item).PropertyChanged -= ChildPropertyChanged;
            }
        }

        private void UpdatePropChangeHandlers(IEnumerable<TItem> olditems, IEnumerable<TItem>? newitems)
        {
            if (keyPropertyName == null || ChildPropertyChanging == null || ChildPropertyChanged == null) return;

            if (olditems != null)
            {
                foreach (TItem? olditem in olditems)
                {
                    ((INotifyPropertyChanging)olditem).PropertyChanging -= ChildPropertyChanging;
                    ((INotifyPropertyChanged)olditem).PropertyChanged -= ChildPropertyChanged;
                }
            }
            if (newitems != null)
            {
                foreach (TItem? newitem in newitems)
                {
                    ((INotifyPropertyChanging)newitem).PropertyChanging += ChildPropertyChanging;
                    ((INotifyPropertyChanged)newitem).PropertyChanged += ChildPropertyChanged;
                }
            }
        }

        private void NotifyChanges(NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler? collectionChangedHandler = CollectionChanged;
            PropertyChangedEventHandler? propertyChangedHandler = PropertyChanged;
            collectionChangedHandler?.Invoke(this, e);
            propertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        }

        #endregion Private methods

        #endregion Methods
    }
}