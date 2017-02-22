using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var sites = new string[][]
            {
                new string[] {"a-b.c",   "a.c"},
                new string[] {"aa-b.c",  "a-b.c"},
                new string[] {"bb-b.c",  "a-b.c"},
                new string[] {"cc-b.c",  "a-b.c"},
                new string[] {"d-cc-b.c","bb-b.c"},
                new string[] {"e-cc-b.c","bb-b.c"}
            };
            foreach (var s in domainForwarding(sites))
            {
                foreach (var s1 in s)
                {
                    Console.Write($"{s1}\t");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
        static string[] domainType(string[] domains)
        {
            //.org - organization, .com - commercial, .net - network, .info - information
            for (int i = 0; i < domains.Length; i++)
            {
                if (domains[i].EndsWith(".org")) domains[i] = "organization";
                if (domains[i].EndsWith(".com")) domains[i] = "commercial";
                if (domains[i].EndsWith(".net")) domains[i] = "network";
                if (domains[i].EndsWith(".info")) domains[i] = "information";
            }
            return domains;
        }
        static string[][] domainForwarding(string[][] redirects)
        {
            var groups = new List<List<string>>();
            var gr = redirects.Select(x => x[1]).Distinct().ToArray();
            for (int i = 0; i < gr.Length; i++)
            {
                groups.Add(new List<string>() { gr[i] });
                for (int j = 0; j < redirects.Length; j++)
                {
                    if (redirects[j][1] == gr[i]) groups[i].Add(redirects[j][0]);
                }
            }
            var index = 0;
            for (int i = 0; i < groups.Count; i++)
            {
                for (int j = 1; j < groups[i].Count; j++)
                {
                    if (groups.Select(x => x[0]).Contains(groups[i][j]))
                    {
                        index = Array.IndexOf(groups.Select(x => x[0]).ToArray(), groups[i][j]);
                        groups[i].AddRange(groups[index].Skip(1));
                        groups.RemoveAt(index);
                    }
                }
            }
            var sorted = groups.Select(x => x.OrderBy(z => z).ThenBy(r => r.Length).ToArray()).ToArray();
            sorted = sorted.OrderByDescending(inner => inner[1]).ToArray();
            //var sorted2 = sorted1.Select(x => x[0]).OrderBy(x => x).ThenBy(c => c.Length).ToArray();

            //return groups.Select(x => x.ToArray()).ToArray();
            return sorted;
        } //lexicographical fuck
    }
}