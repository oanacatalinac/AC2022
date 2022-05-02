using OpenQA.Selenium;

namespace ACAutomation.PageObjects
{
    public class AddAddressPage
    {
        private IWebDriver driver;

        public AddAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

        private IWebElement TxtFirstName => driver.FindElement(By.Id("address_first_name"));

        private IWebElement TxtLastName => driver.FindElement(By.CssSelector("input[name='address[last_name]']"));

        private IWebElement TxtAddress1 => driver.FindElement(By.XPath("//input[@name='address[address1]']"));

        private IWebElement TxtCity => driver.FindElement(By.Id("address_city"));

        private IWebElement TxtZipCode => driver.FindElement(By.Id("address_zip_code"));

        private IWebElement BtnCreateAddress => driver.FindElement(By.XPath("//input[@value='Create Address']"));

        public void AddAddress(string firstName, string lastName, string address1, string city, string zipCode)
        {
            TxtFirstName.SendKeys(firstName);
            TxtLastName.SendKeys(lastName);
            TxtAddress1.SendKeys(address1);
            TxtCity.SendKeys(city);
            TxtZipCode.SendKeys(zipCode);
            BtnCreateAddress.Click();
        }
    }
}
