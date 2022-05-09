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

        private IWebElement TxtFirstName => driver.FindElement(By.Id("address_first_name"));

        private IWebElement TxtLastName => driver.FindElement(By.CssSelector("input[name='address[last_name]']"));

        private IWebElement TxtAddress1 => driver.FindElement(By.XPath("//input[@name='address[address1]']"));

        private IWebElement TxtCity => driver.FindElement(By.Id("address_city"));

        private IWebElement DdlState => driver.FindElement(By.Id("address_state"));

        private IWebElement TxtZipCode => driver.FindElement(By.Id("address_zip_code"));

        private IList<IWebElement> LstCountry => driver.FindElements(By.CssSelector("input[type=radio]"));

        private IWebElement TxtBirthday => driver.FindElement(By.Id("address_birthday"));

        private IWebElement ClColor => driver.FindElement(By.Id("address_color"));

        private IWebElement BtnCreateAddress => driver.FindElement(By.XPath("//input[@value='Create Address']"));

        public AddressDetailsPage AddAddress(AddAddressBO address)
        {
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
