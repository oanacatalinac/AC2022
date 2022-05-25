﻿using ACAutomation.PageObjects;
using ACAutomation.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ACAutomation
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;


        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            loginPage.menuItemControl.NavigateToLoginPage();
        }

        [TestMethod]
        public void User_Should_Login_Successfully()
        {
            loginPage.LoginApplication("test@test.test", "test");

            var homePage = new HomePage(driver);

            Assert.IsTrue(homePage.menuItemControl.UserEmailText.Equals("test@test.test"));
        }

        [TestMethod]
        public void User_Should_Fail_Login_With_WrongEmail()
        {
            loginPage.LoginApplication("test@test.testWrong", "test");

            Assert.AreEqual("Bad email or password.", loginPage.ErrorMessage);
        }

        [TestMethod]
        public void User_Should_Fail_Login_With_WrongPassword()
        {
            loginPage.LoginApplication("test@test.test", "testWrong");

            Assert.AreEqual("Bad email or password.", loginPage.ErrorMessage);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
