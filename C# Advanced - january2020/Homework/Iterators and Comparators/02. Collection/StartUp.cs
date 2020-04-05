using System;
using System.Linq;

namespace ListyIterator
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            var command = Console.ReadLine();
            var cmdArgs = command
                .Split()
                .Skip(1)
                .ToList();
            ListyIterator<string> listyIterator = new ListyIterator<string>(cmdArgs);

            while (command != "END")
            {
                if (command == "Print")
                {
                    try
                    {
                        Console.WriteLine(listyIterator.Print()); 
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command == "Move")
                {
                    Console.WriteLine(listyIterator.Move()); 
                }
                else if (command == "HasNext" )
                {
                    Console.WriteLine(listyIterator.HasNext());
                }
                else if (command == "PrintAll")
                {
                    Console.WriteLine(string.Join(" ", listyIterator));
                }
                command = Console.ReadLine();
            }
            
        }
    }
}
