using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Designer_PDF_Viewer
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] h_temp = Console.ReadLine().Split(' ');
            //int[] h = Array.ConvertAll(h_temp, Int32.Parse);
            string word = Console.ReadLine();
            var h = new[] { 1, 3, 1, 3, 1, 4, 1, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            Console.WriteLine(word?.Select(c => c - 97).Select(i => h[i]).Max() * word?.Length);
        }
    }
}
