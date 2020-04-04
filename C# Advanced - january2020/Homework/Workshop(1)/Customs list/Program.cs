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
            var list = new CustomList<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            Debug.Assert(list[2] == 2);
            Debug.Assert(list.Count == 10);
            list.RemoveAt(2);
            Debug.Assert(list[2] == 3);
            Debug.Assert(list[3] == 4);
            Debug.Assert(list.Count == 9);
            list.Reverse();
            Debug.Assert(list.Count == 9);
            Debug.Assert(list[0] == 9);
            Debug.Assert(list[1] == 8);
            Debug.Assert(list[2] == 7);
        }
    }
}
