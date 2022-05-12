using ACAutomation.Helpers;
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

        private By NewAddress = By.CssSelector("a[data-test=create]");
        private IWebElement BtnNewAddress => driver.FindElement(NewAddress);

        public AddAddressPage NavigateToAddAddressPage()
        {
            WaitHelpers.WaitForElementToBeVisible(driver, NewAddress);

            BtnNewAddress.Click();
            return new AddAddressPage(driver);
        }
    }
}
