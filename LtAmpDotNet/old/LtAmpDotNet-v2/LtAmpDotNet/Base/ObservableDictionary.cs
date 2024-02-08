using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LtAmpDotNet.Base
{
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, INotifyCollectionChanged, INotifyPropertyChanged where TKey : notnull
    {
        protected IDictionary<TKey, TValue> Items { get; }

        #region Events

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructors

        public ObservableDictionary()
        {
            Items = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            Items = new Dictionary<TKey, TValue>(dictionary);
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            Items = new Dictionary<TKey, TValue>(comparer);
        }

        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            Items = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            Items = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        public ObservableDictionary(int capacity)
        {
            Items = new Dictionary<TKey, TValue>(capacity);
        }

        #endregion

        #region IDictionary<TKey, TValue>

        public TValue this[TKey key]
        {
            get => Items[key];
            set
            {
                if (InsertObject(
                    key: key,
                    value: value,
                    appendMode: AppendMode.Replace,
                    oldValue: out var oldItem))
                {
                    OnCollectionChanged(
                        action: NotifyCollectionChangedAction.Replace,
                        newItem: new KeyValuePair<TKey, TValue>(key, value),
                        oldItem: new KeyValuePair<TKey, TValue>(key, oldItem));
                }
                else
                {
                    OnCollectionChanged(
                        action: NotifyCollectionChangedAction.Add,
                        changedItem: new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        public ICollection<TKey> Keys => Items.Keys;

        public ICollection<TValue> Values => Items.Values;

        public int Count => Items.Count;

        public bool IsReadOnly => Items.IsReadOnly;

        public void Add(TKey key, TValue value) => Add(item: new KeyValuePair<TKey, TValue>(key, value));

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            InsertObject(
                key: item.Key,
                value: item.Value,
                appendMode: AppendMode.Add);

            OnCollectionChanged(
                action: NotifyCollectionChangedAction.Add,
                changedItem: item);
        }

        public void Clear()
        {
            if (!Items.Any())
            {
                return;
            }

            var removedItems = Items.ToList();
            Items.Clear();
            OnCollectionChanged(
                action: NotifyCollectionChangedAction.Reset,
                newItems: null,
                oldItems: removedItems);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => Items.Contains(item);

        public bool ContainsKey(TKey key) => Keys.Contains(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Items.CopyTo(array: array, arrayIndex: arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Items.GetEnumerator();

        public bool Remove(TKey key)
        {
            if (Items.TryGetValue(key, out var value))
            {
                Items.Remove(key);
                OnCollectionChanged(
                    action: NotifyCollectionChangedAction.Remove,
                    changedItem: new KeyValuePair<TKey, TValue>(key, value));
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Items.Remove(item))
            {
                OnCollectionChanged(
                    action: NotifyCollectionChangedAction.Remove,
                    changedItem: item);
                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => Items.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

        #endregion

        private bool InsertObject(TKey key, TValue value, AppendMode appendMode) => InsertObject(key, value, appendMode, out _);

        private bool InsertObject(TKey key, TValue value, AppendMode appendMode, out TValue? oldValue)
        {
            oldValue = default;

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Items.TryGetValue(key, out var item))
            {
                if (appendMode == AppendMode.Add)
                {
                    throw new ArgumentException("Item with the same key has already been added");
                }

                if (Equals(item, value))
                {
                    return false;
                }

                Items[key] = value;
                oldValue = item;
            }
            else
            {
                Items[key] = value;
            }
            return true;
        }

        internal enum AppendMode
        {
            Add,
            Replace
        }

        #region EventHandlers

        protected void OnPropertyChanged()
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Items[]");
            OnPropertyChanged(nameof(Keys));
            OnPropertyChanged(nameof(Values));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                OnPropertyChanged();
            }

            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        protected void OnCollectionChanged()
        {
            OnPropertyChanged();
            var handler = CollectionChanged;
            handler?.Invoke(
                this, new NotifyCollectionChangedEventArgs(
                    action: NotifyCollectionChangedAction.Reset));
        }

        protected void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
        {
            OnPropertyChanged();
            var handler = CollectionChanged;
            handler?.Invoke(
                this, new NotifyCollectionChangedEventArgs(
                    action: action,
                    changedItem: changedItem));
        }

        protected void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
        {
            OnPropertyChanged();
            var handler = CollectionChanged;
            handler?.Invoke(
                this, new NotifyCollectionChangedEventArgs(
                    action: action,
                    newItem: newItem,
                    oldItem: oldItem));
        }

        protected void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
        {
            OnPropertyChanged();
            var handler = CollectionChanged;
            handler?.Invoke(
                this, new NotifyCollectionChangedEventArgs(
                    action: action,
                    changedItems: newItems));
        }

        protected void OnCollectionChanged(NotifyCollectionChangedAction action, IList? newItems, IList oldItems)
        {
            OnPropertyChanged();
            var handler = CollectionChanged;
            handler?.Invoke(
                this, new NotifyCollectionChangedEventArgs(
                    action: action,
                    newItems: newItems,
                    oldItems: oldItems));
        }

        #endregion
    }
}
