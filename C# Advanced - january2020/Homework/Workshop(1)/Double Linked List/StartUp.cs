using System;

namespace DoublyLinkedList
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            var doublyLinkedList = new DoublyLinkedList<string>();

            for (int i = 0; i < 4; i++)
            {
                doublyLinkedList.AddLast("i");
            }
            for (int i = 0; i < 4; i++)
            {
                doublyLinkedList.RemoveFirst();
            }
            
        }
    }
}
