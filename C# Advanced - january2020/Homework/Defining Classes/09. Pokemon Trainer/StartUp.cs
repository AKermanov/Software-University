using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            string command = null;
            Dictionary<string, Trainer> collectionsOfTrainers = new Dictionary<string, Trainer>();
            while ((command = Console.ReadLine()) != "Tournament")
            {
                var cmdArgs = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var tranerName = cmdArgs[0];
                var pokemonName = cmdArgs[1];
                var pokemonElement = cmdArgs[2];
                var pokemonHealth = int.Parse(cmdArgs[3]);
                if (!collectionsOfTrainers.ContainsKey(tranerName))
                {
                    collectionsOfTrainers.Add(tranerName, new Trainer(tranerName));
                }
                Trainer currentTraner = collectionsOfTrainers[tranerName];
                Pokemon pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);
                currentTraner.Pokemons.Add(pokemon);
            }
            string command2 = null;
            while ((command2=Console.ReadLine()) != "End")
            {
                var element = command2;
                foreach (var trainers in collectionsOfTrainers)
                {
                    if (trainers.Value.Pokemons.Exists(x=>x.Elemenet == element))
                    {
                        trainers.Value.Badges++;
                    }
                    else
                    {
                        foreach (var pokemon in trainers.Value.Pokemons)
                        {
                            pokemon.Health -= 10;
                        }
                        trainers.Value.Pokemons.RemoveAll(x => x.Health <= 0);
                    }
                }
            }
            var result = collectionsOfTrainers
                .OrderByDescending(x => x.Value.Badges).ToDictionary(k => k.Key, v => v.Value);
            foreach (var item in result)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
