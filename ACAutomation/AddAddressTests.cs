﻿using ACAutomation.PageObjects;
using ACAutomation.PageObjects.InputDataBO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace ACAutomation
{
    [TestClass]
    public class AddAddressTests
    {
        private IWebDriver driver;
        private AddAddressPage addAddressPage;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            var btnSignIn = driver.FindElement(By.Id("sign-in"));
            btnSignIn.Click();
            Thread.Sleep(2000);

            var loginPage = new LoginPage(driver);
            loginPage.LoginApplication("test@test.test", "test");

            var homePage = new HomePage(driver);
            homePage.NavigateToAddressesPage();

            var addressesPage = new AddressesPage(driver);
            Thread.Sleep(2000);

            addAddressPage = addressesPage.NavigateToAddAddressPage();
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void User_Should_Add_Address_Successfully()
        {
            addAddressPage.AddAddress(new AddAddressBO());

            //var addressDetailsPage = new AddressDetailsPage(driver);
            //Assert.AreEqual("Message to be added", addressDetailsPage.LblSuccess.Text);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}