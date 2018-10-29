﻿using System;
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
            var password = ConfigurationManager.AppSettings["password"];
            var count = 0;
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.linkedin.com/mynetwork/");
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://www.linkedin.com/uas/login?session_redirect=%2Fvoyager%2FloginRedirect%2Ehtml&fromSignIn=true&trk=uno-reg-join-sign-in");
            Thread.Sleep(2000);
            var loginInput = driver.FindElementById("session_key-login");
            loginInput.SendKeys(login);
            var passInput = driver.FindElementById("session_password-login");
            passInput.SendKeys(password);
            var signInButton = driver.FindElementById("btn-primary");
            signInButton.Click();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://www.linkedin.com/mynetwork/");
            Console.WriteLine("Press ESC to stop");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                while (count != 100)
                {
                    driver.ExecuteScript($"window.scrollTo(0, {count*10000})");
                    Thread.Sleep(1000);
                    count++;
                }
                var buttons = driver.FindElementsByXPath(".//button[@data-control-name='invite']");
                foreach (var button in buttons)
                {
                    driver.ExecuteScript("arguments[0].click();", button);
                    //button.Click();
                    Thread.Sleep(1000);
                }
                count = 0;
            }
        }
    }
}