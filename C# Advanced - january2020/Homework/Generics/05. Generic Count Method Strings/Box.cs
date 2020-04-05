using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp90
{
    public class Box<T>
        where T : IComparable
    {
        public Box(T value)
        {
            this.Values = new List<T>();
        }
        public List<T> Values { get; set; }

        public int GreaterValuesThan(T targetItem)
            //where T : ICloneable
        {
            int counter = 0;
            foreach (var value in this.Values)
            {
                if (value.CompareTo(targetItem) > 0)
                {
                    counter++;
                }
               

            }

            return counter;
        }

        public void Swap(int a, int b)
        {
            bool isInRage = a >= 0 && a < this.Values.Count && b >= 0 && b < this.Values.Count;
            if (!isInRage)
            {
                throw new InvalidOperationException("Values are not in range!");
            }

            T tempValue = this.Values[a];
            this.Values[a] = this.Values[b];
            this.Values[b] = tempValue;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.Values)
            {
                sb.AppendLine($"{item.GetType()}: {item}");
            }

            string result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
