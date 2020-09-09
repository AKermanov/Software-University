namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public Stack()
        {
            this._top = null;
            this.Count = 0;
        }

        public Stack(Node<T> top)
        {
            this._top = top;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = this._top;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Peek()
        {
            this.ValideteIfEmpty();
            return this._top.Value;
        }

        public T Pop()
        {
            this.ValideteIfEmpty();

            Node<T> toReturn = this._top;
            this._top = this._top.Next;
            this.Count--;

            return toReturn.Value;
        }

      
        public void Push(T item)
        {
            Node<T> toInsert = new Node<T>(item);
            toInsert.Next = this._top;
            this._top = toInsert;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this._top;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValideteIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }
        }

    }
}