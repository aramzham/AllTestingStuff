using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AnnuaireLjaFr
{
    class Program
    {
        private const string MainUrl = "http://annuaire.lja.fr";
        static void Main(string[] args)
        {
            var option = new ChromeOptions();
            option.AddArgument("no-sandbox");
            option.AddArgument("--start-maximized");
            option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            option.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);

            var driver = new ChromeDriver(option);
            driver.Navigate().GoToUrl(MainUrl);
            Thread.Sleep(4000);

            var selections = driver.FindElementById("IDrcd");
            driver.ExecuteScript("arguments[0].click()", selections);
            //selections.Click();
            var optionTexts = selections.FindElements(By.XPath(".//option[@value]")).Select(x => x.Text).ToList();
            foreach (var optionText in optionTexts)
            {
                try
                {
                    if (optionText == "Domaine d'intervention") continue;
                    //driver.ExecuteScript("arguments[0].click();", optionElement);
                    driver.Navigate().GoToUrl("http://annuaire.lja.fr/annuaire/search?cle=&rcv=&rcd={searchPart}&hajarechgo=1&rcrech=C&nom=&prenom=&rcspe=&rcspec=&rcl=&rcs=&rcvi=&rcpays=&hajavrechgo=0&triordrealpha=asc&page=1".Replace("{searchPart}", WebUtility.UrlEncode(optionText)));
                    Thread.Sleep(2000);
                    //var findInput = driver.FindElementById("IDhajarechgo");
                    //findInput.Click();
                    var links = driver.FindElementsByXPath(".//a[@title='En savoir plus']").Select(x => x.GetAttribute("href")).ToList();
                    foreach (var link in links)
                    {
                        try
                        {
                            driver.Navigate().GoToUrl(link);
                            var elements = driver.FindElementsByXPath(".//li[@prop='FICHEDETCON']");
                            if (!elements.Any()) continue;
                            driver.ExecuteScript("window.scrollTo(0, 600)");
                            var connection = driver.FindElementByXPath(".//li[@prop='FICHEDETCON']");
                            connection.Click();
                            var responseModel = new ResponseModel();
                            responseModel.Email = new string(driver.FindElementByClassName("reverse").Text.Reverse().ToArray());
                            responseModel.Name = driver.FindElementByXPath(".//h1[@class='aja-fichtop-nom']").Text;
                            WriteToCsv(@"E:\test\annuaire.csv", responseModel);
                        }
                        catch (Exception e)
                        {
                            File.AppendAllText(@"E:\test\brokenLinks.txt", $"{link}{Environment.NewLine}");
                        }
                    }
                }
                catch (Exception e)
                {
                    File.AppendAllText(@"E:\test\brokenLinks.txt", $"Couldn't open option = {optionText}{Environment.NewLine}");
                }
            }
        }

        static void WriteToCsv(string path, ResponseModel model)
        {
            File.AppendAllText(path, $"\"{model.Name}\",\"{model.Email}\"{Environment.NewLine}");
        }
    }

    class ResponseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
