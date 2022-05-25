using ACAutomation.Helpers;
using ACAutomation.Shared;
using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        // reference the menu item control
        public MenuItemControlLoggedOut menuItemControl => new MenuItemControlLoggedOut(driver);

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        private By Email = By.Id("session_email");
        private IWebElement TxtEmail => driver.FindElement(Email);

        private By Password = By.Id("session_password");
        private IWebElement TxtPassword => driver.FindElement(Password);

        private By Login = By.Name("commit");
        private IWebElement BtnLogin => driver.FindElement(Login);

        private By ErrorMessageDisplayed = By.CssSelector(".alert-notice");
        private IWebElement LblErrorMessage => driver.FindElement(ErrorMessageDisplayed);

        public HomePage LoginApplication(string username, string password)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, Email);

            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();

            return new HomePage(driver);
        }

        public string ErrorMessage => LblErrorMessage.Text;
    }
}
