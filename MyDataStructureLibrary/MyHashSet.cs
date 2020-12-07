using System.Collections;
using System.Collections.Generic;

namespace MyDataStructure
{
    public class MyHashSet<T> : IEnumerable<T>
    {
        private MyList<T>[] _bucket;
        private IEqualityComparer _equalityComparer;
        private int _count;

        public MyHashSet(IEqualityComparer<T> equalityComparer = null)
            : this(3, equalityComparer)
        {
        }

        public MyHashSet(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            int size = HashHelpers.GetPrime(capacity);
            _bucket = new MyList<T>[size];
            _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        }

        private MyList<T> FindBucketList(T item)
        {
            int hashCode = (_equalityComparer.GetHashCode(item) & 0x7fffffff);
            int index = hashCode % _bucket.Length;
            return _bucket[index];
        }

        private void Resize(int capacity)
        {
            var newSize = HashHelpers.GetPrime(capacity);
            var newBucket = new MyList<T>[newSize];

            // 새 capacity에 대한 해싱 작업. (기존 버킷에 들어있던 모든 데이터에 대해)
            for (int i = 0; i < _bucket.Length; i++)
            {
                var list = _bucket[i];
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        int hashCode = (_equalityComparer.GetHashCode(item) & 0x7fffffff);
                        int index = hashCode % newSize;
                        if (newBucket[index] == null)
                        {
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
            if (list == null)
            {
                return null;
            }
            return list.Contains(e => _equalityComparer.Equals(e, item));
        }

        public bool Add(T item)
        {
            if (_count >= _bucket.Length * HashHelpers.RESIZE_FACTOR)
            {
                Resize(_bucket.Length * HashHelpers.PRIME_FACTOR);
            }

            int hashCode = (_equalityComparer.GetHashCode(item) & 0x7fffffff);
            int index = hashCode % _bucket.Length;
            var list = _bucket[index];
            if (list == null)
            {
                list = new MyList<T>();
                _bucket[index] = list;
            }

            if (!_bucket[index].Contains(item))
            {
                _bucket[index].Add(item);
                _count++;
                return true;
            }

            return false;
        }

        public bool Remove(T item)
        {
            var list = FindBucketList(item);
            if (list != null)
            {
                if (list.Remove(item))
                {
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
            if (list != null)
            {

            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new MyHashSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MyHashSetEnumerator<T> : IEnumerator<T>
    {
        private MyList<T>[] _bucket;
        private IEnumerator<T> _iterator;
        private int _index;

        public MyHashSetEnumerator(MyList<T>[] bucket)
        {
            _bucket = bucket;
            _index = 0;
            _iterator = FindNextIterator();
        }

        public IEnumerator<T> FindNextIterator()
        {
            var list = _bucket[_index++];
            if (list != null)
            {
                return list.GetEnumerator();
            }

            return null;
        }

        public T Current { get; }
        public bool MoveNext()
        {
            while (_iterator != null && !_iterator.MoveNext())
            {
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
            throw new System.NotImplementedException();
        }
    }
}