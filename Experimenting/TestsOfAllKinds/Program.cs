using System;
using System.Collections.Generic;
using System.Linq;

namespace TestsOfAllKinds
{
    // Math.Floor() function:   The two numbers 123.456 and 123.987 are rounded down to the nearest integer.This means that regardless of how close they are close to 124, they are rounded to 123.
    //Note:
    //Floor can be useful when rounding numbers that are part of a larger representation of another number.

    //Discussion.The Math.Floor method when given a positive number will erase the digits after the decimal place. But when it receives a negative number, it will erase the digits and increase the number's negativity by 1.
    //So:
    //Using Math.Floor on a negative number will still decrease the total number.This means it will always become smaller.
    class Program
    {
        static void Main(string[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT */
            var stringLength = int.Parse(Console.ReadLine());
            var parameters = Console.ReadLine().Split();
            var K = int.Parse(parameters[0]);
            var L = int.Parse(parameters[1]);
            var M = int.Parse(parameters[2]);
            var stringItself = Console.ReadLine();

            var substringDict = GetSubstringOccurences(stringItself, K, L, M);
            var mostFrequent = GetKeyWithMaxValue(substringDict);

            Console.Write(mostFrequent);
        }

        private static string GetKeyWithMaxValue(Dictionary<string, int> dict)
        {
            return dict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        private static Dictionary<string, int> GetSubstringOccurences(string text, int least, int most, int distCharCount)
        {
            var substringDict = new Dictionary<string, int>();
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = least; j <= most && i + j < text.Length; j++)
                {
                    var substring = text.Substring(i, j);
                    if (substring.Distinct().Count() > distCharCount)
                        continue;

                    if (!substringDict.ContainsKey(substring))
                        substringDict[substring] = 1;
                    else
                        substringDict[substring]++;
                }
            }

            return substringDict;
        }
    }
}
