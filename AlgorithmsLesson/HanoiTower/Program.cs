using System;
using System.Collections.Generic;

namespace HanoiTower
{
    class Program
    {
        //TODO: recursion-ov gtnel 2 tvi amenamec @ndhanur bajanarar@
        //TODO: aranc bajanman gorcoxutyunneri veradardznel tveri qanordi amboxj mas@
        //TODO: recursiayov aranc bazmapatkman gorcoxutyan veradardznum e 2 inegerneri artadryal@
        //TODO: unenq integer array, karucel rekursiv funkcia, vor@ veradardznum e zangvaci k-rd minimun@ (sortavorel chka)
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a number");
                var n = int.Parse(Console.ReadLine());
                var A = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    A.Add(n - i);
                    Console.Write($"{A[i]}\t");
                }

                var B = new List<int>();
                var C = new List<int>();
                Tower(n, A, B, C);
                ShowList(C);
            }
        }

        static void Tower(int n, List<int> source, List<int> bridge, List<int> destination)
        {
            if (n == 1)
            {
                destination.Add(source[source.Count - 1]);
                source.RemoveAt(source.Count - 1);
            }
            else
            {
                Tower(n - 1, source, destination, bridge);
                Tower(1, source, bridge, destination);
                Tower(n - 1, bridge, source, destination);
            }
        }

        private static void ShowList(IEnumerable<int> COLLECTION)
        {
            foreach (var i in COLLECTION)
            {
                Console.Write($"{i}\t");
            }
            Console.WriteLine();
        }
    }
}
