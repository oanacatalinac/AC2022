using ACAutomation.Helpers;
using ACAutomation.PageObjects.InputDataBO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace ACAutomation.PageObjects
{
    public class AddAddressPage
    {
        private IWebDriver driver;

        public AddAddressPage(IWebDriver browser)
        {
            driver = browser;
        }

        private By FirstName = By.Id("address_first_name");
        private IWebElement TxtFirstName => driver.FindElement(FirstName);

        private By LastName = By.CssSelector("input[name='address[last_name]']");
        private IWebElement TxtLastName => driver.FindElement(LastName);

        private By Address1 = By.XPath("//input[@name='address[address1]']");
        private IWebElement TxtAddress1 => driver.FindElement(Address1);

        private By City = By.Id("address_city");
        private IWebElement TxtCity => driver.FindElement(City);

        private By State = By.Id("address_state");
        private IWebElement DdlState => driver.FindElement(State);

        private By ZipCode = By.Id("address_zip_code");
        private IWebElement TxtZipCode => driver.FindElement(ZipCode);

        private By Country = By.CssSelector("input[type=radio]");
        private IList<IWebElement> LstCountry => driver.FindElements(Country);

        private By Birthday = By.Id("address_birthday");
        private IWebElement TxtBirthday => driver.FindElement(Birthday);

        private By Color = By.Id("address_color");
        private IWebElement ClColor => driver.FindElement(Color);

        private By CreateAddress = By.XPath("//input[@value='Create Address']");
        private IWebElement BtnCreateAddress => driver.FindElement(CreateAddress);

        public AddressDetailsPage AddAddress(AddAddressBO address)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, CreateAddress);

            TxtFirstName.SendKeys(address.TxtFirstName);
            TxtLastName.SendKeys(address.TxtLastName);
            TxtAddress1.SendKeys(address.TxtAddress1);
            TxtCity.SendKeys(address.TxtCity);

            // select from drop-down
            var selectState = new SelectElement(DdlState);
            selectState.SelectByText(address.TxtState);

            TxtZipCode.SendKeys(address.TxtZipCode);

            // select radio button value -> country
            LstCountry[1].Click();

            TxtBirthday.SendKeys(address.TxtBirthdate);

            // select color from color picker
            var js = (IJavaScriptExecutor)driver;
            // js.ExecuteScript(script, arguments);
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, address.TxtColor);

            BtnCreateAddress.Click();

            return new AddressDetailsPage(driver);
        }
    }
}
