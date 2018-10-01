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
using OpenQA.Selenium.Firefox;

namespace Property24
{
    class Program
    {
        private const string MainUrl = "https://www.property24.com";
        private const string KwazuluNatal = "https://www.property24.com/for-sale/kwazulu-natal/2";
        static void Main(string[] args)
        {
            var option = new ChromeOptions();
            option.AddArgument("no-sandbox");
            option.AddArgument("--start-maximized");
            option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            option.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);

            var driver = new ChromeDriver(option);
            driver.Navigate().GoToUrl(KwazuluNatal);
            Thread.Sleep(4000);

            var regionAs = driver.FindElementsByClassName("p24_bold");
            var regionLinks = regionAs.Select(x => x.GetAttribute("href")).Where(x=>x != null).ToList();
            foreach (var regionLink in regionLinks)
            {
                driver.Navigate().GoToUrl(regionLink);
                var lastPage = int.Parse(driver.FindElementByXPath("(.//ul[@class='pagination']//li)[last()]").Text);
                for (int i = 1; i <= lastPage; i++)
                {
                    driver.Navigate().GoToUrl($"{regionLink}/p{i}");
                    var agentLinkNodes = driver.FindElements(By.XPath(".//div[starts-with(@class,'p24_tile')]//a[@title]"));
                    var agentLinks = agentLinkNodes.Select(x => x.GetAttribute("href")).ToList();
                    foreach (var agentLink in agentLinks)
                    {
                        try
                        {
                            driver.Navigate().GoToUrl(agentLink);
                            var h1Node = driver.FindElementByXPath(".//div[@class='pull-left']/h1");
                            var parentNode = h1Node.FindElement(By.XPath(".."));
                            var response = new ResponseModel();
                            response.Title1 = h1Node.Text;
                            response.Title2 = parentNode.Text.Replace(h1Node.Text, string.Empty).Trim(' ', '\r', '\n', '\t');
                            var contactA = driver.FindElementById("P24_ToggleAgentNumbersLink");
                            contactA.Click();
                            driver.ExecuteScript("arguments[0].click()", contactA);
                            response.ContactInfo = driver.FindElementById("P24_ListingContactNumbersDiv").Text.Trim(' ', '\r', '\n', '\t');
                            var emailA = driver.FindElementById("P24_ToggleAgentEmailLink");
                            emailA.Click();
                            driver.ExecuteScript("arguments[0].click()", emailA);
                            response.Email = driver.FindElementById("P24_listingContactEmailsDiv").Text.Trim(' ', '\r', '\n', '\t');

                            WriteToCsv(@"E:\test\p24.csv", response);
                        }
                        catch (Exception e)
                        {
                            File.AppendAllText(@"E:\test\brokenLinks.txt",$"{agentLink}{Environment.NewLine}");
                        }
                    }
                }
            }
        }

        static void WriteToCsv(string path, ResponseModel model)
        {
            File.AppendAllText(path, $"\"{model.Title1}\",\"{model.Title2}\",\"{model.City}\",\"{model.ContactInfo}\",\"{model.Email}\"{Environment.NewLine}");
        }
    }

    class ResponseModel
    {
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string City { get; set; }
        public string ContactInfo { get; set; }
        public string Email { get; set; }
    }
}

