using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Heepsy
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            var doc = new HtmlDocument();
            driver.Navigate().GoToUrl("https://www.heepsy.com");
            var loginButton =driver.FindElementByXPath(".//a[@href='/login']");
            loginButton.Click();
            doc.LoadHtml(driver.PageSource);
            //if (doc.DocumentNode.SelectSingleNode(".//h3[text()='Log in']") != null)
            //{
            //var loginForm = doc.DocumentNode.SelectSingleNode(".//form[@id='loginForm']");
            //if (loginForm is null) return;
            var loginInput = driver.FindElementById("user_login");
            loginInput.Clear();
            loginInput.SendKeys("thomas.lemasle@pinotbleu.com");
            //var loginInput = loginForm.SelectSingleNode(".//input[@id='user_login']");
            //var passwordInput = loginForm.SelectSingleNode(".//input[@id='user_password']");
            //if(loginInput is null || passwordInput is null) return;
            var passwordInput = driver.FindElementById("user_password");
            passwordInput.Clear();
            passwordInput.SendKeys("aszdefrg");
            var log_inButton = driver.FindElementByXPath(".//input[@name='commit']");
            log_inButton.Click();
            var locationSelector = driver.FindElementById("select2-filter_location_aal0-container");
            locationSelector.Click();
            var searchField = driver.FindElementByClassName("select2-search__field");
            searchField.Clear();
            searchField.SendKeys("France");
            searchField.SendKeys(Keys.Enter);
            var filterResultsButton = driver.FindElementById("submit-filters");
            filterResultsButton.Click();

            var cardHandleNames = driver.FindElementsByClassName("hp-card-handle-and-name");
            foreach (var cardHandleName in cardHandleNames)
            {
                
            }
            //}
        }
    }
}
