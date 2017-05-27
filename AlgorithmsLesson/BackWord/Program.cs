using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackWord
{
    class Program
    {
        static void Main(string[] args)
        {
            ReverseWordFromStart("Armen");

            Console.ReadKey();
        }

        private static void ReverseWord(string s)
        {
            if (s.Length < 1) return;
            var s1 = s.Substring(0, s.Length - 1);
            Console.Write(s[s.Length - 1]);
            ReverseWord(s1);
        }

        private static void ReverseWordFromStart(string s)
        {
            if (s.Length < 1) return;
            var s1 = s.Substring(0, s.Length - 1);
            ReverseWord(s1);
            Console.Write(s[s.Length - 1]);
        }
    }
}
