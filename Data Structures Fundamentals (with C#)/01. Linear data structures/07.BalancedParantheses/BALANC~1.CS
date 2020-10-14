namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class BalancedParenthesesSolve : ISolvable
    {
        /*
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
        */

        public bool AreBalanced(string parentheses)
        {
            if (string.IsNullOrEmpty(parentheses) || parentheses.Length % 2 == 1)
            {
                return false;
            }

            while (parentheses.Length > 0)
            {
                var openingBracket = parentheses[0];
                char oppositBracket = default;

                switch (openingBracket)
                {
                    case '(':
                        oppositBracket = ')';
                        break;
                    case '[':
                        oppositBracket = ']';
                        break;
                    case '{':
                        oppositBracket = '}';
                        break;
                    default:
                        return false;
                }

                var oppositIsContained = parentheses.Contains(oppositBracket);

                if (!oppositIsContained)
                {
                    return false;
                }
                parentheses = parentheses.Remove(0,1);
                parentheses = parentheses.Remove(parentheses.IndexOf(oppositBracket), 1);
            }

            return true;
        }
    }
}
