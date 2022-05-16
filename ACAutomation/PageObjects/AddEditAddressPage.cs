using ACAutomation.Helpers;
using ACAutomation.PageObjects.InputDataBO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace ACAutomation.PageObjects
{
    public class AddEditAddressPage
    {
        private IWebDriver driver;

        public AddEditAddressPage(IWebDriver browser)
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

        private By Color = By.Id("address_color");
        private IWebElement ClColor => driver.FindElement(Color);

        private By Submit = By.XPath("//input[@data-test='submit']");
        private IWebElement BtnSubmit => driver.FindElement(Submit);

        public AddressDetailsPage AddEditAddress(AddAddressBO address)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, Submit);

            TxtFirstName.Clear();
            TxtFirstName.SendKeys(address.FirstName);
            TxtLastName.Clear();
            TxtLastName.SendKeys(address.LastName);
            TxtAddress1.SendKeys(address.Address1);
            TxtCity.Clear();
            TxtCity.SendKeys(address.City);

            // select from drop-down
            var selectState = new SelectElement(DdlState);
            selectState.SelectByText(address.State);

            TxtZipCode.SendKeys(address.ZipCode);

            // select radio button value -> country
            LstCountry[1].Click();

            // select color from color picker
            var js = (IJavaScriptExecutor)driver;
            // js.ExecuteScript(script, arguments);
            js.ExecuteScript("arguments[0].setAttribute('value', arguments[1])", ClColor, address.Color);

            BtnSubmit.Click();

            return new AddressDetailsPage(driver);
        }
    }
}
