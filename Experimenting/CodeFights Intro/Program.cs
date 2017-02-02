using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeFights_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(almostIncreasingSequence(new[] {1,3,2}));

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
                for (int j = 0; j < list.Count-1; j++)
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
    }
}

