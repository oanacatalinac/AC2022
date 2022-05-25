using ACAutomation.PageObjects;
using ACAutomation.PageObjects.InputDataBO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ACAutomation
{
    [TestClass]
    public class AddressTests
    {
        private IWebDriver driver;
        private AddEditAddressPage addEditAddressPage;
        private AddressesPage addressesPage;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            var btnSignIn = driver.FindElement(By.Id("sign-in"));
            btnSignIn.Click();

            var loginPage = new LoginPage(driver);
            var homePage = loginPage.LoginApplication("test@test.test", "test");
            addressesPage = homePage.NavigateToAddressesPage();   
        }

        [TestMethod]
        public void User_Should_Add_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                LastName = "AC LN",
                Address1 = "AC address1",
                City = "AC city",
                State = "Hawaii",
                ZipCode = "AC zipcode",
                Color = "#FF0000"
            };

            addEditAddressPage = addressesPage.NavigateToAddAddressPage();
            var addressDetailsPage = addEditAddressPage.AddEditAddress(inputData);

            //Assert.AreEqual("Message to be added", addressDetailsPage.LblSuccess.Text);
        }

        [TestMethod]
        public void User_Should_Edit_Address_Successfully()
        {
            var inputData = new AddAddressBO
            {
                FirstName = "AC First Name1 edit 1",
                LastName = "AC edit",
                Address1 = "AC address1 edit",
                City = "AC city edit",
                State = "Arizona",
                ZipCode = "AC zipc",
                Color = "#17A2B8"
            };

            addEditAddressPage = addressesPage.NavigateToEditAddressPage(inputData.FirstName);

            var addressDetailsPage = addEditAddressPage.AddEditAddress(inputData);

            Assert.AreEqual("Address was successfully updated.", addressDetailsPage.NoticeText);
        }

        [TestMethod]
        public void User_Should_Dismiss_Destroying_An_Address()
        {
            var addressToBeRemoved = "Pretty please don't edit/delete";

            addressesPage.DestroyAddress(addressToBeRemoved);
            addressesPage.DismissDestruction();

            Assert.IsTrue(addressesPage.BtnDelete(addressToBeRemoved).Displayed);
        }

            [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
