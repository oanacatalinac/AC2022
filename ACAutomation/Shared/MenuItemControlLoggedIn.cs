using OpenQA.Selenium;

namespace ACAutomation.Shared
{
    public class MenuItemControlLoggedIn : MenuItemControl
    {
        private IWebDriver driver;

        public MenuItemControlLoggedIn(IWebDriver browser) : base(browser)
        {
            driver = browser;
        }

        public By Addresses = By.CssSelector("a[data-test=addresses]");
        public IWebElement BtnAddresses => driver.FindElement(Addresses);

        private By SignOut= By.CssSelector("");
        public IWebElement BtnSignOut => driver.FindElement(SignOut);

        private By UserEmail = By.XPath("//span[@data-test='current-user']");
        public IWebElement LblUserEmail => driver.FindElement(UserEmail);

        public string UserEmailText => LblUserEmail.Text;
    }
}
