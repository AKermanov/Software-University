namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidatwIndex(index);
                return this._items[this.Count - 1 - index];
            }
            set
            {
                this.ValidatwIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.GrowIfNeccessary();
            this._items[this.Count++] = item;
        }

     

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 1; i <= this.Count; i++)
            {
                if (this._items[this.Count - i].Equals(item))
                {
                    return i - 1; 
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.GrowIfNeccessary();
            this.ValidatwIndex(index);
            var indexToInser = this.Count - index;
            for (int i = this.Count; i > indexToInser; i--)
            {
                this._items[i] = this._items[i - 1];
            }

            this._items[indexToInser] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var indexOfElelemnt = this.IndexOf(item);

            if (indexOfElelemnt == -1)
            {
                return false;
            }

            this.RemoveAt(indexOfElelemnt);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidatwIndex(index);
            var indexOfEl = this.Count - 1 - index;

            for (int i = indexOfEl; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
      

        private void GrowIfNeccessary()
        {
            if (this.Count == this._items.Length)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newItems = new T[this._items.Length * 2];
            Array.Copy(this._items, newItems, this._items.Length);
            this._items = newItems;
        }

        private void ValidatwIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index is out of range!");
            }
        }
    }
}