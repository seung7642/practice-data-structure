using System.Collections;
using System.Collections.Generic;

namespace MyDataStructure
{
    public class MyHashSet<T> : IEnumerable<T>
    {
        private MyList<T>[] _bucket;
        private IEqualityComparer<T> _equalityComparer;
        private int _count;

        public MyHashSet(IEqualityComparer<T> equalityComparer = null)
            : this(3, equalityComparer)
        {
        }

        public MyHashSet(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            var size = HashHelpers.GetPrime(capacity);
            _bucket = new MyList<T>[size];
            _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        }

        private MyList<T> FindBucketList(T item)
        {
            var hashCode = (_equalityComparer.GetHashCode(item) & 0x7fffffff);
            var index = hashCode % _bucket.Length;
            return _bucket[index];
        }

        private void Resize(int capacity)
        {
            var newSize = HashHelpers.GetPrime(capacity);
            var newBucket = new MyList<T>[newSize];

            for (var i = 0; i < _bucket.Length; i++) {
                var list = _bucket[i];
                if (list != null) {
                    foreach (var item in list) {
                        var hashCode = (_equalityComparer.GetHashCode(item) & 0x7fffffff);
                        var index = hashCode % newSize;
                        if (newBucket[index] == null) {
                            newBucket[index] = new MyList<T>();
                        }

                        newBucket[index].Add(item);
                    }
                }
            }

            this._bucket = newBucket;
        }

        public bool Contains(T item)
        {
            var list = FindBucketList(item);
            if (list == null) {
                return false;
            }

            foreach (var element in list) {
                
            }

            //return list.Contains(e => _equalityComparer.Equals(e, item));
            return false;
        }

        public bool Add(T item)
        {
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR) {
                Resize(_bucket.Length * HashHelpers.PRIME_FACTOR);
            }

            int hashCode = (_equalityComparer.GetHashCode(item) & 0x7fffffff);
            int index = hashCode % _bucket.Length;
            var list = _bucket[index];
            if (list == null) {
                list = new MyList<T>();
                _bucket[index] = list;
            }

            if (!_bucket[index].Contains(item)) {
                _bucket[index].Add(item);
                _count++;
                return true;
            }

            return false;
        }

        public bool Remove(T item)
        {
            var list = FindBucketList(item);
            if (list != null) {
                if (list.Remove(item)) {
                    _count--;
                    return true;
                }
            }

            return false;
        }

        public T FindEntry(T key)
        {
            // TODO
            var list = FindBucketList(key);
            if (list != null) {

            }

            // return null;
            return key;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyHashSetEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MyHashSetEnumerator<T> : IEnumerator<T>
    {
        private MyHashSet<T> _hashSet;
        private IEnumerator<T> _iterator;
        private int _index;

        public MyHashSetEnumerator(MyHashSet<T> hashSet)
        {
            _hashSet = hashSet;
            _index = 0;
            _iterator = FindNextIterator();
        }

        public IEnumerator<T> FindNextIterator()
        {
            // var list = _hashSet[_index++];
            // if (list != null) {
            //     return list.GetEnumerator();
            // }

            return null;
        }

        public T Current { get; }
        public bool MoveNext()
        {
            while (_iterator != null && !_iterator.MoveNext()) {
                _iterator = FindNextIterator();
            }

            return (_iterator != null);
        }

        public void Reset()
        {
            _index = 0;
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}