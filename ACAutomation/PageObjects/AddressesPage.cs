using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class AddressesPage
    {
        private IWebDriver driver;

        public AddressesPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement BtnNewAddress => driver.FindElement(By.CssSelector("a[data-test=create]"));

        public AddAddressPage NavigateToAddAddressPage()
        {
            BtnNewAddress.Click();
            return new AddAddressPage(driver);
        }
    }
}
