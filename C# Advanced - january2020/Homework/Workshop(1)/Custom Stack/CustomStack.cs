using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomDataStructures
{
   public class CustomStack<T> : IEnumerable<T>
    {
        private const int InitialCapacity = 4;
        private T[] items;
        private int count;
        public CustomStack()
        {
            this.count = 0;
            this.items = new T[InitialCapacity];
        }
        public int Count => this.count; //става само за read only property (няма сет)
        public void Push(T element)
        {
            EnsureCapacity();
            this.items[this.count] = element;
            this.count++;
        }
        public T Pop()
        {
            ThrowWhenEmpty();
            var lastElement = this.items[this.count - 1];
            this.count--;
            return lastElement;
        }
        public T Peek()
        {
            ThrowWhenEmpty();
            return this.items[this.count - 1];
        }
        public void ForEach(Action<T> action)
        {
            for (int i = this.count- 1; i <= 0; i--)
            {
                action(this.items[i]);
            }
        }
        private void ThrowWhenEmpty()
        {
            if (this.count == 0)
            {
                throw new Exception("Stakc is menpty!");
            }
        }
        private void EnsureCapacity() //Resize()
        {
            if (this.items.Length > this.Count)
            {
                return;
            }
            //Array.Copy(this.items, 0, tempArray, 0, this.items.Length); същото като for цикъла
            var tempArray = new T[2 * this.items.Length];
            for (int i = 0; i < this.items.Length; i++)
            {
                tempArray[i] = this.items[i];
            }

            this.items = tempArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }
    }
}
