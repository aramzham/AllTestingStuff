﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GetNumberWithoutConvertOrParse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "LINQ + StackOverFlow :-)";
            Console.WriteLine(Convert("123456"));

            var a = new[] { 6, 10, 25, 13, 20, 21, 11, 10, 18, 21 };
            var b = new[] { 21, 10, 6, 0, 29, 25, 1, 17, 19, 25 };
            var v = 37;
            Console.WriteLine(sumOfTwo(a, b, v));
            var mc = new MyClass { Count = 10 };
            mc.Count = 35;

            var nums = new[] { 37, 50, 50, 6, 8, 54, 7, 64, 2, 64, 67, 59, 22, 73, 33, 53, 43, 77, 56, 76, 59, 96, 64, 100, 89, 38, 64, 73, 85, 96, 65, 50, 62, 4, 82, 57, 98, 61, 92, 55, 80, 53, 80, 55, 94, 9, 73, 89, 83, 80 };
            var m = 67;
            Console.WriteLine(productExceptSelf(nums, m));

            var arr = new[] { 1, 2, 3, 4, 5, 0, 0, 0, 6, 7, 8, 9, 10 };
            var s = 15;
            Console.WriteLine(findLongestSubarrayBySum(15, arr));

            Console.ReadKey();
        }

        static int Convert(string number)
        {
            return number.Select(x => x - '0').Select((t, i) => t * (int)Math.Pow(10, number.Length - i - 1)).Sum();
        }
        static bool sumOfTwo(int[] a, int[] b, int v)
        {
            //var line = a.Select(x => v - x);
            //return line.Any(b.Contains);
            if (a.Length < 1 || b.Length < 1) return false;
            var aList = a.ToList();
            var bList = b.ToList();
            aList.Sort();
            bList.Sort();
            //return bList.Any(t => aList.BinarySearch(v - t) >= -1);
            for (int i = 0; i < a.Length; i++)
            {
                if (v - a[i] > a.Max() || v - a[i] < a.Min()) return false;
                if (bList.BinarySearch(v - aList[i]) >= -1) return true;
            }
            return false;
        }
        static int strstr(string s, string x)
        {
            if (!s.Contains(x)) return -1;
            return s.IndexOf(x, StringComparison.Ordinal);
        }
        #region Product except self
        static int productExceptSelf(int[] nums, int m)
        {
            var arr = nums.Select(x => ProductWithout(nums) / x).Select(x => x % m).ToArray();
            var sum = arr.Aggregate<BigInteger, BigInteger>(0, (current, bigInteger) => current + bigInteger);
            var modulus = sum % m;
            return (int)modulus;
        }

        private static BigInteger ProductWithout(int[] array)
        {
            return array.Aggregate<int, BigInteger>(1, (current, t) => current * t);
        }
        #endregion
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
            return new int[]{};
        }
    }

    public class MyClass
    {
        private int count;
        public int Count
        {
            get { return count; }
            set { if (value > 20) count = value; }
        }
    }
}
