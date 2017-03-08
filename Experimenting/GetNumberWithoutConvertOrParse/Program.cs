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
            var queries = new[]
            {
                new[] {'O','2'},
                new[] {'T','4'},
                new[] {'W','6'},
                new[] {'E','1'},
                new[] {'N','3'}
            };
            var board = new[] {"a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8",
                               "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8",
                               "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8",
                               "d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8",
                               "e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8",
                               "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8",
                               "g1", "g2", "g3", "g4", "g5", "g6", "g7", "g8",
                               "h1", "h2", "h3", "h4", "h5", "h6", "h7", "h8"};
            var crypt = new[] { "ONE", "ONE", "TWO" };
            Console.WriteLine(isCryptSolution(crypt, queries));
            //Console.WriteLine(missingNumber(new[] { 0, 3, 5, 8, 4, 6, 1, 9, 7 }));

            foreach (var VARIABLE in innerRanges(new[] { 2147483647 }, -2147483648, 2147483647))
            {
                Console.Write($"{VARIABLE} ");
            }

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
        static bool sumOfTwo(int[] a, int[] b, int v)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = v - a[i];
            }
            var listA = a.OrderBy(x => x).ToList();
            return b.Any(t => listA.BinarySearch(t) >= 0);

            //var set = new HashSet<int>(b);
            //return a.Select(x => v - x).Any(diff => set.Contains(diff));

            //return a.Select(_ => v - _).Intersect(b).Any(); // a one-liner

            //Array.Sort(b);
            //return a.Select(t => Array.BinarySearch(b, v - t)).Any(indice => indice > -1);
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
            var abs = Abs(x);
            var queue = new Queue<int>();
            while (abs != 0)
            {
                queue.Enqueue(abs % 10);
                abs /= 10;
            }
            var degree = Abs(x).ToString().Length - 1;
            var result = 0;
            var count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                result += queue.Dequeue() * (int)Pow(10, degree);
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
        static string columnTitle(int number)
        {
            //const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            //var lengthOfLetters = (int)Math.Log(number, alphabet.Length) + 1;
            //var sb = new StringBuilder();
            //var index = 0;
            //for (int i = lengthOfLetters - 1; i >= 0; i--)
            //{
            //    index = number / (int)Math.Pow(alphabet.Length, i);
            //    sb.Append(index == 0 ? 'z' : alphabet[index - 1]);
            //    number %= (int)Math.Pow(alphabet.Length, i);
            //}

            //return sb.ToString().ToUpper();

            string columnString = "";
            decimal columnNumber = number;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;

            //string r = "";
            //while (n > 0)
            //{
            //    int m = (n - 1) % 26;
            //    r = (char)(65 + m) + r;
            //    n = (n - m) / 26;
            //}
            //return r;

            //n--;
            //var s = "";
            //while (n > -1)
            //{
            //    s = Convert.ToChar(n % 26 + 'A') + s;
            //    n /= 26;
            //    n--;
            //}
            //return s;
        }
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
        static int[] nearestGreater(int[] a)
        {
            var leftSteps = 0;
            var rightSteps = 0;
            var leftMax = 0;
            var indexOfLeftMax = 0;
            var rightMax = 0;
            var indexOfRightMax = 0;
            var b = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == a.Max()) b[i] = -1;
                if (i != 0)
                {
                    for (int j = i; j >= 0; j--) //left
                    {
                        if (a[j] > a[i]) //1 ≤ a[i] ≤ 109
                        {
                            indexOfLeftMax = j;
                            leftMax = a[j];
                            leftSteps = i - j;
                            break;
                        }
                    }
                }
                if (i != a.Length - 1)
                {
                    for (int k = i + 1; k < a.Length; k++) //right
                    {
                        if (a[k] > a[i])
                        {
                            indexOfRightMax = k;
                            rightMax = a[k];
                            rightSteps = k - i;
                            break;
                        }
                    }
                }
                if (leftMax != 0 && rightMax != 0)
                {
                    if (leftSteps > rightSteps) b[i] = indexOfRightMax;
                    else b[i] = indexOfLeftMax;
                }
                if (leftMax == 0 && rightMax != 0) b[i] = indexOfRightMax;
                if (leftMax != 0 && rightMax == 0) b[i] = indexOfLeftMax;
                leftMax = rightMax = 0;
            }
            return b;
        }
        static int[] matrixElementsInSpiralOrder(int[][] matrix)
        {
            if (matrix.Length == 0) return new int[] { };

            var result = new int[matrix.Length * matrix[0].Length];
            var k = -1;
            var i = 0;
            var j = 0;

            while (true)
            {
                while (j < matrix[i].Length && k < result.Length - 1 && matrix[i][j] != -1001) //right
                {
                    result[++k] = matrix[i][j];
                    matrix[i][j] = -1001; //-1000 ≤ matrix[i][j] ≤ 1000
                    j++;
                }
                if (matrix.All(x => x.All(a => a == -1001))) break;
                j--;
                i++;
                while (i != matrix.Length && k < result.Length - 1 && matrix[i][j] != -1001) //down
                {
                    result[++k] = matrix[i][j];
                    matrix[i][j] = -1001; //-1000 ≤ matrix[i][j] ≤ 1000
                    i++;
                }
                if (matrix.All(x => x.All(a => a == -1001))) break;
                j--;
                i--;
                while (j >= 0 && k < result.Length - 1 && matrix[i][j] != -1001) //left
                {
                    result[++k] = matrix[i][j];
                    matrix[i][j] = -1001; //-1000 ≤ matrix[i][j] ≤ 1000
                    j--;
                }
                if (matrix.All(x => x.All(a => a == -1001))) break;
                j++;
                i--;
                while (i > 0 && k < result.Length - 1 && matrix[i][j] != -1001) //up
                {
                    result[++k] = matrix[i][j];
                    matrix[i][j] = -1001; //-1000 ≤ matrix[i][j] ≤ 1000
                    i--;
                }
                i++;
                j++;
                if (matrix.All(x => x.All(a => a == -1001))) break;
            }
            return result;
            //if (matrix.Length == 0) //amazing solution!!!
            //    return new int[0];

            //string currentDirection = "Right";
            //int xIndex = 0;
            //int yIndex = 0;
            //int leftLimit = 0;
            //int rightLimit = matrix[0].Length - 1;
            //int upLimit = 0;
            //int downLimit = matrix.Length - 1;
            //int counter = 0;
            //int totalElements = matrix.Length * matrix[0].Length;
            //int[] spiralArray = new int[totalElements];

            //while (counter < totalElements)
            //{
            //    spiralArray[counter] = matrix[yIndex][xIndex];

            //    switch (currentDirection)
            //    {
            //        case "Right":
            //            if (xIndex < rightLimit)
            //                xIndex++;
            //            else
            //            {
            //                currentDirection = "Down";
            //                yIndex++;
            //                upLimit++;
            //            }
            //            break;
            //        case "Left":
            //            if (xIndex > leftLimit)
            //                xIndex--;
            //            else
            //            {
            //                currentDirection = "Up";
            //                yIndex--;
            //                downLimit--;
            //            }
            //            break;
            //        case "Up":
            //            if (yIndex > upLimit)
            //                yIndex--;
            //            else
            //            {
            //                currentDirection = "Right";
            //                xIndex++;
            //                leftLimit++;
            //            }
            //            break;
            //        case "Down":
            //            if (yIndex < downLimit)
            //                yIndex++;
            //            else
            //            {
            //                currentDirection = "Left";
            //                xIndex--;
            //                rightLimit--;
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //    counter++;
            //}

            //return spiralArray;

            //int rows = matrix.Length, cols = matrix.Length > 0 ? matrix[0].Length : 0; //shortest one i've ever seen
            //var elements = new int[rows * cols];
            //for (int level = 0, i = 0; rows > 0 && cols > 0; cols -= 2)
            //{
            //    var ring = matrix[level].Skip(level++).Take(cols)
            //        .Concat(matrix.Skip(level).Take(rows -= 2).Select(_ => _[level + cols - 2]));
            //    if (rows >= 0)
            //        ring = ring.Concat(matrix[level + rows].Skip(level - 1).Take(cols).Reverse());
            //    if (cols > 1)
            //        ring = ring.Concat(matrix.Skip(level).Take(rows).Select(_ => _[level - 1]).Reverse());
            //    foreach (var e in ring)
            //    {
            //        elements[i++] = e;
            //    }
            //}
            //return elements;
        }
        static int excelSheetColumnNumber(string s)
        {
            return s.Select((c, i) => (c - 'A' + 1) * (int)Pow(26, s.Length - i - 1)).Sum();

            //int sum = 0;
            //for (int i = 0; i < s.Length; i++)
            //{
            //    sum *= 26;
            //    sum += (s[i] - 'A' + 1);
            //}
            //return sum;

            //return s.ToCharArray().Select(c => c - 'A' + 1).Reverse().Select((v, i) => v * (int)Math.Pow(26, i)).Sum();
        }
        static int[] nextLarger(int[] a)
        {
            //var result = new int[a.Length];
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (i == a.Length -1) result[i] = -1;
            //    if (a.ToList().GetRange(i, a.Length - i).All(x => x <= a[i])) result[i] = -1;
            //    else result[i] = a.ToList().GetRange(i, a.Length - i).First(x => x > a[i]);
            //}
            //return result;

            var result = new int[a.Length];
            var rightSide = new Stack<int>();
            var remainder = new Stack<int>();
            for (int i = a.Length - 1; i >= 1; i--)
            {
                rightSide.Push(a[i]);
            }
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i] < rightSide.Peek()) result[i] = rightSide.Pop();
                else
                {
                    while (rightSide.Count != 0 && rightSide.Peek() <= a[i])
                    {
                        remainder.Push(rightSide.Pop());
                    }
                    if (rightSide.Count == 0) result[i] = -1;
                    else result[i] = rightSide.Peek();
                    while (remainder.Count != 0)
                    {
                        rightSide.Push(remainder.Pop());
                    }
                    rightSide.Pop();
                }
            }
            result[a.Length - 1] = -1;
            return result;

            //for (int i = 0, j; i < a.Length; i++)  // who is this guy?
            //{
            //    for (j = i + 1; j < a.Length && a[i] > a[j]; j++) ;
            //    a[i] = j < a.Length ? a[j] : -1;
            //}
            //return a;
        }
        #region Remove Duplicate Adjacent // only Van's solution works properly
        static string removeDuplicateAdjacent(string s)
        {
            //string r = "", s = " " + str + " ";
            //for (int i = 1; i < s.Length - 1; i++)
            //{
            //    if (s[i - 1] != s[i] && s[i] != s[i + 1])
            //        r += s[i];
            //}
            //if (str != r)
            //    r = removeDuplicateAdjacent(r);
            //return r;
            var current = default(char);
            var word = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == current) continue;
                if (i == s.Length - 1 || s[i] != s[i + 1]) word += s[i];
                else
                {
                    current = s[i];
                }
            }
            s = word;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    removeDuplicateAdjacent(s);
                }
            }
            return s;

            //string r = "";
            //char p = '$';
            //foreach (var e in s)
            //{
            //    if (p != e)
            //    {
            //        r += e;
            //        p = e;
            //    }
            //    else if (r.EndsWith(e + ""))
            //        r = r.Substring(0, r.LastIndexOf(e + ""));
            //}
            //if (r != s) r = removeDuplicateAdjacent(r);

            //return r;
        }
        #endregion
        static string sortByString(string s, string t)
        {
            //string p = "";
            //foreach (char c in t)
            //{
            //    for (int a = 0; a < s.Split(c).Length - 1; a++)
            //        p += c;
            //}
            //return p;

            //return t.Aggregate("", (current, c) => current + new string(s.Where(x => x == c).ToArray()));  //what about this one-liner?! :D

            //return new string(s.OrderBy(t.IndexOf).ToArray()); // another one?

            var intersect = t.Intersect(s).ToArray();
            var counts = new int[intersect.Length];
            var listOfChars = new List<char>();
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = s.Count(c => c == intersect[i]);
            }
            for (int i = 0; i < counts.Length; i++)
            {
                listOfChars.AddRange(Enumerable.Repeat(intersect[i], counts[i]));
            }
            return new string(listOfChars.ToArray());

            //var g = s.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            //string res = "";
            //foreach (var c in t)
            //{
            //    if (g.ContainsKey(c))
            //    {
            //        res += new string(c, g[c]);
            //    }
            //}
            //return res;
        }
        static bool isCryptSolution(string[] crypt, char[][] solution)
        {
            var digits = new[] { new char[crypt[0].Length], new char[crypt[1].Length], new char[crypt[2].Length] };
            var lettersInMap = solution.Select(x => x[0]).ToArray();
            var indexOfLetter = 0;
            for (int i = 0; i < crypt.Length; i++)
            {
                for (int j = 0; j < crypt[i].Length; j++)
                {
                    indexOfLetter = Array.IndexOf(lettersInMap, crypt[i][j]);
                    digits[i][j] = solution[indexOfLetter][1];
                }
            }
            foreach (var digit in digits)
            {
                if (digit.Length == 1) continue;
                if (digit[0] == '0') return false;
            }
            var numbers = digits.Select(x => int.Parse(new string(x))).ToArray();
            return numbers[0] + numbers[1] == numbers[2];
        } // doesn't pass hidden tests 
        static string reverseSentence(string sentence)
        {
            return string.Join("", sentence.Split(' ').Reverse().ToArray());
        }
        static string stringReformatting(string s, int k)
        {
            const char dash = '-';
            var joint = string.Join("", s.Split('-').ToArray());
            var count = 0;
            var firstLength = joint.Length % k;
            var list = new List<char>();
            for (int i = 0; i < firstLength; i++)
            {
                list.Add(joint[i]);
            }
            if (firstLength != 0) list.Add(dash);
            for (int i = firstLength; i < joint.Length; i++)
            {
                list.Add(joint[i]);
                if ((i - firstLength + 1) % k == 0) list.Add(dash);
            }
            return new string(list.ToArray()).TrimEnd(dash);

            //s = s.Replace("-", "");
            //var r = "";
            //for (int i = 0; ++i <= s.Length;)
            //{
            //    r = (i % k == 0 && s.Length != i ? "-" : "") + s[s.Length - i] + r;
            //}
            //return r;

            //string a = s.Replace("-", ""); !!!!!!!!!!!!!!!!!!!!!!!<----------------------------------
            //for (int i = a.Length - k; i > 0; i -= k)
            //    a = a.Insert(i, "-");
            //return a;
        }
        static string[] chessQueen(string q)
        {
            var board = new[]
            {
                new[] { "a1", "a2", "a3", "a4", "a5", "a6", "a7", "a8"},
                new[] { "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8"},
                new[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8"},
                new[] { "d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8"},
                new[] { "e1", "e2", "e3", "e4", "e5", "e6", "e7", "e8"},
                new[] { "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8"},
                new[] { "g1", "g2", "g3", "g4", "g5", "g6", "g7", "g8"},
                new[] { "h1", "h2", "h3", "h4", "h5", "h6", "h7", "h8"}
            };
            var hit = new List<string>();
            var indexI = 0;
            var indexJ = 0;
            for (int i = 0; i < board.Length; i++) // find the index of q
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (q == board[i][j])
                    {
                        indexI = i;
                        indexJ = j;
                    }
                }
            }
            var reserveI = indexI;
            var reserveJ = indexJ;
            while (reserveI >= 0 && reserveJ >= 0) // diagonally left and up
            {
                hit.Add(board[reserveI][reserveJ]);
                reserveI--;
                reserveJ--;
            }
            reserveI = indexI;
            reserveJ = indexJ;
            while (reserveI >= 0 && reserveJ < board[0].Length) // diagonally right and up
            {
                hit.Add(board[reserveI][reserveJ]);
                reserveI--;
                reserveJ++;
            }
            reserveI = indexI;
            reserveJ = indexJ;
            while (reserveI < board.Length && reserveJ < board[0].Length) // diagonally right and down
            {
                hit.Add(board[reserveI][reserveJ]);
                reserveI++;
                reserveJ++;
            }
            reserveI = indexI;
            reserveJ = indexJ;
            while (reserveI < board.Length && reserveJ >= 0) // diagonally left and down
            {
                hit.Add(board[reserveI][reserveJ]);
                reserveI++;
                reserveJ--;
            }
            return board.SelectMany(inner => inner).Except(hit).Except(board[indexI]).Except(board.Select(x => x[indexJ])).OrderBy(x => x).ToArray();

            //int i, j;
            //string t;
            //List<string> L = new List<string>(), res = new List<string>();

            //for (i = 1; i < 9; i++)
            //{
            //    // Vertical Line
            //    L.Add(q[0] + "" + i);
            //    // Horizontal Line
            //    L.Add((char)('a' + i - 1) + "" + q[1]);

            //    L.Add((char)(q[0] - i) + "" + (char)(q[1] - i));
            //    L.Add((char)(q[0] - i) + "" + (char)(q[1] + i));
            //    L.Add((char)(q[0] + i) + "" + (char)(q[1] - i));
            //    L.Add((char)(q[0] + i) + "" + (char)(q[1] + i));
            //}
            //for (i = 1; i < 9; i++)
            //    for (j = 1; j < 9; j++)
            //    {
            //        t = (char)('a' + i - 1) + "" + (char)('1' + j - 1);
            //        if (!L.Contains(t))
            //            res.Add(t);
            //    }
            //return res.ToArray();
        }
        static int[][] rotateImage(int[][] a)
        {
            //int row = a.Length;
            //int col = a[0].Length;
            //int[][] res = new int[col][];
            //for (int i = 0; i < col; i++)
            //    res[i] = new int[row];
            //for (int i = 0; i < row; i++)
            //{
            //    for (int j = 0; j < col; j++)
            //    {
            //        res[j][row - 1 - i] = a[i][j];
            //    }
            //}
            //return res;

            var result = new int[a.Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = a.Select(x => x[i]).Reverse().ToArray();
            }
            return result;

            //int[][] res = new int[a.Length][];
            //for (int i = 0; i < a.Length; i++)
            //{
            //    res[i] = new int[a.Length];
            //    for (int j = 0; j < a.Length; j++)
            //    {
            //        res[i][a.Length - j - 1] = a[j][i];
            //    }
            //}
            //return res;
        }
        static int missingNumber(int[] arr)
        {
            //Array.Sort(arr); //fucking frenchy!
            //int i = 0;
            //while (arr.Contains(i)) i++;
            //return i;

            //return arr.Length * (arr.Length + 1) / 2 - arr.Sum();

            //return Enumerable.Range(0, arr.Length + 1).Except(arr).ToArray()[0];

            var sorted = arr.OrderBy(x => x).ToArray();
            var missing = -1;
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                if (sorted[i + 1] == sorted[i] + 1) continue;
                missing = sorted[i] + 1;
                break;
            }
            if (missing == -1 && sorted[0] != 0) return sorted[0] - 1;
            if (missing == -1 && sorted[0] == 0) return sorted[sorted.Length - 1] + 1;
            return missing;
        }
        static string[] innerRanges(int[] nums1, int l1, int r1)
        {
            var list = new List<string>();
            long l = l1, r = r1;
            long[] nums = nums1.Select(x=>(long)x).ToArray();
            if (nums.Length == 0)
            {
                if (l == r || r - l == 1) return new string[] { l.ToString() };
                if (r - l > 2) return new string[] { string.Format("{0}->{1}", l, r) };
            }
            //for l
            if (nums[0] > l && nums[0] - l > 2) list.Add(string.Format("{0}->{1}", l, nums[0] - 1));
            else if (nums[0] - l == 2) list.Add(string.Format("{0}->{1}", l, nums[0] - 1));
            else if (l == nums[0] - 1) list.Add(l.ToString());
            //for inner part
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] - nums[i - 1] == 2) list.Add((nums[i] - 1).ToString());
                else if (nums[i] - nums[i - 1] > 2) list.Add(string.Format("{0}->{1}", nums[i - 1] + 1, nums[i] - 1));
            }
            //for r
            if (nums[nums.Length - 1] < r && r - nums[nums.Length - 1] > 2) list.Add(string.Format("{0}->{1}", nums[nums.Length - 1] + 1, r));
            else if (r - nums[nums.Length - 1] == 2) list.Add(string.Format("{0}->{1}", nums[0]+1, r));
            else if (r - nums[nums.Length - 1] == 1) list.Add(r.ToString());
            return list.ToArray();
        }
    }
}
