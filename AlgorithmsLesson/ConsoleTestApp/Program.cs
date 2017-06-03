using System;
using ArrayService;

namespace ConsoleTestApp
{
    class Program
    {
        //TODO: gnahatel insertion sort-i bardutyun@, binary sortinn el,
        static void Main(string[] args)
        {
            do
            {
                var a = ArrayHelper.CreateArray();
                ArrayHelper.ShowArray(a);

                MergeSort(a, 0, a.Length - 1);
                ArrayHelper.ShowArray(a);

                int x = 100, y = 200;
                ArrayHelper.Swap(ref x, ref y);
                Console.WriteLine($"x = {x}, y = {y}");

            } while (Console.ReadKey().Key == ConsoleKey.RightArrow);
        }

        static void InsertionSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                var temp = a[i];
                var k = i;
                for (; k > 0 && temp < a[k - 1]; k--)
                {
                    a[k] = a[k - 1];
                }
                a[k] = temp;
            }
        }

        static void BinarySort(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                var first = 0;
                var last = i;
                var mid = last / 2;
                while (last >= first) //gtnum a tex@, insert chi anum
                {
                    if (a[i + 1] == a[mid]) break;
                    if (a[i + 1] > a[mid]) first = mid + 1;
                    else last = mid - 1;

                    mid = first + (last - first) / 2;
                }

                var temp = a[i + 1];
                for (int j = i + 1; j > mid; j--)
                {
                    a[j] = a[j - 1];
                }
                a[mid] = temp;
            }
        }

        static void MergeSort(int[] a, int first, int last)
        {
            if (first < last)
            {
                var mid = (first + last) / 2;
                MergeSort(a, first, mid);
                MergeSort(a, mid + 1, last);
                Merge(a, first, mid, last);
            }
        }

        static void Merge(int[] a, int first, int mid, int last)
        {
            var temp = new int[a.Length];
            var first1 = first;
            var last1 = mid;
            var first2 = mid + 1;
            var last2 = last;
            var i = first;
            for (; first1 < last1 && first2 < last2; i++)
            {
                if (a[first1] < a[first2])
                {
                    temp[i] = a[first1];
                    first1++;
                }
                else
                {
                    temp[i] = a[first2];
                    first2++;
                }
            }
            if (first1 < last1)
            {
                for (; first1 <= last1; i++)
                {
                    temp[i] = a[first1++];
                }
            }
            else
            {
                for (; first2 <= last2; i++)
                {
                    temp[i] = a[first2++];
                }
            }
            var k = 0;
            for (var j = first; k < last; j++)
            {
                a[j] = temp[k++];
            }
        }
    }
}
