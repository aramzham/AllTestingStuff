using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace BringMeSomeContacts
{
    class Program
    {
        static void Main(string[] args)
        {
            var login = ConfigurationManager.AppSettings["login"];
            var pass = ConfigurationManager.AppSettings["password"];
            var count = 0;
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.linkedin.com/mynetwork/");
            var signInA = driver.FindElementByXPath(".//p[@class='signin-link']//a");
            var signInLink = signInA.GetAttribute("href");
            driver.Navigate().GoToUrl(signInLink);
            while (true)
            {
                while (count != 100)
                {
                    driver.ExecuteScript("window.scrollTo(0, 12000)");
                    Thread.Sleep(1000);
                    count++;
                }
                var buttons = driver.FindElementsByXPath(".//button[@data-control-name='invite']");
                foreach (var button in buttons)
                {
                    button.Click();
                    Thread.Sleep(1000);
                } 
            }
        }
    }
}
