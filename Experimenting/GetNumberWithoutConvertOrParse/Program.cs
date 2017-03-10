using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Math;

namespace GetNumberWithoutConvertOrParse
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new[] {"a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8",
                               "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8",
                               "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8",
                               "d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8",
                               "e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8",
                               "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8",
                               "g1", "g2", "g3", "g4", "g5", "g6", "g7", "g8",
                               "h1", "h2", "h3", "h4", "h5", "h6", "h7", "h8"};
            //Console.WriteLine(int.Parse(str));

            //foreach (var VARIABLE in composeRanges(new[] { 0, 1 }))
            //{
            //    Console.Write($"{VARIABLE} ");
            //}

            Console.ReadKey();
        }

        static bool ArraysEqual(int[] ar1, int[] ar2)
        {
            //return !ar1.Where((x, i) => x != ar2[i]).Any();
            if (ar1.Length == ar2.Length)
            {
                for (int i = 0; i < ar1.Length; i++)
                {
                    if (ar1[i] != ar2[i]) return false;
                    if (i == ar1.Length - 1) return true;
                }
            }
            return false;
        }
        static int Convert(string number)
        {
            return number.Select(x => x - '0').Select((t, i) => t * (int)Pow(10, number.Length - i - 1)).Sum();
        }
        static int strstr(string s, string x)
        {
            var hash = new HashSet<char>(s.ToCharArray());
            if (x.Any(ch => !hash.Contains(ch))) return -1;
            for (int i = 0; i <= s.Length - x.Length; i++)
            {
                if (s.Substring(i, x.Length) == x) return i;
            }
            return -1;
        } // doesn't pass hiddens
        static int[] findLongestSubarrayBySum(int s, int[] arr)
        {
            var results = new List<List<int>>();
            var sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr.Length; j++)
                {
                    if (sum == s) results.Add(new List<int> { i, j });
                    sum += arr[j];
                }
                sum = 0;
            }
            return new int[] { };
        } //not finished
        #region Sum in range :-((

        static int sumInRange(int[] nums, int[][] queries) //crashes on hidden tests, cannot really understand why
        {
            var sum = 0;
            var sums = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                sums[i] = sum;
            }
            var total = 0;
            for (int i = 0; i < queries.Length; i++)
            {
                if (queries[i][0] == 0) total += sums[queries[i][1]];
                else if (queries[i][1] == 0) total += sums[0];
                else total += sums[queries[i][1]] - sums[queries[i][0] - 1];
            }
            //for (int i = 0; i < queries.Length; i++)
            //{
            //    sum += nums.ToList().GetRange(queries[i][0], queries[i][1] - queries[i][0] + 1).Sum();
            //}
            //var sum = queries.Sum(t => nums.ToList().GetRange(t[0], t[1] - t[0] + 1).Sum());
            return mod(total, (int)(Pow(10, 9)) + 7);
        }

        // static int sumRange(int i, int j)
        //{
        //    if (i == 0)
        //    {
        //        return sum[j];
        //    }
        //    if (j == 0)
        //    {
        //        return sum[0];
        //    }
        //    return sum[j] - sum[i - 1];
        static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        //int mod(int x, int m)
        //{
        //    int r = x % m;
        //    return r < 0 ? r + m : r;
        //}

        #endregion
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
            for (int i = 0; i < range.Length; i++)
            {
                strNumber = range[i].ToString().PadLeft(n, '0');
                if (IsLucky(strNumber)) count++;
                else
                {
                    list = strNumber.ToList();
                    for (int j = 0; j < strNumber.Length - range[i].ToString().Length; j++)
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
    }
}
