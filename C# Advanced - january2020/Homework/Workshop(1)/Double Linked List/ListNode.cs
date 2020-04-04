namespace DoublyLinkedList
{
    public class ListNode<T>
    {
        public ListNode(T value)
        {
            this.Value = value; //държи стойността
        }
        public T Value { get; set; }
        public ListNode<T> NextNode { get; set; } //рекутсивно извикване, реферира инстанция на същия клас
        public ListNode<T> PreviousNode { get; set; }
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
