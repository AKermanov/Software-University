namespace Christmas
{
   public class Present
    {
        public Present(string name, double weight, string gender)
        {
            this.Name = name;
            this.Weight = weight;
            this.Gender = gender;
        }

        public string Name { get; private set; }
        public double Weight  { get; private set; }
        public string Gender { get; private set; }

        public override string ToString()
        {
            return $"Present {this.Name} ({this.Weight}) for a {this.Gender}"; 
        }
    }
}
