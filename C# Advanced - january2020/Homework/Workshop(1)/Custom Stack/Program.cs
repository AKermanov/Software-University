using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace CustomDataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new CustomStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(string.Join(", ", stack.Where(x => x % 2 == 0)));
        }
    }
}
