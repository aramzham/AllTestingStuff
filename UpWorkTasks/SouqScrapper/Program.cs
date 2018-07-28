using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using SouqScrapper.Models;

namespace SouqScrapper
{
    class Program
    {
        private const string Section2Page1 = "?section=2&page=1";
        private const string Section2Page1WithoutQuestion = "section=2&page=1";
        private const string AllCategoriesLink = "https://uae.souq.com/ae-en/shop-all-categories/c/";
        private const int SleepTime = 1000;

        private static int count = 0;
        private static HttpClientHandler _handler = new HttpClientHandler() { AllowAutoRedirect = false };
        private static HttpClient _client = new HttpClient(_handler);
        private static HtmlDocument _doc = new HtmlDocument();
        private static HashSet<string> _allLinks = new HashSet<string>();

        static void Main(string[] args)
        {
            
        }



        static string GetHrefFromA(HtmlNode a)
        {
            if (a is null) return null;
            var href = a.GetAttributeValue("href", "def");
            return href == "def" ? null : href;
        }

        static HashSet<string> GetAllCategoryLinks(HttpClient client, HtmlDocument doc)
        {
            var links = new HashSet<string>();
            var categoryPageSource = client.GetStringAsync(AllCategoriesLink).GetAwaiter().GetResult();
            doc.LoadHtml(categoryPageSource);
            var groupedLists = doc.DocumentNode.SelectNodes(".//div[@class='grouped-list']");
            if (groupedLists is null) return null;
            foreach (var groupedList in groupedLists)
            {
                var lisInGroup = groupedList.SelectNodes(".//li");
                if (lisInGroup is null) continue;
                foreach (var li in lisInGroup)
                {
                    if (!li.HasClass("parent"))
                    {
                        //continue;
                        var href = GetHrefFromA(li.SelectSingleNode(".//a"));
                        if (href != null) links.Add(href);
                    }
                    else
                    {
                        var subShopListLis = li.SelectNodes(".//ul[@class='sub-shop-list']//li");
                        if (subShopListLis is null) continue;
                        foreach (var subLi in subShopListLis)
                        {
                            var href = GetHrefFromA(subLi.SelectSingleNode(".//a"));
                            if (href != null) links.Add(href);
                        }
                    }
                }
            }

            return links;
        }

        

        

    }
}
