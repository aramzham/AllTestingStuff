using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestsOfAllKinds
{
    public class WilliamHillSeleniumParser
    {
        private const string MainPageUrl = "http://sports.williamhill.com/bet/en-gb/betlive/all";

        private static bool _isFirstTime = true;

        private ChromeOptions _options;
        private ChromeDriver _driver;
        private List<string> _lastLinks;

        public BookmakerModelNew Parse()
        {
            var bookmaker = new BookmakerModelNew() { Name = "WilliamHill", Id = 9 };
            try
            {
                var doc = new HtmlDocument();
                if (_isFirstTime)
                {
                    _options = new ChromeOptions();
                    _options.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
                    _driver = new ChromeDriver(_options);
                    _isFirstTime = false;
                    _lastLinks = new List<string>();
                }
                _driver.Navigate().GoToUrl(MainPageUrl);

                try
                {
                    var okButton = _driver.FindElementById("yesBtn");
                    Thread.Sleep(20);
                    _driver.ExecuteScript("arguments[0].click();", okButton);
                }
                catch (Exception e)
                {
                    // ignore
                }

                while (!_driver.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                {
                    Thread.Sleep(20);
                }

                doc.LoadHtml(_driver.PageSource);
                var sportsHolder = doc.DocumentNode.SelectSingleNode(".//*[@id='sports_holder']");
                var sportDivs = sportsHolder.SelectNodes("./div[starts-with(@id,'ip_sport_')]");
                var currentLinks = new List<string>();
                foreach (var sportDiv in sportDivs)
                {
                    var sportNameDiv = sportDiv.SelectSingleNode(".//h2[@class='subtitlenew']");
                    if(sportNameDiv is null) continue;
                    var sportName = sportNameDiv.InnerText;
                    if(sportName!="Football") continue;
                    var ipSportTypeDiv = sportDiv.SelectSingleNode("./div[starts-with(@id,'ip_sport_')]");
                    if(ipSportTypeDiv is null) continue;
                    var ipTypeDivs = ipSportTypeDiv.SelectNodes("./div[starts-with(@id,'ip_type_')]");
                    if(ipTypeDivs is null) continue;
                    foreach (var ipTypeDiv in ipTypeDivs)
                    {
                        var h3LeagueDiv = ipTypeDiv.SelectSingleNode(".//h3//a[@href]");
                        if(h3LeagueDiv is null) continue;
                        var leagueName = h3LeagueDiv.InnerText;
                        var ipMktGrpTblDivs = ipTypeDiv.SelectNodes(".//tbody[starts-with(@id,'ip_mkt_grp_tbl_')]");
                        if(ipMktGrpTblDivs is null) continue;
                        foreach (var ipMktGrpTblDiv in ipMktGrpTblDivs)
                        {
                            var rowDivs = ipMktGrpTblDiv.SelectNodes(".//tr[starts-with(@id,'ip_row_')]");
                            if(rowDivs is null) continue;
                            foreach (var rowDiv in rowDivs)
                            {
                                var linkDiv = rowDiv.SelectSingleNode(
                                    ".//div[starts-with(@id,'more_bets_container_')]//a[starts-with(@id,'ip_more_bets_link_')]");
                                if(linkDiv is null) continue;
                                var link = linkDiv.Attributes["href"].Value;
                                if(!_lastLinks.Contains(link)) currentLinks.Add(link);
                                //_driver.Navigate().GoToUrl(link);
                                //Thread.Sleep(100); // get markets
                            }
                        }
                    }
                }

                foreach (var link in currentLinks)
                {
                    //_driver.FindElement(By.TagName("body")).SendKeys(Keys.Control + "t");
                    //_driver.SwitchTo().Window(_driver.WindowHandles.Last());
                    //_driver.Navigate().GoToUrl(link);
                     Task.Run(() => _driver.ExecuteScript($"window.open('{link}','_blank');"));
                    _driver.SwitchTo().Window(_driver.WindowHandles.First());
                }

                var tabs = _driver.WindowHandles;
                for (int i = 1; i < currentLinks.Count; i++)
                {
                    _driver.SwitchTo().Window(tabs[i]);
                    Thread.Sleep(100); // get markets
                    _driver.Close();
                }

                _driver.SwitchTo().Window(tabs[0]);
                _lastLinks = currentLinks.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return bookmaker;
        }
    }
}
