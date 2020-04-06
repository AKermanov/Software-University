namespace Christmas
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Bag
    {
        private List<Present> presents;

        public Bag(string color, int capacity)
        {
            this.Color = color;
            this.Capacity = capacity;
            presents = new List<Present>();
        }

        
        public string Color { get; private set; }
        public int Capacity { get; private set; }

        public void Add(Present present)
        {
            if (presents.Count < Capacity)
            {
                presents.Add(present);
            }
            else if (presents.Count<= Capacity)
            {
                //TODO: exeption or message??
            }
        }

        public bool Remove(string name)
        {
            foreach (var item in this.presents)
            {
                if (item.Name == name)
                {
                    this.presents.Remove(item);
                    return true;
                }
            }
            return false;
        }
        public int Count => this.presents.Count;
        public Present GetHeaviestPresent() => this.presents.OrderByDescending(a => a.Weight).FirstOrDefault();
        public Present GetPresent(string name) => this.presents.FirstOrDefault(x=>x.Name==name);
        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Color} bag contains:");
            foreach (var item in presents)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }

        //public string GetHeaviestPresent()
        //{
        //    string heaviestPreset = string.Empty;
        //    string gender = string.Empty;
        //    int weightOfTheHeaviestPresent = int.MinValue;

        //    foreach (var item in presents)
        //    {
        //        if (item.Weight >weightOfTheHeaviestPresent)
        //        {
        //            heaviestPreset = item.Name;
        //            gender = item.Gender;
        //        }
        //    }
        //    foreach (var item in presents)
        //    {
        //        if (item.Name == heaviestPreset)
        //        {
        //            this.presents.Remove(item);
        //        }
        //    }

        //    return $"Present {heaviestPreset} for a {gender}";
        //}
    }
}
