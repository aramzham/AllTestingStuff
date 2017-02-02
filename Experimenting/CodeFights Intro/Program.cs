using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFights_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(checkPalindrome("aabaa"));

            Console.ReadKey();
        }
        public static bool checkPalindrome(string inputString)
        {
            return inputString.Substring(0, inputString.Length/2) ==
                   new string(inputString.Reverse().ToArray()).Substring(0, inputString.Length/2);
        }
        public static int adjacentElementsProduct(int[] inputArray)
        {
            var list = new List<int>();
            for (int i = 0; i < inputArray.Length-1; i++)
                list.Add(inputArray[i]*inputArray[i+1]);

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
            if (n == 1) return 1;
            if (n == 2) return 5;
            return n*n + 4;
        }
    }
}
