using System.Collections;
using System.Collections.Generic;

namespace MyDataStructure
{
    public class MyDictionary<TKey, TValue> : IEnumerable<TKey>
    {
        private MyList<KeyValuePair<TKey, TValue>> _bucket;
        private IEqualityComparer<TKey> _equalityComparer;
        private int _index;

        public MyDictionary(MyList<KeyValuePair<TKey, TValue>> bucket, IEqualityComparer<TKey> equalityComparer, int index)
        {
            _bucket = bucket;
            _equalityComparer = equalityComparer;
            _index = index;
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}