﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using static System.Math;
using static System.DateTime;

namespace GetNumberWithoutConvertOrParse
{
    class Program
    {
        static void Main(string[] args)
        {
            var queries = new char[][]
            {
                new[] {'.', '.', '.', '1', '4', '.', '.', '2', '.'},
                new[] {'.', '.', '6', '.', '.', '.', '.', '.', '.'},
                new[] {'.', '.', '.', '.', '.', '.', '.', '.', '.'},
                new[] {'.', '.', '1', '.', '.', '.', '.', '.', '.'},
                new[] {'.', '6', '7', '.', '.', '.', '.', '.', '9'},
                new[] {'.', '.', '.', '.', '.', '.', '8', '1', '.'},
                new[] {'.', '3', '.', '.', '.', '.', '.', '.', '6'},
                new[] {'.', '.', '.', '.', '.', '7', '.', '.', '.'},
                new[] {'.', '.', '.', '5', '.', '.', '.', '7', '.'}
            };
            var nums = new int[] { 3, 0, -2, 6, -3, 2 };
            //Console.WriteLine(sumInRange(nums, queries));
            Console.WriteLine(columnTitle(1636807827));
            Console.WriteLine(happyNumber(1111111));

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
            return new int[] { };
        } //not finished
        static string reverseVowelsOfString(string s)
        {
            var indexes = new List<int>();
            var vowels = new List<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a' || s[i] == 'e' || s[i] == 'i' || s[i] == 'o' || s[i] == 'u' || s[i] == 'A' ||
                    s[i] == 'E' || s[i] == 'I' || s[i] == 'O' || s[i] == 'U')
                {
                    indexes.Add(i);
                    vowels.Add(s[i]);
                }
            }
            indexes.Reverse();
            var charray = s.ToCharArray();
            for (int i = 0; i < indexes.Count; i++)
            {
                charray[indexes[i]] = vowels[i];
            }
            return new string(charray);
            //string vowel = "aeiouAEIOU";
            //string r = "";
            //var t = s.Where(x => vowel.Contains(x)).Reverse().ToList();
            //for (int i = 0, j = 0; i < s.Length; i++)
            //{
            //    r += vowel.Contains(s[i]) ? t[j++] : s[i];
            //}
            //return r;

            //another solution with stack
            //            bool IsVovel(char c)
            //{
            //                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' ||
            //                  c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U')
            //                {
            //                    return true;
            //                }
            //                return false;
            //            }

            //            string reverseVowelsOfString(string s) 
            //{
            //                Stack<char> st = new Stack<char>();
            //                for (int i = 0; i < s.Length; i++)
            //                {
            //                    if (IsVovel(s[i]))
            //                        st.Push(s[i]);
            //                }
            //                string result = "";
            //                for (int i = 0; i < s.Length; i++)
            //                {
            //                    if (IsVovel(s[i]))
            //                        result += st.Pop();
            //                    else
            //                        result += s[i];
            //                }
            //                return result;
            //            }
        }
        static int reverseInteger(int x)
        {
            var abs = Math.Abs(x);
            var queue = new Queue<int>();
            while (abs != 0)
            {
                queue.Enqueue(abs % 10);
                abs /= 10;
            }
            var degree = Math.Abs(x).ToString().Length - 1;
            var result = 0;
            var count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                result += queue.Dequeue() * (int)Math.Pow(10, degree);
                degree--;
            }
            return x < 0 ? result * -1 : result;
            //int result = 0;   //this guy is genius!!!
            //while (x != 0)
            //{
            //    result *= 10;
            //    result += x % 10;
            //    x /= 10;
            //}
            //return result;
        }
        static int kthLargestElement(int[] nums, int k)
        {
            var ordered = nums.OrderByDescending(x => x).ToArray();
            return ordered[k - 1];
            //Array.Sort(nums);
            //Array.Reverse(nums);
            //return nums[k - 1];
        }
        static int higherVersion2(string ver1, string ver2)
        {
            var firstVersion = ver1.Split('.').Select(int.Parse).ToArray();
            var secondVersion = ver2.Split('.').Select(int.Parse).ToArray();
            for (int i = 0; i < firstVersion.Length; i++)
            {
                if (secondVersion[i] > firstVersion[i]) return -1;
                if (secondVersion[i] < firstVersion[i]) return 1;
            }
            return 0;
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
            return mod(total, (int)(Math.Pow(10, 9)) + 7);
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
        static string columnTitle(int number)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var lengthOfLetters = (int)Math.Log(number, alphabet.Length) + 1;
            var sb = new StringBuilder();
            var index = 0;
            for (int i = lengthOfLetters - 1; i >= 0; i--)
            {
                index = number / (int)Math.Pow(alphabet.Length, i);
                sb.Append(index == 0 ? 'z' : alphabet[index - 1]);
                number %= (int)Math.Pow(alphabet.Length, i);
            }

            return sb.ToString().ToUpper();
        }
        #region Count lucky numbers // doesn't work well

        static int countLuckyNumbers(int n) // 4 - 670, 6 - 55252
        {
            var range = Enumerable.Range(0, (int)Math.Pow(10, n)).ToArray();
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
        static bool sudoku2(char[][] grid)
        {
            IEnumerable<char> line;
            IEnumerable<char> column;
            List<char> rect;
            for (int i = 0; i < 9; i++)
            {
                line = grid.Select(l => l[i]).Where(char.IsDigit);
                if (line.Any(x => line.Count(y => y == x) > 1))   //this checks columns
                    return false;

                column = grid[i].Where(char.IsDigit);
                if (column.Any(x => column.Count(y => y == x) > 1))  //this one - lines
                    return false;
            }
            for (int x = 0; x < 3; x++)      // this is for squares
            {
                for (int y = 0; y < 3; y++)
                {
                    rect = new List<char>();
                    for (int i = 0; i < 9; i++)
                    {
                        rect.Add(grid[y * 3 + i / 3][x * 3 + i % 3]);
                    }
                    rect = rect.Where(char.IsDigit).ToList();
                    if (rect.Any(a => rect.Count(b => b == a) > 1))
                        return false;
                }
            }
            return true;
        }
#region Happy number
        static bool happyNumber(int n)
        {
            //while (true)
            //{
            //    if (SumDigitSquares(n) == 1) return true;
            //    if (SumDigitSquares(n) < 10 && SumDigitSquares(n) != 7) return false;
            //    n = SumDigitSquares(n);
            //}
            List<int> done = new List<int>();
            while (n != 1)
            {
                n = (n + "").Sum(x => (x - '0') * (x - '0'));
                if (done.Contains(n)) // if you encounter a number that you'd already obtained then return false! MAGIC!
                    return false;
                done.Add(n);
            }
            return true;
        }

        private static int SumDigitSquares(int number)
        {
            int sum = 0;
            int digit;
            while (number != 0)
            {
                digit = number % 10;
                number /= 10;
                sum += digit * digit;
            }
            return sum;
        }
#endregion
    }
}
