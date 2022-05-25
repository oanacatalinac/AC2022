using ACAutomation.PageObjects;
using OpenQA.Selenium;

namespace ACAutomation.Shared
{
    public class MenuItemControlLoggedOut : MenuItemControl
    {
        private IWebDriver driver;

        public MenuItemControlLoggedOut(IWebDriver browser) : base(browser)
        {
            driver = browser;
        }

        private By SignIn = By.Id("sign-in");
        public IWebElement BtnSignIn => driver.FindElement(SignIn);

        public LoginPage NavigateToLoginPage()
        {
            BtnSignIn.Click();

            return new LoginPage(driver);
        }

    }
}
