using System;
using System.Collections.Generic;
using System.Text;

namespace ExerciseGenerics
{
   public class Box<T>
    {
        public Box(T value)
        {
            this.Value = value;
        }
        public T Value { get; set; }

        public override string ToString()
        {
            return $"System.Int32: {this.Value}";
        }
    }
}
