using ConsoleApp31.FactoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp31
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var list = new List<HeroFactory>();
            HeroFactory hero = null;

            for (int i = 0; i < n; i++)
            {
                var nameHero = Console.ReadLine();

                var typeHero = Console.ReadLine();
                if (!ValidateHero(typeHero))
                {
                    Console.WriteLine("Invalid hero!");
                    i--;
                    continue;
                }

                hero = CreateHero(list, hero, nameHero, typeHero);
            }

            var bossPower = int.Parse(Console.ReadLine());
            int totPawor = 0;
            foreach (var heroPower in list)
            {
                Console.WriteLine(heroPower.GetHero().CastAbility());
                totPawor += heroPower.GetHero().Power;
            }

            var result = totPawor >= bossPower ? "Victory!" : "Defeat...";

            Console.WriteLine(result);
        }

        private static HeroFactory CreateHero(List<HeroFactory> list, HeroFactory hero, string nameHero, string typeHero)
        {
            switch (typeHero)
            {
                case "Druid":
                    hero = new DruidFactory(nameHero);
                    break;
                case "Paladin":
                    hero = new PaladinFactory(nameHero);
                    break;
                case "Rogue":
                    hero = new RogueFactory(nameHero);
                    break;
                case "Warrior":
                    hero = new WarriorFactory(nameHero);
                    break;

            }

            list.Add(hero);
            return hero;
        }

        private static bool ValidateHero(string typeHero)
        {
            foreach (var hero in Enum.GetNames(typeof(HeroEnum)))
            {
                if (hero == typeHero)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
