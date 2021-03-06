﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using static System.Math;

namespace GetNumberWithoutConvertOrParse
{
    class Program
    {
        static void Main(string[] args)
        {
            byte b = 9;
            Console.WriteLine($"{Convert.ToString(b, 2).PadLeft(8, '0')} = {b}");
            var turn = b << 3; //сдвиг в лево
            Console.WriteLine($"{Convert.ToString(turn, 2).PadLeft(8, '0')} = {turn}");
            var back = turn >> 1; //сдвиг в право
            Console.WriteLine($"{Convert.ToString(back, 2).PadLeft(8, '0')} = {back}");
            #region SizeOf
            Console.WriteLine($"size of sbyte is {sizeof(sbyte)}");
            Console.WriteLine($"size of byte is {sizeof(byte)}");
            Console.WriteLine($"size of short is {sizeof(short)}");
            Console.WriteLine($"size of ushort is {sizeof(ushort)}");
            Console.WriteLine($"size of int is {sizeof(int)}");
            Console.WriteLine($"size of uint is {sizeof(uint)}");
            Console.WriteLine($"size of long is {sizeof(long)}");
            Console.WriteLine($"size of ulong is {sizeof(ulong)}");
            Console.WriteLine($"size of char is {sizeof(char)}");
            Console.WriteLine($"size of float is {sizeof(float)}");
            Console.WriteLine($"size of double is {sizeof(double)}");
            Console.WriteLine($"size of decimal is {sizeof(decimal)}");
            Console.WriteLine($"size of bool is {sizeof(bool)}");
            #endregion
            #region goto
            //Label:
            //    Console.WriteLine("Infinite loop with goto");
            //    goto Label;
            //anotherLabel:
            //    Console.WriteLine("Unreachable code");
            //    goto anotherLabel;
            #endregion
            #region Boolean assingning
            bool b1 = 1 > 2;
            Console.WriteLine(b1);
            var a1 = 3;
            var a2 = 1;
            bool b2 = a1 > a2;
            Console.WriteLine(b2);
            #endregion
            //ctrl + h = replace, ctrl + g = go to line #

            foreach (var randoms in Generator(100).Take(15))
            {
                Console.Write($"{randoms} ");
            }

            Console.ReadKey();
        }

        static bool ArraysEqual(int[] ar1, int[] ar2)
        {
            //return !ar1.Where((x, i) => x != ar2[i]).Any();
            if (ar1.Length == ar2.Length)
            {
                return !ar1.Where((item, index) => item != ar2[index]).Any();
            }
            return false;
        }
        static int ConvertStringToInt32(string number)
        {
            return number.Select(x => x - '0').Select((t, i) => t * (int)Pow(10, number.Length - i - 1)).Sum();
        }
        #region Bubble sort

        static void BubbleSort(ref int[] items)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = 0; j < items.Length - i - 1; j++)
                {
                    if (items[j] > items[j + 1])
                    {
                        Swap(ref items[j], ref items[j + 1]);
                    }
                }
            }
        }

        static void Swap<T>(ref T a, ref T b)
        {
            if (!Equals(a, b))
            {
                T temp = a;
                a = b;
                b = temp;
            }
        }

        #endregion
        #region Count lucky numbers // doesn't work well

        static int countLuckyNumbers(int n) // 4 - 670, 6 - 55252
        {
            var range = Enumerable.Range(0, (int)Pow(10, n)).ToArray();
            var strNumber = string.Empty;
            var list = new List<char>();
            var count = 0;
            foreach (int item in range)
            {
                strNumber = item.ToString().PadLeft(n, '0');
                if (IsLucky(strNumber)) count++;
                else
                {
                    list = strNumber.ToList();
                    for (int j = 0; j < strNumber.Length - item.ToString().Length; j++)
                    {
                        list.RemoveAt(0);
                        if (IsLucky(new string(list.ToArray()))) count++;
                    }
                }
            }
            return count;
        }

        private static bool IsLucky(string N)
        {
            //if (N.Length % 2 == 1) N = N.Insert(0, "0");
            var n = N.Length / 2;
            return N.Substring(n).Sum(x => x - '0') == N.Remove(n).Sum(x => x - '0');
        }

        #endregion
        static IEnumerable<int> Generator(int max)
        {
            var r = new Random();
            while (true)
            {
                yield return r.Next(max);
            }
        }
    }
}