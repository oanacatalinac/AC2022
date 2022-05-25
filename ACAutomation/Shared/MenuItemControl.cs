using OpenQA.Selenium;

namespace ACAutomation.Shared
{
    public class MenuItemControl
    {
        private IWebDriver driver;

        public MenuItemControl(IWebDriver browser)
        {
            driver = browser;
        }

        private By Home = By.CssSelector("");
        private IWebElement BtnHome => driver.FindElement(Home);
    }
}
