using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACAutomation.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement Username()
        {
            return driver.FindElement(By.Id("session_email"));
        }

        private IWebElement Password()
        {
            return driver.FindElement(By.Id("session_password"));
        }

        private IWebElement LoginClick()
        {
            return driver.FindElement(By.Name("commit"));
        }

        public void LoginApplication(string username, string password)
        {
            Username().SendKeys(username);
            Password().SendKeys(password);
            LoginClick().Click();
        }
    }
}
