﻿using ACAutomation.PageObjects;
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

            var btnSignIn = driver.FindElement(By.Id("sign-in"));
            btnSignIn.Click();

            // implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        [TestMethod]
        public void User_Should_Login_Successfully()
        {
            loginPage.LoginApplication("test@test.test", "test");

            Assert.IsTrue(driver.FindElement(By.XPath("//span[@data-test='current-user']")).Text.Equals("test@test.test"));
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
