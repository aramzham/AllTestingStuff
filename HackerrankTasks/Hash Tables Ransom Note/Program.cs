﻿using System;
using System.Collections;

namespace Hash_Tables_Ransom_Note
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens_m = Console.ReadLine().Split(' '); //need to learn Hash Tables
            int m = Convert.ToInt32(tokens_m[0]);
            int n = Convert.ToInt32(tokens_m[1]);
            if (m < n) Console.WriteLine("No");
            string[] magazine = "give me one grand today night".Split(' ');//Console.ReadLine().Split(' ');
            string[] ransom = "give one grand today".Split(' ');//Console.ReadLine().Split(' ');
            bool no = false;
            //foreach (var item in ransom)
            //{
            //    if (!magazine.Contains(item))
            //    {
            //        Console.WriteLine("No");
            //        no = true;
            //        break;
            //    }
            //}
            //if(no == false) Console.WriteLine("Yes");
            var hashTable = new Hashtable();
            for (int i = 0; i < magazine.Length; i++)
            {
                hashTable.Add(i,magazine[i]);
            }
            foreach (var s in ransom)
            {
                if (!hashTable.ContainsValue(s))
                {
                    Console.WriteLine("No");
                    no = true;
                    break;
                }
            }
            if (no == false) Console.WriteLine("Yes");

            Console.ReadKey();
        }
    }
}
