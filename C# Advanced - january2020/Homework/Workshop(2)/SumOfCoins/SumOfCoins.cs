using System;
using System.Collections.Generic;
using System.Linq;

public class SumOfCoins
{
    public static void Main(string[] args)
    {
        var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
        var targetSum = 300;

        var selectedCoins = ChooseCoins(availableCoins, targetSum);

        Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
        foreach (var selectedCoin in selectedCoins)
        {
            Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
        }
    }

    public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
    {
        var results = new Dictionary<int, int>();
        var sortedCoin = coins.OrderByDescending(x => x).ToList();
        for (int currentCoinIndex = 0; currentCoinIndex < sortedCoin.Count; currentCoinIndex++)
        {
            var currentCoin = sortedCoin[currentCoinIndex];
            var totlCurrentCoins = targetSum / sortedCoin[currentCoinIndex]; //общия брой стотинки, които можем да вземем - дава 2
            targetSum %= currentCoin; //23
            results[currentCoin] = totlCurrentCoins;
        }
        // 123
        // 50
        return results;
    }
}