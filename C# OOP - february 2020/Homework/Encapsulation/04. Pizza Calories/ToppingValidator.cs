using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza
{
    public static class ToppingValidator
    {
        private static Dictionary<string, double> toppings;
        public static bool IsValidTopping(string topping)
        {
            InitializeTopping();
            return toppings.ContainsKey(topping.ToLower());
        }
        public static double GetToppingModifire(string topping)
        {
            InitializeTopping();
            return toppings[topping.ToLower()];
        }
        private static void InitializeTopping()
        {
            if (toppings != null)
            {
                return;
            }

            toppings = new Dictionary<string, double>();
            toppings.Add("meat", 1.2);
            toppings.Add("veggies", 0.8);
            toppings.Add("cheese", 1.1);
            toppings.Add("sauce", 0.9);
        }
    }
}