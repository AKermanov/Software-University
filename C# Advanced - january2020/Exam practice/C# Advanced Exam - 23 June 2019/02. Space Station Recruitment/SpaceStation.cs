using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace SpaceStationRecruitment
{

    class SpaceStation
    {
        private List<Astronaut> astronauts;
        public SpaceStation(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.astronauts = new List<Astronaut>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        //public int Count //1ви вариянт
        //{
        //    get
        //    {
        //        return this.astronauts.Count;
        //    }
        //}
        public int Count => this.astronauts.Count; //2ри вариянт, това е пропърти
        //public int Count() => this.astronauts.Count; //това е метод, НЕ пропърти!!
        public void Add(Astronaut astronaut)
        {
            if (this.astronauts.Count < this.Capacity)
            {
                this.astronauts.Add(astronaut);
            }
        }
        public bool Remove(string name)
        {
            foreach (var item in this.astronauts)
            {
                if (item.Name == name)
                {
                    this.astronauts.Remove(item);
                    return true;
                }
            }
            return false;
        }
        // public Astronaut GetOldestAstronaut()
        //{
        //Astronaut result = new Astronaut(string.Empty, int.MinValue, string.Empty); //1ви вариянт

        //foreach (var item in astronauts)
        //{
        //    if (item.Age > result.Age)
        //    {
        //        result = item;
        //    }
        //}

        //return result;

        //Astronaut result = this.astronauts.OrderByDescending(a => a.Age).FirstOrDefault(); //2ри вариян
        //return result;
        //}
        public Astronaut GetOldestAstronaut() => this.astronauts.OrderByDescending(a => a.Age).FirstOrDefault();
        //3ти вариянт: прави същото като горното

        public Astronaut GetAstronaut(string name)
        {
            return this.astronauts.Where(a => a.Name == name).FirstOrDefault(); //1ви вариян
            //return this.astronauts.FirstOrDefault(a => a.Name == name); //2ви вариян

            //Astronaut result = null; //3ви вариян
            //foreach (var item in this.astronauts)
            //{
            //    if (item.Name == Name)
            //    {
            //        result = item;
            //        break;
            //    }
            //}

            //return result;
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Astronauts working at Space Station {this.Name}:");

            foreach (var astro in astronauts)
            {
                sb.AppendLine(astro.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
