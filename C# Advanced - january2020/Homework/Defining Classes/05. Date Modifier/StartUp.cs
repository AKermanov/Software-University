﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;
using System.IO;


namespace DefiningClasses
{
   public class StartUp
    {
        public static void Main(string[] args)
        {
            string firstDate = Console.ReadLine();
            string secondDate = Console.ReadLine();
            double result = DateModifier.GetDifferentInDaysBetweenTwoDates(firstDate, secondDate);
            Console.WriteLine(result);
        }
    }
}
