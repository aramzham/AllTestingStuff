using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryFinally
{
    class Program
    {
        static int x =0;
        static string s = string.Empty;
        static void Main(string[] args)
        {
            Console.WriteLine(Method());
            Console.WriteLine(SomeMethod());
            Console.WriteLine(s);

            Console.ReadKey();
        }

        public static int Method()
        {
            try
            {
                return x;
            }
            finally
            {
                x = 1;
            }
        }

        public static string SomeMethod()
        {
            try
            {
                s = "try";
                return s;
            }
            finally
            {
                s = "finally";
            }
        }
    }
}
