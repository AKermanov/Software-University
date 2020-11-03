using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryFarming
{
    class Program
    {
        public static void Main()
        {
            Dictionary<string, int> keyMaterials = new Dictionary<string, int>();
            keyMaterials["motes"] = 0;
            keyMaterials.Add("fragments", 0);
            keyMaterials.Add("shards", 0);
            Dictionary<string, int> junkMaterials = new Dictionary<string, int>();

            while (true)
            {
                string[] input = Console.ReadLine()
                    .Split();

                bool hasToBreak = false;
                for (int i = 0; i < input.Length; i+= 2)
                {
                    int quantity = int.Parse(input[i]);
                    string material = input[i + 1].ToLower();
                    if (material == "shards" || material == "fragments" || material == "motes")
                    {
                        keyMaterials[material] += quantity;

                        if (keyMaterials[material] >= 250)
                        {
                            keyMaterials[material] -= 250;
                            if (material == "shards")
                            {
                                Console.WriteLine($"Shadowmourne obtained!");
                            }
                            else if (material == "fragments")
                            {
                                Console.WriteLine($"Valanyr obtained!");
                            }
                            else
                            {
                                Console.WriteLine($"Dragonwrath obtained!");
                            }
                            hasToBreak = true;
                            break;
                        }
                    }
                    else
                    {
                        if (junkMaterials.ContainsKey(material) == false)
                        {
                            junkMaterials[material] = 0;
                        }
                        junkMaterials[material] += quantity;

                    }
                }
                if (hasToBreak)
                {
                    break;
                }
            }
            Dictionary<string, int> filteredKeyMaterials = keyMaterials
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .ToDictionary(a => a.Key, a => a.Value);
            foreach (var kvp in filteredKeyMaterials)
            {
                string material = kvp.Key;
                int quantity = kvp.Value;
                Console.WriteLine($"{material}: {quantity}");
            }
            foreach (var kvp in junkMaterials.OrderBy(kvp => kvp.Key))
            {
                string material = kvp.Key;
                int quantity = kvp.Value;
                Console.WriteLine($"{material}: {quantity}");
            }
        }
    }
}
