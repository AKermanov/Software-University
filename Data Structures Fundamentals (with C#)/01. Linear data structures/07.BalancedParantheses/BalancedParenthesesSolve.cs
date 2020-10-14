namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (string.IsNullOrEmpty(parentheses) || parentheses.Length % 2 == 1)
            {
                return false;
            }

            var openBrakets = new Stack<char>(parentheses.Length / 2);

            foreach (var curentBraket in parentheses)
            {
                char expectedCharecter = default;
                switch (curentBraket)
                {
                    case ')':
                        expectedCharecter = '(';
                        break;
                    case ']':
                        expectedCharecter = '[';
                        break;
                    case '}':
                        expectedCharecter = '{';
                        break;
                    default:
                        openBrakets.Push(curentBraket);
                        break;
                }

                if (expectedCharecter != default && openBrakets.Pop() != expectedCharecter)
                {
                    return false;
                }
            }

            return openBrakets.Count == 0;
        }
    }
}
