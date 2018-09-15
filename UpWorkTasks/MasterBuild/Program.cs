//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;

//namespace SeleniumTest
//{
//    class Program
//    {
//        private static string[] _typeOptions = new[] { "Home", "Reno", "Multi", "Dev" };
//        private static string[] _priceRanges = new[] { "U500", "O500" };
//        static void Main(string[] args)
//        {
//            var option = new ChromeOptions();
//            option.AddArgument("no-sandbox");
//            option.AddArgument("--start-maximized");
//            option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
//            option.AddUserProfilePreference("profile.default_content_setting_values.stylesheet", 2);
//            var driver = new ChromeDriver(option);
//            var list = new List<MasterBuilderContact>();

//            driver.Navigate().GoToUrl("http://www.mbansw.asn.au/Find-a-Master-Builder-Results/");
//            var typeSelect = driver.FindElementById("ctl00_MainContentPlaceHolder_uxDropZone1_columnDisplay_ctl00_controlcolumn_ctl00_WidgetHost_WidgetHost_widget_ctl00_ctl00_uxType");
//            typeSelect.Click();
//            //driver.ExecuteScript("arguments[0].click()", typeSelect);

//            foreach (var type in _typeOptions)
//            {
//                var typeOption = driver.FindElementByXPath($".//option[@value='{type}']");
//                typeOption.Click();

//                Thread.Sleep(2000);
//                var priceRangeSelect = driver.FindElementById("ctl00_MainContentPlaceHolder_uxDropZone1_columnDisplay_ctl00_controlcolumn_ctl00_WidgetHost_WidgetHost_widget_ctl00_ctl00_uxPrice");
//                priceRangeSelect.Click();
//                foreach (var range in _priceRanges)
//                {
//                    var rangeOption = driver.FindElementByXPath($".//option[@value='{range}']");
//                    rangeOption.Click();
//                    var searchButton = driver.FindElementById("ctl00_MainContentPlaceHolder_uxDropZone1_columnDisplay_ctl00_controlcolumn_ctl00_WidgetHost_WidgetHost_widget_ctl00_ctl00_uxSearch");
//                    searchButton.Click();
//                    Thread.Sleep(3000);

//                    while (true)
//                    {
//                        var ul = driver.FindElementByXPath(".//div[@id='ctl00_MainContentPlaceHolder_uxDropZone1_columnDisplay_ctl00_controlcolumn_ctl00_WidgetHost_dvContent']//ul[@class='mba-list-gray-background']");
//                        var lis = ul.FindElements(By.XPath("./li"));
//                        foreach (var li in lis)
//                        {
//                            try
//                            {
//                                list.Add(GetContact(li));
//                            }
//                            catch (Exception e)
//                            {
//                                Console.WriteLine(e);
//                            }
//                        }

//                        var nextButton = driver.FindElementByXPath(".//a[contains(text(), 'Next')]");
//                        if (nextButton is null) break;
//                        nextButton.Click();
//                        Thread.Sleep(4000);
//                    }
//                }
//                //driver.ExecuteScript("arguments[0].click()", typeOption);
//            }

//            list.ForEach(WriteToFile);
//        }

//        static MasterBuilderContact GetContact(IWebElement element)
//        {
//            var contact = new MasterBuilderContact();
//            var large = element.FindElement(By.ClassName("large"));
//            var split = large.Text.Split('\n').Select(x => x.Trim(' ', '\r', '\n', '\t')).ToList();
//            contact.CompanyName = split[0];
//            contact.DirectorName = split[1];
//            contact.Adress = split[2];
//            contact.Fax = split[3];
//            var orange = element.FindElement(By.XPath(".//li[@class='medium']//p[@class='orange']"));
//            contact.Telephone = orange.Text.Trim(' ', '\r', '\n', '\t');

//            return contact;
//        }

//        public static void WriteToFile(MasterBuilderContact model)
//        {
//            File.AppendAllText(@"D:\temp\heepsy.txt", $"{model.CompanyName}, {model.DirectorName}, {model.Adress}, {model.Fax}, {model.Telephone}{Environment.NewLine}");
//        }
//    }

//    public class MasterBuilderContact
//    {
//        public string CompanyName { get; set; }
//        public string DirectorName { get; set; }
//        public string Adress { get; set; }
//        public string Fax { get; set; }
//        public string Telephone { get; set; }
//    }
//}
