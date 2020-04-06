using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Heroes
{
   public class HeroRepository
    {
        private List<Hero> data;
        public HeroRepository()
        {
            this.data = new List<Hero>();
        }

        public void Add (Hero hero)
        {
            this.data.Add(hero);
        }
        public void Remove(string name)
        {
            var heroToRemove = this.data.FirstOrDefault(x => x.Name == name);
            this.data.Remove(heroToRemove);
        }
        public Hero GetHeroWithHighestStrength()
        {
            var sort = this.data.Select(x => x.Item.Strenght);
            var heroToreturn = this.data[0];
            return heroToreturn;
        }
        public Hero GetHeroWithHighestAbility()
        {
            var sort = this.data.Select(x => x.Item.Ability);
            var heroToreturn = this.data[0];
            return heroToreturn;
        }

        public Hero GetHeroWithHighestIntelligence()
        {
            var sort = this.data.Select(x => x.Item.Intelligence);
            var heroToreturn = this.data[0];
            return heroToreturn;
        }

        public int Count => this.data.Count();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.AppendLine($"Hero: {item.Name} - {item.Level}");
                sb.AppendLine($"{item.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
