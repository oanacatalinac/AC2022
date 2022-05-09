using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement TxtEmail => driver.FindElement(By.Id("session_email"));

        private IWebElement TxtPassword => driver.FindElement(By.Id("session_password"));

        private IWebElement BtnLogin => driver.FindElement(By.Name("commit"));

        private IWebElement LblErrorMessage => driver.FindElement(By.CssSelector(".alert-notice"));

        public HomePage LoginApplication(string username, string password)
        {
            TxtEmail.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();

            return new HomePage(driver);
        }

        public string ErrorMessage => LblErrorMessage.Text;
    }
}
