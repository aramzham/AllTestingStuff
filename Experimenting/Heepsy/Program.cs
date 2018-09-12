using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            //https://www.heepsy.com/influencers?utf8=%E2%9C%93&filter%5Bfixed_search%5D=&filter%5Blocation_aal0%5D=France&filter%5Blocation_aal1%5D=&filter%5Blocation_type%5D=mixed_frequent_location&filter%5Bcategories_AND%5D%5B%5D=&filter%5Bmentions_AND_mobile%5D=&filter%5Bmentions_AND%5D%5B%5D=&filter%5Binstagram_followers_greater_than%5D=&filter%5Binstagram_followers_less_than%5D=&filter%5Binstagram_engagement_greater_than%5D=&filter%5Binstagram_engagement_less_than%5D=&filter%5Binstagram_emv_greater_than%5D=&filter%5Binstagram_emv_less_than%5D=&filter%5Border_by_property%5D=

            //https://www.heepsy.com/influencers?utf8=%E2%9C%93&filter%5Bfixed_search%5D=&filter%5Blocation_aal0%5D=France&filter%5Blocation_aal1%5D=&filter%5Blocation_type%5D=mixed_frequent_location&filter%5Bcategories_AND%5D%5B%5D=&filter%5Bmentions_AND_mobile%5D=&filter%5Bmentions_AND%5D%5B%5D=&filter%5Binstagram_followers_greater_than%5D=&filter%5Binstagram_followers_less_than%5D=&filter%5Binstagram_engagement_greater_than%5D=&filter%5Binstagram_engagement_less_than%5D=&filter%5Binstagram_emv_greater_than%5D=&filter%5Binstagram_emv_less_than%5D=&filter%5Border_by_property%5D=&page=1

            //https://www.heepsy.com/influencers?utf8=%E2%9C%93&filter%5Bfixed_search%5D=&filter%5Blocation_aal0%5D=France&filter%5Blocation_aal1%5D=&filter%5Blocation_type%5D=mixed_frequent_location&filter%5Bcategories_AND%5D%5B%5D=&filter%5Bmentions_AND_mobile%5D=&filter%5Bmentions_AND%5D%5B%5D=&filter%5Binstagram_followers_greater_than%5D=&filter%5Binstagram_followers_less_than%5D=&filter%5Binstagram_engagement_greater_than%5D=&filter%5Binstagram_engagement_less_than%5D=&filter%5Binstagram_emv_greater_than%5D=

            //https://www.heepsy.com/influencers?utf8=%E2%9C%93&filter%5Bfixed_search%5D=&filter%5Blocation_aal0%5D=France&filter%5Blocation_aal1%5D=&filter%5Blocation_type%5D=mixed_frequent_location&filter%5Bcategories_AND%5D%5B%5D=

            var option = new ChromeOptions();
            option.AddArgument("no-sandbox");
            option.AddArgument("--start-maximized");
            option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            option.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);

            var driver = new ChromeDriver(option);
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
            Thread.Sleep(500);

            driver.Navigate().GoToUrl("https://www.heepsy.com/influencers?utf8=%E2%9C%93&filter%5Bfixed_search%5D=&filter%5Blocation_aal0%5D=France&filter%5Blocation_aal1%5D=&filter%5Blocation_type%5D=mixed_frequent_location&filter%5Bcategories_AND%5D%5B%5D=");

            //var locationSelector = driver.FindElementById("select2-filter_location_aal0-container");
            //locationSelector.Click();
            //var searchField = driver.FindElementByClassName("select2-search__field");
            //searchField.Clear();
            //searchField.SendKeys("France");
            //searchField.SendKeys(Keys.Enter);
            //var filterResultsButton = driver.FindElementById("submit-filters");
            //filterResultsButton.Click();

            while (true)
            {
                var cardHandleNames = driver.FindElementsByClassName("hp-card-handle-and-name");
                for (var i = 0; i < cardHandleNames.Count; i++)
                {
                    if (i != 0 && i % 3 == 0) driver.ExecuteScript("scroll(0, 300);");
                    var cardHandleName = cardHandleNames[i];
                    var aSharp = cardHandleName.FindElement(By.TagName("a"));
                    driver.ExecuteScript("arguments[0].click()", aSharp);
                    //aSharp.Click();
                    var wrapper = driver.FindElementById("wrapper");
                    var psm = wrapper.FindElement(By.ClassName("p-sm"));
                    var colmd8 = psm.FindElement(By.ClassName("col-md-8"));
                    var model = new HeepsyModel();
                    model.InstagramUrl = colmd8.FindElement(By.XPath(".//h4//a")).GetAttribute("href");
                    model.InstagramName = colmd8.FindElement(By.XPath(".//h4/a")).Text;
                    model.Email = GetEmails(colmd8.FindElement(By.Id("sidebarProfile_personal-info")).Text.Trim());
                    model.Name = colmd8.FindElement(By.Id("sidebarProfile_full_name")).Text.Trim();
                    model.NumberOfFollowers = psm.FindElement(By.Id("sidebarProfile_follower_count")).Text.Trim();
                    model.Engagement = psm.FindElement(By.XPath(".//div[@class='metrics-value']//h4")).Text.Trim();
                    //WriteToFile(model);
                    Thread.Sleep(200);
                    var closeButton = wrapper.FindElement(By.Id("sidebar-close"));
                    driver.ExecuteScript("arguments[0].click()", closeButton);
                    //closeButton.Click();
                }

                var liNext = driver.FindElementByXPath(".//li[@class='next']");
                if (liNext is null) break;
                var aNext = liNext.FindElement(By.TagName("a"));
                driver.ExecuteScript("arguments[0].click()", aNext);
                //aNext.Click();
            }
            //}
        }

        public static void WriteToFile(HeepsyModel model)
        {
            File.AppendAllText(@"E:\test\heepsy.txt", $"{model.InstagramName}, {model.Name}, {model.Email}, {model.NumberOfFollowers}, {model.InstagramUrl}{Environment.NewLine}");
        }

        public static string GetEmails(string info)
        {
            var validator = new EmailAddressAttribute();
            return string.Join(" ", info.Split().Where(x => validator.IsValid(x)));
        }
    }

    public class HeepsyModel
    {
        public string InstagramName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string NumberOfFollowers { get; set; }
        public string InstagramUrl { get; set; }
        public string Engagement { get; set; }
    }
}
