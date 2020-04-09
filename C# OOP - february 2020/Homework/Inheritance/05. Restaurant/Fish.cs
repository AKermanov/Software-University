namespace Restaurant
{
   public class Fish : MainDish
    {
        public Fish(string name, decimal price, double grams)
         : base(name, price, grams)
        {
            this.Grams = 22;
        }
    }
}
