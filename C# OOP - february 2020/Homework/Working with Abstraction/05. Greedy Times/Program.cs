using System;
using System.Collections.Generic;
using System.Linq;

namespace P05_GreedyTimes
{

    public class Potato
    {
        static void Main(string[] args)
        {
            long input = long.Parse(Console.ReadLine());
            string[] chest = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var bagWithTresuers = new Dictionary<string, Dictionary<string, long>>();
            long gold = 0;
            long stones = 0;
            long money = 0;

            for (int i = 0; i < chest.Length; i += 2)
            {
                string chestName = chest[i];
                long count = long.Parse(chest[i + 1]);

                string isCashGemOrGold = string.Empty;

                if (chestName.Length == 3)
                {
                    isCashGemOrGold = "Cash";
                }
                else if (chestName.ToLower().EndsWith("gem"))
                {
                    isCashGemOrGold = "Gem";
                }
                else if (chestName.ToLower() == "gold")
                {
                    isCashGemOrGold = "Gold";
                }

                if (isCashGemOrGold == "")
                {
                    continue;
                }
                else if (input < bagWithTresuers.Values.Select(x => x.Values.Sum()).Sum() + count)
                {
                    continue;
                }

                switch (isCashGemOrGold)
                {
                    case "Gem":
                        if (!bagWithTresuers.ContainsKey(isCashGemOrGold))
                        {
                            if (bagWithTresuers.ContainsKey("Gold"))
                            {
                                if (count > bagWithTresuers["Gold"].Values.Sum())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (bagWithTresuers[isCashGemOrGold].Values.Sum() + count > bagWithTresuers["Gold"].Values.Sum())
                        {
                            continue;
                        }
                        break;
                    case "Cash":
                        if (!bagWithTresuers.ContainsKey(isCashGemOrGold))
                        {
                            if (bagWithTresuers.ContainsKey("Gem"))
                            {
                                if (count > bagWithTresuers["Gem"].Values.Sum())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (bagWithTresuers[isCashGemOrGold].Values.Sum() + count > bagWithTresuers["Gem"].Values.Sum())
                        {
                            continue;
                        }
                        break;
                }

                if (!bagWithTresuers.ContainsKey(isCashGemOrGold))
                {
                    bagWithTresuers[isCashGemOrGold] = new Dictionary<string, long>();
                }

                if (!bagWithTresuers[isCashGemOrGold].ContainsKey(chestName))
                {
                    bagWithTresuers[isCashGemOrGold][chestName] = 0;
                }

                bagWithTresuers[isCashGemOrGold][chestName] += count;
                if (isCashGemOrGold == "Gold")
                {
                    gold += count;
                }
                else if (isCashGemOrGold == "Gem")
                {
                    stones += count;
                }
                else if (isCashGemOrGold == "Cash")
                {
                    money += count;
                }
            }

            foreach (var x in bagWithTresuers)
            {
                Console.WriteLine($"<{x.Key}> ${x.Value.Values.Sum()}");
                foreach (var item2 in x.Value.OrderByDescending(y => y.Key).ThenBy(y => y.Value))
                {
                    Console.WriteLine($"##{item2.Key} - {item2.Value}");
                }
            }
        }
    }
}