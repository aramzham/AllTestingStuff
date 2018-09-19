using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DeliverooSg
{
    class Program
    {
        private const string MainUrl = "https://deliveroo.com.sg";

        static void Main(string[] args)
        {
            var option = new ChromeOptions();
            option.AddArgument("no-sandbox");
            option.AddArgument("--start-maximized");
            option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            option.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);
            var driver = new ChromeDriver(option);

            //var doc = new HtmlDocument();
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            //var s = client.GetStringAsync("https://deliveroo.com.sg/sitemap").GetAwaiter().GetResult();
            driver.Navigate().GoToUrl("https://deliveroo.com.sg/sitemap");
            //doc.LoadHtml(s);
            //var @as = doc.DocumentNode.SelectNodes(".//ul[@class='no-ui sitemap--zones']/li/a");
            var @as = driver.FindElementsByXPath(".//ul[@class='no-ui sitemap--zones']/li/a").ToDictionary(x => x.Text, x => x.GetAttribute("href"));
            foreach (var a in @as) // Yishun Ring Road, sranic sksac a petq
            {
                try
                {
                    var list = new List<RestaurantModel>();
                    //var link = a.GetAttribute("href");
                    driver.Navigate().GoToUrl(a.Value);
                    Thread.Sleep(3000);
                    driver.ExecuteScript("window.scrollTo(0, 125000)");
                    var restAs = driver.FindElementsByXPath(".//li[contains(@class,'RestaurantsList-')]//a[@href]").Select(x => x.GetAttribute("href")).ToList();
                    //var link = a.GetAttributeValue("href", string.Empty);
                    //var restsContent = client.GetStringAsync($"{MainUrl}{link}").GetAwaiter().GetResult();
                    //doc = new HtmlDocument();
                    //doc.LoadHtml(restsContent);
                    //var restAs = doc.DocumentNode.SelectNodes(".//li[contains(@class,'RestaurantsList-')]"); // TODO: brings 9 links
                    foreach (var restA in restAs)
                    {
                        Thread.Sleep(500);
                        try
                        {
                            //var concreteRestContent = client.GetStringAsync(restA.GetAttributeValue("href", string.Empty)).GetAwaiter().GetResult();
                            //doc = new HtmlDocument();
                            //doc.LoadHtml(concreteRestContent);
                            driver.Navigate().GoToUrl(restA);
                            var restaurant = new RestaurantModel();
                            //restaurant.CompanyName = ToNormalString(doc.DocumentNode.SelectSingleNode(".//h1[@class='restaurant__name']").InnerText);
                            //restaurant.Rating = ToNormalString(doc.DocumentNode.SelectSingleNode(".//div[starts-with(@class,'orderweb')]").InnerText);
                            //restaurant.ShortDescription = $"{ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='price-category']").InnerText)} / {ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='restaurant__metadata-tags']").InnerText)}";
                            //restaurant.Address = ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='address']").InnerText);
                            //restaurant.Phone = ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='phone']").InnerText);
                            //restaurant.OpeningHours = ToNormalString(doc.DocumentNode.SelectSingleNode(".//small[@class='opening-hours']").InnerText);
                            try
                            {
                                restaurant.CompanyName = ToNormalString(driver.FindElementByXPath(".//h1[@class='restaurant__name']").Text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            try
                            {
                                restaurant.Rating = ToNormalString(driver.FindElementByXPath(".//div[starts-with(@class,'orderweb')]").Text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            try
                            {
                                restaurant.ShortDescription = $"{ToNormalString(driver.FindElementByXPath(".//small[@class='price-category']").Text)} / {ToNormalString(driver.FindElementByXPath(".//small[@class='restaurant__metadata-tags']").Text)}";
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            try
                            {
                                restaurant.Address = ToNormalString(driver.FindElementByXPath(".//small[@class='address']").Text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            try
                            {
                                restaurant.Phone = ToNormalString(driver.FindElementByXPath(".//small[@class='phone']").Text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                            try
                            {
                                restaurant.OpeningHours = ToNormalString(driver.FindElementByXPath(".//small[@class='opening-hours']").Text);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            restaurant.Url = restA;
                            //restaurant.OtherInfo = "no other info";

                            list.Add(restaurant);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            continue;
                        }
                    }
                    WriteToFile(list, a.Key);
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

            File.AppendAllText($@"D:\Temp\{name}.csv", $"{nameof(a.CompanyName)},{nameof(a.ShortDescription)},{nameof(a.Address)},{nameof(a.Rating)},{nameof(a.Url)},{nameof(a.Phone)},{nameof(a.OpeningHours)}{Environment.NewLine}");

            foreach (var rest in rests)
            {
                var str = $"\"{rest.CompanyName}\",\"{rest.ShortDescription}\",\"{rest.Address}\",\"{rest.Rating}\",\"{rest.Url}\",\"{rest.Phone}\",\"{rest.OpeningHours}\"{Environment.NewLine}";
                File.AppendAllText($@"D:\Temp\{name}.csv", str);
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
    }
}
