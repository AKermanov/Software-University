using Wintellect.PowerCollections;
using System;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var priorityQueue = new OrderedBag<int>();

            foreach (var cookied in cookies)
            {
                priorityQueue.Add(cookied);
            }

            int currentMinSweetness = priorityQueue[0];
            int steps = 0;

            while (currentMinSweetness < k && priorityQueue.Count > 1)
            {
                int lastSweetCookie = priorityQueue.RemoveFirst();
                int secondLeastSweetCookie = priorityQueue.RemoveFirst();

                int combined = lastSweetCookie + (2 * secondLeastSweetCookie);

                priorityQueue.Add(combined);

                currentMinSweetness = priorityQueue.GetFirst();
                steps++;
            }

            return currentMinSweetness < k ? -1 : steps;
        }
        int CompareElements (int first, int second)
        {
            return second - first;
        }
    }
}
