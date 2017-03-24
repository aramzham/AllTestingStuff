using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiplyBigIntegers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Multiply method vs BigIntegers!";
            //Console.WriteLine(Add("123", "987"));
            //Console.WriteLine(MultiplySimple("123", "10"));
            var result = string.Empty;

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = Multiply("1967869876756567845", "14567890123456789012");
            }
            sw.Stop();
            Console.WriteLine($"for 100000 iterations multiply consumed {sw.ElapsedMilliseconds}ms");

            //Console.WriteLine(Multiply("1967869876756567845", "14567890123456789012"));
            BigInteger big1 = 1967869876756567845;
            var big2 = new BigInteger(14567890123456789012);
            var resultBig = new BigInteger();

            sw.Reset();

            sw.Start();
            for (int i = 0; i < 100000; i++)
            {
                resultBig = big1*big2;
            }
            sw.Stop();
            Console.WriteLine($"for 100000 iterations biginteger consumed {sw.ElapsedMilliseconds}ms");

            //Console.WriteLine(big1*big2);

            Console.ReadKey();
        }

        static string Add(string a, string b)
        {
            string n1 = a.TrimStart('0');
            string n2 = b.TrimStart('0');
            StringBuilder result = new StringBuilder();
            if (n1.Length > n2.Length)
            {
                n2 = n2.PadLeft(n1.Length, '0');
            }
            else if (n1.Length < n2.Length)
            {
                n1 = n1.PadLeft(n2.Length, '0');
            }
            char[] str1 = n1.ToCharArray();
            char[] str2 = n2.ToCharArray();

            sbyte remainder = 0;
            sbyte addition = 0;
            for (int i = str1.Length - 1; i >= 0; i--)
            {
                sbyte num1 = sbyte.Parse(str1[i].ToString());
                sbyte num2 = sbyte.Parse(str2[i].ToString());

                num1 += addition;
                addition = 0;
                if (num1 + num2 < 10)
                {
                    result.Append(num1 + num2);
                }
                else
                {
                    remainder = (sbyte)((num1 + num2) % 10);
                    result.Append(remainder);
                    addition = (sbyte)((num1 + num2) / 10);
                }
            }
            if (addition != 0)
            {
                result.Append(addition);
            }

            char[] endResult = result.ToString().ToCharArray();
            Array.Reverse(endResult);

            return string.Join("", endResult);
        }

        static string MultiplySimple(string a, string b)
        {
            var result = string.Empty;
            for (int i = 0; i < int.Parse(b); i++)
            {
                result = Add(a, result);
            }
            return result;
        }

        static string Multiply(string a, string b)
        {
            var array = new string[b.Length];
            var sb = new StringBuilder();
            var result = string.Empty;
            for (int i = 0; i < b.Length; i++)
            {
                array[i] = sb.Append(MultiplySimple(a, b[i].ToString())).Append(new string('0', b.Length - 1 - i)).ToString();
                sb.Clear();
            }
            return array.Aggregate(result, (current, t) => Add(t, current));
        }
    }
}
