﻿using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement BtnAddresses => driver.FindElement(By.CssSelector("a[data-test=addresses]"));

        public void NavigateToAddressesPage()
        {
            BtnAddresses.Click();
        }
    }
}
