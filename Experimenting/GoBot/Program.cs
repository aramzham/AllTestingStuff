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
                new string[] {"godaddy.net","godaddy.com"},
                new string[] {"godaddy.org","godaddycares.com"},
                new string[] {"godady.com","godaddy.com"},
                new string[] { "godaddy.ne", "godaddy.net" }
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

            return groups.Select(x => x.ToArray()).ToArray();
        }
    }
}