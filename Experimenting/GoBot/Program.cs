using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var domain = "godaddy.com";
            var n = 7;
            Console.WriteLine(typosquatting(n, domain));

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
        static int typosquatting(int n, string domain)
        {
            var countSite = 0;
            var countDomain = 0;
            var site = domain.Split('.')[0];
            var suffix = domain.Split('.')[1];
            countSite = PossibleTypoCount(site);
            countDomain = PossibleTypoCount(suffix);
            return countSite + countDomain;
        }

        private static int PossibleTypoCount(string text)
        {
            var count = 0;
            var textCharArray = text.ToCharArray();
            for (int i = 0; i < text.Length - 1; i++)
            {
                Swap(ref textCharArray[i], ref textCharArray[i + 1]);
                if (new string(textCharArray) != text) count++;
                textCharArray = text.ToCharArray();
            }
            return count;
        }

        private static void Swap(ref char a, ref char b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}