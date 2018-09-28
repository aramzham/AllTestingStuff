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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AnnuaireLjaFr
{
    class Program
    {
        private const string MainUrl = "http://annuaire.lja.fr";
        static void Main(string[] args)
        {
            //var option = new ChromeOptions();
            //option.AddArgument("no-sandbox");
            //option.AddArgument("--start-maximized");
            //option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            //option.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);

            //var driver = new ChromeDriver(option);
            //driver.Navigate().GoToUrl(MainUrl);
            var doc = new HtmlDocument();
            var handler= new HttpClientHandler();
            handler.Proxy = new WebProxy("212.237.31.42:80");
            var client = new HttpClient(handler);
            
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");

            var mainContent = client.GetStringAsync(MainUrl).GetAwaiter().GetResult();
            doc.LoadHtml(mainContent);
            //Thread.Sleep(4000);

            //var selections = driver.FindElementById("IDrcd");
            var selections = doc.DocumentNode.SelectSingleNode(".//select[@id='IDrcd']");
            //driver.ExecuteScript("arguments[0].click()", selections);
            //selections.Click();
            //var optionTexts = selections.FindElements(By.XPath(".//option[@value]")).Select(x => x.Text).ToList();
            var optionTexts = selections.SelectNodes(".//option").Select(x=>WebUtility.HtmlDecode(x.GetAttributeValue("value",string.Empty).Trim(' ', '\n', '\r', '\t'))).ToList();
            foreach (var optionText in optionTexts)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(optionText)) continue;
                    //driver.ExecuteScript("arguments[0].click();", optionElement);
                    //driver.Navigate().GoToUrl("http://annuaire.lja.fr/annuaire/search?cle=&rcv=&rcd={searchPart}&hajarechgo=1&rcrech=C&nom=&prenom=&rcspe=&rcspec=&rcl=&rcs=&rcvi=&rcpays=&hajavrechgo=0&triordrealpha=asc&page=1".Replace("{searchPart}", WebUtility.UrlEncode(optionText)));
                    var domainContent = client.GetStringAsync("http://annuaire.lja.fr/annuaire/search?cle=&rcv=&rcd={searchPart}&hajarechgo=1&rcrech=C&nom=&prenom=&rcspe=&rcspec=&rcl=&rcs=&rcvi=&rcpays=&hajavrechgo=0&triordrealpha=asc&page=1".Replace("{searchPart}", WebUtility.UrlEncode(optionText))).GetAwaiter().GetResult();
                    doc = new HtmlDocument();
                    doc.LoadHtml(domainContent);
                    Thread.Sleep(2000);
                    //var findInput = driver.FindElementById("IDhajarechgo");
                    //findInput.Click();
                    //var links = driver.FindElementsByXPath(".//a[@title='En savoir plus']").Select(x => x.GetAttribute("href")).ToList();
                    var links = doc.DocumentNode.SelectNodes(".//a[@title='En savoir plus']").Select(x =>$"{MainUrl}{ x.GetAttributeValue("href", string.Empty)}").ToList();
                    foreach (var link in links)
                    {
                        if(string.IsNullOrWhiteSpace(link)) continue;
                        try
                        {
                            //driver.Navigate().GoToUrl(link);
                            var cabinetContent = client.GetStringAsync(link).GetAwaiter().GetResult();
                            //var elements = driver.FindElementsByXPath(".//li[@prop='FICHEDETCON']");
                            //if (!elements.Any()) continue;
                            //driver.ExecuteScript("window.scrollTo(0, 600)");
                            //var connection = driver.FindElementByXPath(".//li[@prop='FICHEDETCON']");
                            //connection.Click();
                            doc = new HtmlDocument();
                            doc.LoadHtml(cabinetContent);
                            var responseModel = new ResponseModel();
                            responseModel.Domain = optionText;
                            responseModel.Email = new string(doc.DocumentNode.SelectSingleNode(".//a[@class='reverse']").InnerText.Trim(' ', '\n', '\r', '\t').Reverse().ToArray());
                            responseModel.Name = doc.DocumentNode.SelectSingleNode(".//h1[@class='aja-fichtop-nom']").InnerText.Trim(' ', '\n', '\r', '\t');
                            WriteToCsv(@"D:\Aram\test\annuaire.csv", responseModel);
                            Thread.Sleep(1000);
                        }
                        catch (Exception e)
                        {
                            File.AppendAllText(@"D:\Aram\test\brokenLinks.txt", $"{link}{Environment.NewLine}");
                        }
                    }
                }
                catch (Exception e)
                {
                    File.AppendAllText(@"D:\Aram\test\brokenLinks.txt", $"Couldn't open option = {optionText}{Environment.NewLine}");
                }
            }
        }

        static void WriteToCsv(string path, ResponseModel model)
        {
            File.AppendAllText(path, $"\"{model.Domain}\",\"{model.Name}\",\"{model.Email}\"{Environment.NewLine}");
        }
    }

    class ResponseModel
    {
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
