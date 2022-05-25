using ACAutomation.Helpers;
using ACAutomation.Shared;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        // reference the menu item control
        public MenuItemControlLoggedIn menuItemControl => new MenuItemControlLoggedIn(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }

        public AddressesPage NavigateToAddressesPage()
        {
            WaitHelpers.WaitForElementToBeVisible(driver, menuItemControl.Addresses);
            menuItemControl.BtnAddresses.Click();

            return new AddressesPage(driver);
        }
    }
}
