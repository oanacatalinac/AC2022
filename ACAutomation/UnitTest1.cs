using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ACAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void User_Should_Login_Successfully()
        {
            //open browser
            var driver = new ChromeDriver();

            //maximize browser window
            driver.Manage().Window.Maximize();

            //navigate to url
            driver.Navigate().GoToUrl("http://a.testaddressbook.com/");

            //click sign in button
            var btnSignIn = driver.FindElement(By.Id("sign-in"));
            btnSignIn.Click();
            Thread.Sleep(2000);

            //fill in email
            driver.FindElement(By.Id("session_email")).SendKeys("test@test.test");

            //fill in password
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("test");

            //click sign in button
            driver.FindElement(By.Name("commit")).Click();

            //assert email
            Assert.IsTrue(driver.FindElement(By.XPath("//span[@data-test='current-user']")).Text.Equals("test@test.test"));

            //close browser
            driver.Quit();
        }
    }
}
