using System;
using System.Collections;
using System.Collections.Generic;

namespace MyDataStructure
{
    public class MyList<T> : IEnumerable<T>
    {
        private T[] _array;        // 할당된 배열을 가리키는 참조변수
        private int _size;         // 현재 저장된 원소 개수
        private int position = -1;
        private IEqualityComparer<T> _equalityComparer;

        public MyList()
            : this(4)
        {
        }

        public MyList(int capacity)
        {
            this._size = 0;
            this._array = new T[capacity];
        }

        public MyList(IEqualityComparer<T> equalityComparer = null)
            : this(4, equalityComparer)
        {
        }

        public MyList(int capacity, IEqualityComparer<T> equalityComparer = null)
        {
            this._size = 0;
            this._array = new T[capacity];


            this._equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        }


        public int Count
        {
            get { return _size; }
        }

        public int Capacity
        {
            get { return _array.Length; }
            set
            {
                if (value < _size) // capacity는 size보다만 크다면 줄일 수도 있어야 한다.
                    throw new ArgumentOutOfRangeException();

                if (value != _array.Length) // 변경하려는 capacity값이 기존과 다를 때만 실행.
                {
                    if (value > 0) // 음수 capacity는 있을 수 없다.
                    {
                        var newItems = new T[Capacity * 2];
                        if (_size > 0)
                            Array.Copy(_array, newItems, _size);
                        _array = newItems;
                    }
                }
            }
        }

        // 외부에서 배열 요소에 접근을 위한 인덱서 프로퍼티
        public T this[int index]
        {
            get
            {
                if (index >= _size)
                    throw new IndexOutOfRangeException();
                return _array[index];
            }
            set
            {
                if (index >= _size)
                    throw new IndexOutOfRangeException();
                _array[index] = value;
            }
        }

        private void EnsureCapacity()
        {
            int capacity = _array.Length;
            if (_size >= capacity)
            {
                this.Capacity = capacity == 0 ? 4 : capacity * 2;
            }
        }

        // 배열의 마지막에 원소 추가
        public void Add(T element)
        {
            this.Insert(_size, element);
        }

        // 해당 위치에 원소 추가 =
        public void Insert(int index, T element)
        {
            if (index > _size)
                throw new ArgumentOutOfRangeException();

            // 배열 공간 체크, 부족할 시 resize
            EnsureCapacity();

            if (index <= _size)
            {
                Array.Copy(_array, index, _array, index + 1, _size - index);

                // 원소 추가
                _array[index] = element;
                _size++;
            }
        }

        // 해당 위치의 원소 삭제
        public void RemoveAt(int index)
        {
            this.RemoveRange(index, 1);
        }

        public void RemoveRange(int index, int count)
        {
            if (count > 0) // 지정한 인덱스의 앞으로는 삭제 불가능.
            {
                int i = _size;
                _size -= count;
                if (index < _size)
                {
                    Array.Copy(_array, index + count, _array, index, _size - index);
                }

                while (i > _size)
                    _array[--i] = default(T);
            }
        }

        public void CopyTo(Array array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, _size);
        }

        public void Swap(int i, int j)
        {
            T temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[Capacity];
            _array.CopyTo(newArray, 0);
            return newArray;
        }

        public int IndexOf(T item)
        {
            return IndexOf(item, 0, _size);
        }

        public int IndexOf(T item, int index)
        {
            return IndexOf(item, index, _size - index);
        }

        public int IndexOf(T item, int index, int count)
        {
            for (int i = index; i < (index + count); i++)
            {
                if (_equalityComparer.Equals(_array[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public int LastIndexOf(T item)
        {
            return LastIndexOf(item, _size - 1, _size);
        }

        public int LastIndexOf(T item, int index)
        {
            return LastIndexOf(item, index, index + 1);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            if (_size == 0) return -1;
            if (index >= _size) throw new ArgumentOutOfRangeException();
            if (count < 0 || index - count + 1 < 0) throw new ArgumentOutOfRangeException();

            for (int i = index; i > index - count; i--)
            {
                if (_equalityComparer.Equals(_array[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Remove(T item)
        {
            int idx = IndexOf(item);
            if (idx != -1)
            {
                RemoveAt(idx);
                return true;
            }
            return false;
        }

        public bool Contains(T item)
        {
            for (int index = 0; index < _size; index++)
            {
                if (_equalityComparer.Equals(_array[index], item))
                    return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(this);
        }

        // 내부 클래스라 public이든 private이든 상관없다.
        // 내부 클래스는 숨기고자 하는 목적도 있지만, 포함된 클래스에 종속적이다라는 의미도 내포한다.
        public class MyEnumerator : IEnumerator<T>
        {
            private MyList<T> _list;
            private int _index;

            public MyEnumerator(MyList<T> list)
            {
                _list = list;
                _index = -1;
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public T Current
            {
                get
                {
                    if (_index < 0 || _index >= _list.Count)
                        throw new ArgumentOutOfRangeException("Current의 인덱스 범위 에러.");

                    return _list[_index];
                }
            }

            public bool MoveNext()
            {
                if (++_index >= _list.Count)
                {
                    return false;
                }

                return true;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {

            }
        }
    }
}