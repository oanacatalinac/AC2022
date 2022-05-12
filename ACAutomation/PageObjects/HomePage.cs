using ACAutomation.Helpers;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }

        private By Addresses = By.CssSelector("a[data-test=addresses]");
        private IWebElement BtnAddresses => driver.FindElement(Addresses);

        public AddressesPage NavigateToAddressesPage()
        {
            WaitHelpers.WaitForElementToBeVisible(driver, Addresses);
            BtnAddresses.Click();

            return new AddressesPage(driver);
        }
    }
}
