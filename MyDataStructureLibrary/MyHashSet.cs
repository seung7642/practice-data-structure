using System.Collections;
using System.Collections.Generic;

namespace MyDataStructure
{
    public class MyHashSet<T> : IEnumerable<T>
    {
        private MyList<T>[] _bucket;
        private IEqualityComparer _equalityComparer;
        private int _index;

        public MyHashSet(MyList<T>[] bucket, IEqualityComparer equalityComparer, int index)
        {
            _bucket = bucket;
            _equalityComparer = equalityComparer;
            this._index = index;
        }

        public T FindEntry(T key)
        {
            
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

        public MyHashSetEnumerator(MyList<T>[] bucket, int index)
        {
            _bucket = bucket;
            _index = index;
            _iterator = FindNextIterator();
        }

        public IEnumerator<T> FindNextIterator()
        {
            
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