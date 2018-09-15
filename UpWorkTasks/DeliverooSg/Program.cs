using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace DeliverooSg
{
    class Program
    {
        private const string MainUrl = "https://deliveroo.com.sg";

        static void Main(string[] args)
        {
            var doc = new HtmlDocument();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            var s = client.GetStringAsync("https://deliveroo.com.sg/sitemap").GetAwaiter().GetResult();
            doc.LoadHtml(s);
            var @as = doc.DocumentNode.SelectNodes(".//ul[@class='no-ui sitemap--zones']/li/a");
            foreach (var a in @as)
            {
                try
                {
                    var list = new List<RestaurantModel>();
                    var link = a.GetAttributeValue("href", string.Empty);
                    var restsContent = client.GetStringAsync($"{MainUrl}{link}").GetAwaiter().GetResult();
                    doc = new HtmlDocument();
                    doc.LoadHtml(restsContent);
                    var restAs = doc.DocumentNode.SelectNodes(".//li[contains(@class,'RestaurantsList-')]"); // TODO: brings 9 links
                    foreach (var restA in restAs)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            var concreteRestContent = client.GetStringAsync(restA.GetAttributeValue("href", string.Empty)).GetAwaiter().GetResult();
                            doc = new HtmlDocument();
                            doc.LoadHtml(concreteRestContent);
                            var restaurant = new RestaurantModel();
                            restaurant.CompanyName = ToNormalString(doc.DocumentNode.SelectSingleNode(".//h1[@class='restaurant__name']").InnerText);
                            restaurant.Rating = ToNormalString(doc.DocumentNode.SelectSingleNode(".//div[starts-with(@class,'orderweb')]").InnerText);
                            restaurant.ShortDescription = $"{ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='price-category']").InnerText)} / {ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='restaurant__metadata-tags']").InnerText)}";
                            restaurant.Address = ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='address']").InnerText);
                            restaurant.Phone = ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='phone']").InnerText);
                            restaurant.OpeningHours = ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='opening-hours']").InnerText);
                            restaurant.OtherInfo = "no other info";

                            list.Add(restaurant);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            continue;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }
            }
        }

        static void WriteToFile(List<RestaurantModel> rests, string name)
        {
            var a = new RestaurantModel();

            File.AppendAllText($@"E:\test\{name}.csv", $"{nameof(a.CompanyName)},{nameof(a.ShortDescription)},{nameof(a.Address)},{nameof(a.Rating)},{nameof(a.Url)},{nameof(a.Phone)},{nameof(a.OpeningHours)},{nameof(a.OtherInfo)}{Environment.NewLine}");

            foreach (var rest in rests)
            {
                var str = $"\"{rest.CompanyName}\",\"{rest.ShortDescription}\",\"{rest.Address}\",\"{rest.Rating}\",\"{rest.Url}\",\"{rest.Phone}\",\"{rest.OpeningHours}\",\"{rest.OtherInfo}\"{Environment.NewLine}";
                File.AppendAllText($@"E:\test\{name}.csv", str);
            }
        }

        static string ToNormalString(string s)
        {
            return s is null ? null : WebUtility.HtmlDecode(s.Trim(' ', '\r', '\n', '\t'));
        }
    }

    public class RestaurantModel
    {
        public string CompanyName { get; set; }
        public string ShortDescription { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public string OpeningHours { get; set; }
        public string OtherInfo { get; set; }
    }
}
