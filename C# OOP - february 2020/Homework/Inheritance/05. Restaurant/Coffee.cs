
namespace Restaurant
{
   public class Coffee : HotBeverage
    {
        public Coffee(string name, decimal price, double milliliters, double caffeine)
           : base(name, price, milliliters)
        {
            this.CoffeeMilliliters = 50;
            this.CoffeePrice = 3.50m;
            this.Caffeine = caffeine;
        }
        public double Caffeine { get; private set; }
        public double CoffeeMilliliters { get; private set; }
        public decimal CoffeePrice { get;private set; }
    }
}
