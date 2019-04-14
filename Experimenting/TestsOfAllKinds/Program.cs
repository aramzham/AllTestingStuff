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
            var a = consecutive(15);

            Console.ReadLine();
        }

        public static int consecutive(long num)
        {
            var count = 0;
            for (int i = 1; i <= num / 2 + 1; i++)
            {
                if (HasProgression(i, num))
                    count++;
            }

            return count;
        }

        private static bool HasProgression(int first, long num)
        {
            var underSqrt = Math.Pow(first * 2 - 1, 2) + 8 * num;
            var sqrt = Math.Sqrt(underSqrt);
            return Math.Abs(Math.Ceiling(sqrt) - Math.Floor(sqrt)) < double.Epsilon;
        }

        private static bool IsSquare(int n)
        {
            int i = 1;
            while (true)
            {
                if (n < 0)
                    return false;
                if (n == 0)
                    return true;
                n -= i;
                i += 2;
            }
        }

        private static int GetShortestSubArray(List<int> sequence, int number, int degree)
        {
            var list = Enumerable.Range(0, sequence.Count).Where(i => sequence[i] == number).ToList();
            return list[degree - 1] - list[0] + 1;
        }

        private static Dictionary<int, int> MostFrequentElements(IEnumerable<int> sequence)
        {
            var dict = new Dictionary<int, int>();
            foreach (var i in sequence)
            {
                if (!dict.ContainsKey(i))
                    dict[i] = 1;
                else
                    dict[i]++;
            }

            var max = dict.Values.Max();

            return dict.Where(x => x.Value == max).ToDictionary(x => x.Key, y => y.Value);
            //var orderedByCount = sequence.GroupBy(x => x).OrderByDescending(x => x.Count());
            //return orderedByCount.Where(x => x.Count() == orderedByCount.First().Count()).Select(x=>x.Key).ToList();
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
