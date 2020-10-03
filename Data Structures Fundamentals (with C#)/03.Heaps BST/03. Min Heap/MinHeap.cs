namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }
        public int Size => _elements.Count;

        public T Dequeue()
        {
            var firstElement = this.Peek();
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();

            return firstElement;
        }


        private int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }


        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp();
        }

        public T Peek()
        {
            this.EnsureNotEmptly();
            return this._elements[0];
        }
        private void EnsureNotEmptly()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Max heap is empty!");
            }
        }

        private void HeapifyUp()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = this.GetParentIndex(currentIndex);
            while (currentIndex > 0 &&
                   this.IsLess(currentIndex, parentIndex))
            {
                Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = this.GetParentIndex(currentIndex);
            }
        }

        private void HeapifyDown()
        {
            int index = 0;
            int lefCHildIndex = this.GetLeftChildIndex(0);
            while (lefCHildIndex < this.Size && this.IsGreater(index, lefCHildIndex))
            {
                int toSwapWith = lefCHildIndex;
                int rightChildIndex = this.GetRightChildIndex(index);

                if (rightChildIndex < this.Size && this.IsGreater(toSwapWith, rightChildIndex))
                {
                    toSwapWith = rightChildIndex;
                }

                this.Swap(toSwapWith, index);
                index = toSwapWith;
                lefCHildIndex = this.GetLeftChildIndex(index);
            }
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool IndexIsValid(int index)
        {
            return index < this.Size;
        }

        private bool IsGreater(int childIndex, int parentIndex)
        {
            return this._elements[childIndex].CompareTo(this._elements[parentIndex]) > 0;
        }

        private bool IsLess(int childIndex, int parentIndex)
        {
            return this._elements[childIndex].CompareTo(this._elements[parentIndex]) < 0;
        }

        private void Swap(int currentIndex, int parentIndex)
        {
            var temp = this._elements[currentIndex];
            this._elements[currentIndex] = this._elements[parentIndex];
            this._elements[parentIndex] = temp;
        }
    }
}
