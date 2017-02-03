using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;

namespace CodeFights_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            var room = new[]
            {
                new[] {0,1,1,2},
                new[] {0,5,0,0},
                new[] {2,0,3,3}
            };
            Console.WriteLine(obtainMaxNumber(new[] { 2, 4, 8, 1, 1, 15 }));
            //foreach (var i in sortByHeight(new[] { -1, 150, 190, 170, -1, -1, 160, 180 }))
            //{
            //    Console.WriteLine(i);
            //}

            Console.ReadKey();
        }
        public static bool checkPalindrome(string inputString)
        {
            return inputString.Substring(0, inputString.Length / 2) ==
                   new string(inputString.Reverse().ToArray()).Substring(0, inputString.Length / 2);
        }
        public static int adjacentElementsProduct(int[] inputArray)
        {
            var list = new List<int>();
            for (int i = 0; i < inputArray.Length - 1; i++)
                list.Add(inputArray[i] * inputArray[i + 1]);

            return list.Max();
            //int max = -2500;
            //for (int i = 1; i < inputArray.Length;)
            //{
            //    max = Math.Max(max, inputArray[i - 1] * inputArray[i++]);
            //}
            //return max;
        }
        public static int shapeArea(int n)
        {
            //return n == 1 ? n : n * n + (n - 1) * (n - 1);
            if (n == 1) return 1;
            if (n == 2) return 5;
            return shapeArea(n - 1) + n * 4 - 4;
            //int r = 0; // without recursion
            //for (int i = 0; i < n - 1; i++)
            //    r += i;

            //return r * 4 + 1 + (n - 1) * 4;

            //return (int)(Math.Pow(n, 2) + Math.Pow(n - 1, 2));
        }
        public static int makeArrayConsecutive2(int[] statues)
        {
            var count = 0;
            var number = statues.Min();
            while (number != statues.Max())
            {
                number++;
                if (!statues.Contains(number)) count++;
            }
            return count;
        }
        public static bool almostIncreasingSequence(int[] sequence)
        {
            var list = sequence.ToList();
            var b = true;
            for (int i = 0; i < sequence.Length; i++)
            {
                list.RemoveAt(i);
                b = true;
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j] >= list[j + 1])
                    {
                        b = false;
                        break;
                    }
                }
                if (b) return true;
                list = sequence.ToList();
            }
            return false;
        }  // doesn't pass the last hidden test
        public static int matrixElementsSum(int[][] matrix)
        {
            var sum = 0;
            for (var i = 0; i < matrix[0].Length; i++)
            {
                sum += matrix.Select(x => x[i]).TakeWhile(x => x != 0).Sum();
            }
            return sum;
        }
        public static string[] allLongestStrings(string[] inputArray)
        {
            //int maxSize = inputArray.Max(a => a.Length);
            var maxLength = inputArray.Select(s => s.Length).Max();
            return inputArray.Where(x => x.Length == maxLength).ToArray();
        }
        public static int commonCharacterCount(string s1, string s2)
        {
            //return s1.Distinct().Sum(_ => Math.Min(s1.Count(l => l == _), s2.Count(l => l == _)));
            var inter = s1.Intersect(s2).ToArray();
            return inter.Sum(t => Math.Min(s2.Count(l => l == t), s1.Count(l => l == t)));
        }
        public static bool isLucky(int n)
        {
            var firstPart = 0;
            var secondPart = 0;
            var half = n.ToString().Length / 2;

            while (half-- != 0)
            {
                secondPart += n % 10;
                n /= 10;
            }
            while (n != 0)
            {
                firstPart += n % 10;
                n /= 10;
            }
            return firstPart == secondPart;
            //var N = n.ToString();
            //n = N.Length / 2;
            //return N.Substring(n).Sum(_ => _ - '0') == N.Remove(n).Sum(_ => _ - '0');
        }
        public static int[] sortByHeight(int[] a)
        {
            var people = a.Where(x => x > 0).ToList();
            people.Sort();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == -1) continue;
                a[i] = people[0];
                people.RemoveAt(0);
            }
            return a;
            //var people = a.Where(p => p != -1).OrderBy(p => p).ToList();
            //int idx = 0;
            //return a.Select(x => x == -1 ? -1 : people[idx++]).ToArray();
        }
        public static int obtainMaxNumber(int[] inputArray)
        {
            //even - զույգ //odd - կենտ
            var list = inputArray.ToList();
            int toBeRemoved;
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Count(x => x == list[i]) >= 2)
                {
                    list.Add(list[i] * 2);
                    toBeRemoved = list[i];
                    list.Remove(toBeRemoved);
                    list.Remove(toBeRemoved);
                    i = -1;
                }
                if (list.All(x => list.Count(a => a == x) == 1)) return list.Max();
            }

            return list.Max();
        }
    }
}

