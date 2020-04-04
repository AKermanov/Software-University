using System;

namespace DoublyLinkedList
{
    public class DoublyLinkedList<T>
    {
        private ListNode<T> head;
        private ListNode<T> tail;
        public int Count { get; private set; }
        public void AddFirst(T element)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new ListNode<T>(element);
            }
            else
            {
                ListNode<T> newHead = new ListNode<T>(element);
                ListNode<T> oldHead = this.head;
                this.head = newHead;
                oldHead.PreviousNode = newHead;
                newHead.NextNode = oldHead;

            }
            this.Count++;
        }
        public void AddLast(T element)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new ListNode<T>(element);
            }
            else
            {
                ListNode<T> newTail = new ListNode<T>(element);
                ListNode<T> oldTail = this.tail;

                this.tail = newTail;
                oldTail.NextNode = newTail;
                newTail.PreviousNode = oldTail;
            }
            this.Count++;
        }
        public T RemoveFirst()
        {
             
            if (this.Count == 0)
            {
                throw new InvalidOperationException("List is empty!");
            }
            T removedElement = this.head.Value;
             if (this.Count == 1)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                ListNode<T> newHead = this.head.NextNode;
                newHead.PreviousNode = null;
                this.head = newHead;
            }
            this.Count--;
            return removedElement;
        }
        public T RemoveLast()
        {
             //има ли head дай ми валю, ако няма head върни ми null в променливата
            if (this.Count == 0 )
            {
                throw new InvalidOperationException("List is empty!");
            }
            T removedElement = this.tail.Value;
            if (this.Count == 1)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                ListNode<T> newTail = this.tail.PreviousNode;
                newTail.NextNode = null;
                this.tail = newTail;
            }
            this.Count--;
            return removedElement;
        }
        public void ForEach(Action<T> action) //функция от по-висок ред
        {
            ListNode<T> currentElement = this.head;

            while (currentElement != null)
            {
                action(currentElement.Value);
                currentElement = currentElement.NextNode;
            }
        }
        public T[] ToArray()
        {
            T[] arr = new T[this.Count];
            int cnt = 0;
            ListNode<T> currentEL = this.head;
            while (currentEL != null)
            {
                arr[cnt++] = currentEL.Value;
                currentEL = currentEL.NextNode;
            }
            return arr;
        }
    }
   
}
