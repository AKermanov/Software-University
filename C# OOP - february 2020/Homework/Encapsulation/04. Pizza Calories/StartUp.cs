using System;

namespace Pizza
{
    class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                string pizzaName = Console.ReadLine().Split()[1];
                string[] input = Console.ReadLine()
                      .Split();

                string flourType = input[1];
                string backingTech = input[2];
                double weight = double.Parse(input[3]);

                Dough dought = new Dough(flourType, backingTech, weight);
                Pizza pizza = new Pizza(pizzaName, dought);

                var command = Console.ReadLine();
                while (command != "END")
                {
                    var comdArgs = command.Split();
                    var typeTopping = comdArgs[1];
                    var weightTopping = double.Parse(comdArgs[2]);
                    var topping = new Topping(typeTopping, weightTopping);
                    pizza.AddTopping(topping);
                    command = Console.ReadLine();
                }
                Console.WriteLine($"{pizza.Name} - {pizza.GetTotalCalories:f2} Calories.");   
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            
			
        }
    }
}
