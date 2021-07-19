using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Novel.Controls {

    public class TreeListViewCollection<T> : IList<T>, INotifyCollectionChanged
            where T : TreeListViewNode {

        private readonly List<T> _container = new List<T>();
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public T this[int index] {
            get {
                return _container[index];
            }

            set {
                _container[index] = value;
            }
        }

        public int Count {
            get {
                return _container.Count;
            }
        }

        public bool IsReadOnly {
            get {
                return false;
            }
        }


        public void Add(T item) {
            _container.Add(item);
            OnCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public void Clear() {
            _container.Clear();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public bool Contains(T item) {
            return _container.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            foreach (var item in _container) {
                array[arrayIndex] = item;
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return _container.GetEnumerator();
        }

        public int IndexOf(T item) {
            return _container.IndexOf(item);
        }

        public void Insert(int index, T item) {
            _container.Insert(index, item);
            OnCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public bool Remove(T item) {
            var ret = _container.Remove(item);
            if (ret) {

                OnCollectionChanged(NotifyCollectionChangedAction.Remove);
            }
            return ret;
        }

        public void RemoveAt(int index) {
            _container.RemoveAt(index);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _container.GetEnumerator();
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action) {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }

    }
}
