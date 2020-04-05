using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListyIterator
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private List<T> elements;
        private int index;
        public ListyIterator(List<T> elemenets)
        {
            this.elements = elemenets;
            this.index = 0;
        }

        public bool Move()
        {
            bool isNextElement = this.HasNext();
            if (isNextElement)
            {
                this.index++;
                return true;
            }
            return HasNext();
        }
        public bool HasNext()
        {
            if (this.index < elements.Count - 1)
            {
                return true;
            }
            return false;
        }

        public string Print()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }
            return $"{elements[index]}";
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in elements)
            {
                yield return item; 
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
