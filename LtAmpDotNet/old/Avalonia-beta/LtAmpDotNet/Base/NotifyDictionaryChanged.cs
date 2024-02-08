using System.Collections;
using System.Collections.Specialized;

namespace LtAmpDotNet.Base
{
    public interface INotifyDictionaryChanged<TKey> : INotifyCollectionChanged
    {
        new event NotifyDictionaryChangedEventHandler<TKey>? CollectionChanged;
    }

    public class NotifyDictionaryChangedEventArgs<TKey> : NotifyCollectionChangedEventArgs
    {
        private readonly TKey _newKey;
        private readonly TKey _oldKey;

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action) : base(action) { }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, object? changedItem) : base(action, changedItem, -1) { }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, object? changedItem, TKey key) : base(action, changedItem, -1)
        {
            _newKey = key;
        }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, IList? changedItems) : base(action, changedItems, -1) { }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, object? newItem, object? oldItem) : base(action, newItem, oldItem, -1) { }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, object? newItem, object? oldItem, TKey key) : base(action, newItem, oldItem, -1)
        {
            _newKey = _oldKey = key;
        }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems) : base(action, newItems, oldItems, -1) { }

        public NotifyDictionaryChangedEventArgs(NotifyCollectionChangedAction action, object? changedItem, TKey newKey, TKey oldKey) : base(action, changedItem)
        {
            _newKey = newKey;
            _oldKey = oldKey;
        }

        public TKey NewKey => _newKey;
        public TKey OldKey => _oldKey;
    }

    public delegate void NotifyDictionaryChangedEventHandler<TKey>(object? sender, NotifyDictionaryChangedEventArgs<TKey> e);
}
