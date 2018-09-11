using System;
using System.Collections.Generic;
using System.IO;
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
            var loginButton = driver.FindElementByXPath(".//a[@href='/login']");
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
                var aSharp = cardHandleName.FindElement(By.TagName("a"));
                aSharp.Click();
                var wrapper = driver.FindElementById("wrapper");
                var psm = wrapper.FindElement(By.ClassName("p-sm"));
                var colmd8 = psm.FindElement(By.ClassName("col-md-8"));
                var model = new HeepsyModel();
                model.InstagramUrl = colmd8.FindElement(By.XPath(".//h4//a")).GetAttribute("href");
                model.InstagramName = colmd8.FindElement(By.XPath(".//h4/a")).Text;
                model.Email = colmd8.FindElement(By.Id("sidebarProfile_personal-info")).Text.Trim();
                model.Name = colmd8.FindElement(By.Id("sidebarProfile_full_name")).Text.Trim();
                model.NumberOfFollowers = psm.FindElement(By.Id("sidebarProfile_follower_count")).Text.Trim();
                WriteToFile(model);
                var closeButton = wrapper.FindElement(By.Id("sidebar-close"));
                closeButton.Click();
            }
            //}
        }

        public static void WriteToFile(HeepsyModel model)
        {
            File.AppendAllText(@"E:\test\heepsy.txt", $"{model.InstagramName}, {model.Name}, {model.Email}, {model.NumberOfFollowers}, {model.InstagramUrl}{Environment.NewLine}");
        }
    }

    public class HeepsyModel
    {
        public string InstagramName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string NumberOfFollowers { get; set; }
        public string InstagramUrl { get; set; }
    }
}
