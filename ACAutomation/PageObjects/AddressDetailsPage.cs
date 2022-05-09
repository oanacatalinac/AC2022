using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class AddressDetailsPage
    {
        private IWebDriver driver;

        public AddressDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        // locator to be added when "add" functionality will work
        public IWebElement LblSuccess => driver.FindElement(By.Id(""));
    }
}
