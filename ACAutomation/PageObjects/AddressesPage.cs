using ACAutomation.Helpers;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace ACAutomation.PageObjects
{
    public class AddressesPage
    {
        private IWebDriver driver;

        public AddressesPage(IWebDriver browser)
        {
            driver = browser;
        }

        private By Addresses = By.CssSelector("tbody tr");
        public IList<IWebElement> LstAddresses => driver.FindElements(Addresses);

        private By EditAddress = By.CssSelector("a[data-test*=edit]");
        public IWebElement BtnEdit(string addressName) => LstAddresses
            .FirstOrDefault(element => element.Text.Contains(addressName))
            .FindElement(EditAddress);

        private By DeleteAddress = By.CssSelector("a[data-test*=destroy]");
        public IWebElement BtnDelete(string addressName) => LstAddresses
            .FirstOrDefault(element => element.Text.Contains(addressName))
            .FindElement(DeleteAddress);

        private By NewAddress = By.CssSelector("a[data-test=create]");
        private IWebElement BtnNewAddress => driver.FindElement(NewAddress);

        public AddEditAddressPage NavigateToAddAddressPage()
        {
            WaitHelpers.WaitForElementToBeVisible(driver, NewAddress);

            BtnNewAddress.Click();
            return new AddEditAddressPage(driver);
        }

        public AddEditAddressPage NavigateToEditAddressPage(string addressName)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, EditAddress);

            BtnEdit(addressName).Click();
            return new AddEditAddressPage(driver);
        }

        public void DestroyAddress(string addressName)
        {
            WaitHelpers.WaitForElementToBeVisible(driver, DeleteAddress);

            BtnDelete(addressName).Click();
        }

        public void ConfirmDestruction()
        {
            driver.SwitchTo().Alert().Accept();
        }

        public void DismissDestruction()
        {
            driver.SwitchTo().Alert().Dismiss();
        }
    }
}
