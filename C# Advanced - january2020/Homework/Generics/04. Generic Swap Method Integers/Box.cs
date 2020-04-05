using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp90
{
    public class Box<T>
    {
        public Box(T value)
        {
            this.Values = new List<T>();
        }
        public List<T> Values { get; set; }

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
